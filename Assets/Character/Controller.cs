using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] 
    public float speed = 2f;
    public float base_speed;
    [SerializeField] 
    private float padding = 0.2f;
    [SerializeField]
    private SpriteRenderer sprite;

    private Rigidbody2D rb;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private int donut;


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
                speed *= 0.5f;
                transform.localScale *= 1.2f;
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        rb = GetComponent<Rigidbody2D>();
        base_speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    public void ResetFat()
    {
        speed = base_speed;
        transform.localScale = Vector2.one;
        donut = 0;
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
}
