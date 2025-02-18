using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger!");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Trigger Collision!!");
    }
}
