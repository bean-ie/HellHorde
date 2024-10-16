using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour
{
    EnemyAI enemyai;
    [SerializeField] float effectiveDistance;
    private void Awake()
    {
        enemyai = GetComponent<EnemyAI>();
    }

    void Update()
    {
        if (DistanceToClosestEnemy() > effectiveDistance)
        {
            enemyai.speed = 15;
        } 
        else
        {
            enemyai.speed = 4;
        }
    }

    float DistanceToClosestEnemy()
    {
        float distance = 10000;
        if (FindObjectsOfType<EnemyAI>().Length == 1)
        {
            return distance;
        }
        foreach (EnemyAI enemy in FindObjectsOfType<EnemyAI>())
        {
            float distanceToEnemy = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distanceToEnemy < (distance) && enemy != enemyai)
            {
                distance = distanceToEnemy;
            }
        }
        return distance;
    }
}
