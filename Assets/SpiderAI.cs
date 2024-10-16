using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : MonoBehaviour
{
    [SerializeField] float stunDuration;
    EnemyAI enemyAI;
    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<HeroManager>(out HeroManager manager))
        {
            manager.Stun(stunDuration);
            manager.Damage(enemyAI.damage);
            enemyAI.Damage(100000);
        }
    }
}
