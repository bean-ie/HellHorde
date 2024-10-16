using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int[] monsterAmount;
    public Enemy[] monsters;
    List<Vector3Int> perRoundEnemies = new List<Vector3Int>();
    public int selectedMonster;

    private void Start()
    {
        AddMonsters(0, 0, 20, 3);
    }

    public void SelectMonster(int monster)
    {
        if (monsterAmount[monster] > 0)
        {
            selectedMonster = monster;
        }
    }

    public void AddMonsters(int ID, int instantAmount, int perRoundAmount, int rounds)
    {
        monsterAmount[ID] += instantAmount;
        Vector3Int newPerRoundEnemy = new Vector3Int(ID, perRoundAmount, rounds);
        perRoundEnemies.Add(newPerRoundEnemy);
    }

    public void UpdatePerRoundEnemies()
    {
        for (int i = 0; i < perRoundEnemies.Count; i++)
        {
            if (perRoundEnemies[i].z == 0) perRoundEnemies.RemoveAt(i);
            else
            {
                monsterAmount[perRoundEnemies[i].x] += perRoundEnemies[i].y;
                perRoundEnemies[i] -= Vector3Int.forward;
            }
        }
    }
}
