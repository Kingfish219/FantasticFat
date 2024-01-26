using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AiMover : MonoBehaviour
{
    public Transform targetPosition;

    private Seeker seeker;
    private Rigidbody2D rb;

    public Path path;

    public float speed = 2;
    public float friction = 1;

    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    public bool reachedEndOfPath;
    Vector3 velocity = Vector3.zero;
    [SerializeField] SpriteRenderer sprite;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        // If you are writing a 2D game you should remove this line
        // and use the alternative way to move sugggested further below.
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating(nameof(SetPath), 0.0f, 0.5f);
    }

    private void SetPath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
        }
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

        if (!p.error)
        {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }

    public void Update()
    {
        if (path == null)
        {
            return;
        }
        reachedEndOfPath = false;
        float distanceToWaypoint;
        while (true)
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        // Slow down smoothly upon approaching the end of the path
        // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        // Multiply the direction by our desired speed to get a velocity
        velocity = dir * speed * speedFactor;
        if (velocity.x > 0)
        {
            sprite.flipX = false;
        }
        else if (velocity.y < 0)
        {
            sprite.flipX = true;
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(velocity);
        rb.AddForce(-rb.velocity * friction);
    }
}
