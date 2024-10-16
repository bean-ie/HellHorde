using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    [SerializeField] int health;
    public int damage;
    [SerializeField] GameObject deathParticle;
    bool canAttack = true;
    float attackCooldown = 1;
    [SerializeField] bool overrideCollision = false;
    float currentHealth;
    GameObject player;
    Rigidbody2D rb;

    private void Start()
    {
        currentHealth = health;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(Instantiate(deathParticle, transform.position, Quaternion.identity), 1f);
            AudioManager.instance.EnemyDeath();
            Destroy(gameObject);
        }
        if (!TimeManager.instance.running)
        {
            rb.bodyType = RigidbodyType2D.Static;
            return;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        rb.velocity = ((Vector2)player.transform.position - rb.position).normalized * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (overrideCollision) return;
        if (collision.gameObject.TryGetComponent<HeroManager>(out HeroManager manager) && canAttack)
        {
            manager.Damage(damage);
            Damage(7);
            canAttack = false;
            StartCoroutine("AttackCooldown");
        }   
    }

    public bool Damage(int damage)
    {
        bool doesKill;
        if (damage > currentHealth) doesKill = true;
        else doesKill = false;
        currentHealth -= damage;
        return doesKill;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
