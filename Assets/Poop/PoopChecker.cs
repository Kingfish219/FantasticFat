using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopChecker : MonoBehaviour
{
    private Pathfinding.AILerp mover;
    private void Start()
    {
        mover = GetComponent<Pathfinding.AILerp>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Entred");
        if (collision.CompareTag("poop"))
        {
            mover.speed *= 0.1f;
            StartCoroutine(RemoveTimer(collision.gameObject));
        }
    }

    IEnumerator RemoveTimer(GameObject g)
    {
        yield return new WaitForSeconds(1.0f);
        mover.speed *= 10;
        Destroy(g);
    }
}
