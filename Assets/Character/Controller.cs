using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] 
    public float speed = 2f;
    [SerializeField] 
    private float padding = 0.2f;
    [SerializeField]
    private SpriteRenderer sprite;

    private Rigidbody2D rd;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;



    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        rd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void Run()
    {
        //transform.position += Vector3.right * speed * Time.deltaTime;

        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        if (deltaX > 0)
        {
            sprite.flipX = false;
        }
        else if (deltaX < 0)
        {
            sprite.flipX = true;
        }

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Donut"))
        {
            print("doun");
            EatDonut(collision.gameObject);
        }
        
    }

    private void EatDonut(GameObject g)
    {
        var donut = g.GetComponent<Donut>();
        speed -= donut.health;
        Destroy(g);
    }
}
