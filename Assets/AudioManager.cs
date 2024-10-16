using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource enemyDeath, playerHit, spawnEnemy;
    private void Awake()
    {
        instance = this;
    }

    public void EnemyDeath()
    {
        enemyDeath.pitch = Random.Range(0.9f, 1.1f);
        enemyDeath.Play();
    }
    public void PlayerHit()
    {
        playerHit.pitch = Random.Range(0.9f, 1.1f);
        playerHit.Play();
    }
    public void SpawnEnemy()
    {
        spawnEnemy.pitch = Random.Range(0.9f, 1.1f);
        spawnEnemy.Play();
    }
}
