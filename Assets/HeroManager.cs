using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroManager : MonoBehaviour
{
    public int health;
    int currentHealth;

    [SerializeField] int damage;

    [SerializeField] float speed;
    [SerializeField] float maxTurnTimer;
    Rigidbody2D rb;
    float turnTimer;
    [SerializeField] GameObject shootPrefab;
    public float reloadTime;
    [SerializeField] Slider healthSlider;
    float shotTimer;
    Vector2 direction;
    SpriteRenderer spriteRenderer;

    [SerializeField] float timeUntilHeal;
    [SerializeField] float healDelay, heal;
    float healTimer, healDelayTimer;

    float stunTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = health;
        healthSlider.maxValue = health;
        turnTimer = maxTurnTimer;
    }

    void Update()
    {
        healthSlider.value = currentHealth;
        healthSlider.maxValue = health;
        if (currentHealth <= 0)
        {
            GameManager.Instance.WinGame();
            spriteRenderer.color = Color.gray;
        }
        if (!TimeManager.instance.running || stunTime > 0)
        {
            rb.bodyType = RigidbodyType2D.Static;
            if (stunTime > 0) stunTime -= Time.deltaTime;
            return;
        } 
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        if (turnTimer >= maxTurnTimer)
        {
            float angle = Random.Range(0, Mathf.PI * 2);
            direction.x = Mathf.Cos(angle);
            direction.y = Mathf.Sin(angle);
            turnTimer -= maxTurnTimer;
        } 
        else
        {
            turnTimer += Time.deltaTime;
        }

        Shooting();

        rb.velocity = direction * speed;
        if (rb.velocity.x < 0) spriteRenderer.flipX = true;

        healTimer -= Time.deltaTime;
        if (healTimer <= 0)
        {
            if (healDelayTimer >= healDelay)
            {
                RestoreHP((int)heal);
                healDelayTimer -= healDelay;
            }
            else healDelayTimer += Time.deltaTime;
        }
    }

    public void Damage(int damage)
    {
        ResetHealTimer();
        currentHealth -= damage;
        AudioManager.instance.PlayerHit();
    }

    public void ResetHealTimer()
    {
        healTimer = timeUntilHeal;
    }

    void Shooting()
    {
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        if (enemies.Length == 0) return;

        Vector2 closestEnemyPosition = Vector2.zero;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (i == 0) closestEnemyPosition = enemies[i].transform.position;

            if ((enemies[i].transform.position - transform.position).sqrMagnitude < (closestEnemyPosition - (Vector2)transform.position).sqrMagnitude)
            {
                closestEnemyPosition = enemies[i].transform.position;
            }
        }

        Vector2 directionToClosestEnemy = closestEnemyPosition - (Vector2)transform.position;

        if (shotTimer >= reloadTime)
        {
            HeroBolt bolt = Instantiate(shootPrefab, transform.position, Quaternion.identity).GetComponent<HeroBolt>();
            bolt.damage = damage;
            bolt.direction = directionToClosestEnemy;
            shotTimer -= reloadTime;
        }
        else
        {
            shotTimer += Time.deltaTime;
        }
    }

    public void RestoreHP(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, health);
    }

    public void RestoreHP()
    {
        currentHealth = health;
    }

    public void Stun(float duration)
    {
        stunTime = duration;
    }
}
