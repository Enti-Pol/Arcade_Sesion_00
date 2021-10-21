using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float speed;
    private Rigidbody2D rb2;

    
    [SerializeField]
    private int hp;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        rb2.velocity = new Vector2(0f, -speed);
    }
    private void FixedUpdate()
    {
        if (rb2.position.y <= -6f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            hp--;

            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
