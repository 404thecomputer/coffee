using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private int xDirection;
    private int yDirection;
    public float speed = 1.0f;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        xDirection = 0;
        yDirection = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision!");

    }

    void FixedUpdate()
    {
        // Vector3 playerloc = Camera.main.WorldToScreenPoint(player.transform.position);
        // player.transform.position.x;
        if (player.transform.position.x < transform.position.x)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }
        if (player.transform.position.y < transform.position.y)
        {
            yDirection = -1;
        }
        else
        {
            yDirection = 1;
        }
        rigidBody.linearVelocity = new Vector2(xDirection * speed, yDirection * speed);
    }

}
