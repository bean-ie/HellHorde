using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject winGameText;
    [SerializeField] HeroManager hero;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] UnitGainButton[] unitGainButtons;
    [SerializeField] UnitGain[] unitGainsFirstLevel, unitGainsSecondLevel, unitGainsBosses;
    [SerializeField] GameObject dark;
    [SerializeField] Spawner spawner;
    [SerializeField] GameObject loseGameText;
    [SerializeField] TMP_Text roundCounter;

    public int currentRound = 1;

    private void Awake()
    {
        Instance = this;
    }

    public void WinGame()
    {
        winGameText.SetActive(true);
        TimeManager.instance.StopTime();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RoundEnd()
    {
        TimeManager.instance.StopTime();
        if (currentRound == 7)
        {
            loseGameText.SetActive(true);
            return;
        }
        dark.SetActive(true);
        UnitGain[] unitGains;
        if (currentRound <= 2) unitGains = unitGainsFirstLevel;
        else if (currentRound <= 5) unitGains = unitGainsSecondLevel;
        else if (currentRound == 6) unitGains = unitGainsBosses;
        else unitGains = unitGainsFirstLevel;
        for (int i = 0; i < unitGainButtons.Length; i++)
        {
            int randomUnitGain = Random.Range(0, unitGains.Length);
            if (currentRound == 6) randomUnitGain = i;
            unitGainButtons[i].SetUnitGain(unitGains[randomUnitGain]);
            unitGainButtons[i].gameObject.SetActive(true);
        }
    }
    public void NewRound()
    {
        ResetMap();
        for (int i = 0; i < unitGainButtons.Length;i++)
        {
            unitGainButtons[i].gameObject.SetActive(false);
        }
        dark.SetActive(false);
        playerManager.UpdatePerRoundEnemies();
        int maxUnits = 0;
        for (int i = 0; i < playerManager.monsterAmount.Length; i++)
        {
            maxUnits += playerManager.monsterAmount[i];
        }
        spawner.UpdateUnitAmounts(maxUnits);
        currentRound++;
        hero.ResetHealTimer();
        roundCounter.text = "round " + (currentRound-1) + "/6";
        TimeManager.instance.ResumeTime();
    }

    public void ResetMap()
    {
        hero.transform.position = Vector3.zero;
        hero.RestoreHP(hero.health / 5);
        hero.health = Mathf.RoundToInt(hero.health * 1.2f);
        hero.reloadTime *= 0.9f;
        spawner.availableDelay *= 0.9f;
        foreach (EnemyAI enemy in FindObjectsOfType<EnemyAI>())
        {
            Destroy(enemy.gameObject);
        }
    }
}
