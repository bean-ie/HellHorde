using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public string enemyDescription;
    public Sprite enemySprite;
    public int enemyID;
    public GameObject prefab;
}
