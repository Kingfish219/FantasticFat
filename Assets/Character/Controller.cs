using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody2D rd;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall", System.StringComparison.InvariantCultureIgnoreCase))
        {
            // Stop the sprite by setting its velocity to zero
            rd.velocity = Vector3.zero;
        }
    }

    private void Run()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
