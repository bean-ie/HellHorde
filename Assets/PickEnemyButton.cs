using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickEnemyButton : MonoBehaviour
{
    [SerializeField] int enemyID;
    [SerializeField] TMP_Text amountText;
    [SerializeField] PlayerManager playerManager;

    private void Start()
    {
        UpdateActive();
    }

    private void Update()
    {
        amountText.text = playerManager.monsterAmount[enemyID].ToString();
    }

    public void UpdateActive()
    {
        gameObject.SetActive(playerManager.monsterAmount[enemyID] > 0);
    }

    public void Click()
    {
        playerManager.SelectMonster(enemyID);
    }
}
