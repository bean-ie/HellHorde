using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAI : MonoBehaviour
{
    [SerializeField] float reloadTime, shotSpeed;
    [SerializeField] int shotDamage;
    [SerializeField] GameObject projectile;
    Transform hero;
    float shootTimer;
    private void Awake()
    {
        hero = FindObjectOfType<HeroManager>().transform;
    }
    void Update()
    {
        if (shootTimer > reloadTime)
        {
            EnemyBolt bolt = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<EnemyBolt>();
            bolt.speed = shotSpeed;
            bolt.damage = shotDamage;
            bolt.direction = ((hero.position + Vector3.up * 0.7f) - transform.position).normalized;
            shootTimer -= reloadTime;
        }
        else shootTimer += Time.deltaTime;
    }
}
