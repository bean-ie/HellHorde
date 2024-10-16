using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBolt : MonoBehaviour
{
    public int damage;
    public Vector2 direction;
    public float speed;
    float life = 5;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!TimeManager.instance.running)
        {
            rb.bodyType = RigidbodyType2D.Static;
            return;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        life -= Time.deltaTime;
        if (life <= 0) Destroy(gameObject);
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HeroManager>(out HeroManager heroManager))
        {
            heroManager.Damage(damage);
            Destroy(gameObject);
        }
    }
}
