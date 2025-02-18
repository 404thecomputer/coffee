using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float horizontal;
    public float vertical;
    public float speed = 5.0f;
    private Rigidbody2D rigidBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rigidBody.linearVelocity = new Vector2(horizontal * speed, vertical * speed);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "EnemyTrigger")
        {
            Debug.Log("EnemyTrigger hit");
            GameManager.Instance.activateEnemies();
        }
    }
}
