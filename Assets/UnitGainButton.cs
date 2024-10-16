using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitGainButton : MonoBehaviour
{
    UnitGain unitGain;
    [SerializeField] Image monsterImage;
    [SerializeField] TMP_Text monsterName, monsterDescription, unitGainText;
    [SerializeField] PlayerManager playerManager;

    public void SetUnitGain(UnitGain _unitGain)
    {
        unitGain = _unitGain;
        monsterImage.sprite = unitGain.enemyToGain.enemySprite;
        monsterName.text = unitGain.enemyToGain.enemyName;
        monsterDescription.text = unitGain.enemyToGain.enemyDescription;
        string unitGainString = "Gain ";
        if (unitGain.instantGain > 0)
        {
            unitGainString += unitGain.instantGain + " now";
            if (unitGain.perRoundGain > 0)
            {
                unitGainString += " and gain ";
            }
        }
        if (unitGain.perRoundGain > 0)
        {
            unitGainString += unitGain.perRoundGain + " at the start of the next " + unitGain.rounds + " rounds";
        }
        unitGainString += ".";
        unitGainText.text = unitGainString;
    }

    public void Select()
    {
        playerManager.AddMonsters(unitGain.enemyToGain.enemyID, unitGain.instantGain, unitGain.perRoundGain, unitGain.rounds);
        GameManager.Instance.NewRound();
    }
}
