using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBolt : MonoBehaviour
{
    public int damage;
    public Vector2 direction;
    [SerializeField] float speed;
    [SerializeField] float life;
    HeroManager hero;
    Rigidbody2D rb;
    float angle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hero = FindObjectOfType<HeroManager>();
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
        rb.velocity = direction.normalized * speed;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        rb.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyAI>(out EnemyAI enemyai))
        {
            if (!enemyai.Damage(damage))
            {
                Destroy(gameObject);
            } else
            {
                hero.RestoreHP(Random.Range(1,3));
                life -= 1f;
            }
        }
    }
}
