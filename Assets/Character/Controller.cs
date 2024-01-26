using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] 
    public float speed = 2f;
    public float friction = 1f;
    public float base_speed;
    [SerializeField] 
    private float padding = 0.2f;
    [SerializeField]
    private SpriteRenderer sprite;

    private Rigidbody2D rb;

    private int donut = 0;


    public void AddDonut()
    {
        donut++;
        switch (donut)
        {
            case 1:
                speed *= 0.9f;
                transform.localScale *= 1.05f;
                break;
            case 2:
                speed *= 0.9f;
                transform.localScale *= 1.05f;
                break;
            case 3:
                sprite.color = Color.green;
                speed *= 0.5f;
                transform.localScale *= 1.2f;
                break;
            default:
                break;
        }
    }

    public int GetDonutsCount()
    {
        return donut;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        base_speed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Run();
    }

    public void ResetFat()
    {
        speed = base_speed;
        transform.localScale = Vector2.one;
        donut = 0;
        sprite.color = Color.white;
    }

    private void Run()
    {
        //transform.position += Vector3.right * speed * Time.deltaTime;
        Vector2 velocity;
        velocity.x = Input.GetAxis("Horizontal") * speed;
        velocity.y = Input.GetAxis("Vertical") * speed;

        if (velocity.x > 0)
        {
            sprite.flipX = false;
        }
        else if (velocity.y < 0)
        {
            sprite.flipX = true;
        }
        rb.AddForce(velocity);
        rb.AddForce(-rb.velocity * friction);
    }
}
