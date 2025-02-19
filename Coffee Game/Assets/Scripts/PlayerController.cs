using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float horizontal;
    public float vertical;
    public float speed = 5.0f;
    private Rigidbody2D rigidBody;
    public bool hasCoffee;
    [SerializeField] private AudioClip audioClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        hasCoffee = false;
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
            if (!hasCoffee)
            {
                Debug.Log("EnemyTrigger hit, no coffee");
                GameManager.Instance.activateEnemies();
            }
            else if (hasCoffee)
            {
                Debug.Log("EnemyTrigger hit, has coffee");
                GameManager.Instance.resetArena();
                collision.gameObject.GetComponent<EnemyTrigger>().particles.Play();
            }
        }
        else if (collision.name.Contains("Coffee"))
        {
            // Debug.Log("Coffee acquired");
            Destroy(collision.gameObject);
            addCoffee();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Enemy"))
        {
            removeCoffee();
            transform.position = new Vector3(0, 14);
        }
    }
    public void addCoffee()
    {
        hasCoffee = true;
        gameObject.GetComponent<TrailRenderer>().emitting = true;
        SoundManager.Instance.playSoundClip(audioClip, transform, 1.0f);

    }
    public void removeCoffee()
    {
        hasCoffee = false;
        gameObject.GetComponent<TrailRenderer>().emitting = false;

    }
}
