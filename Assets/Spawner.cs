using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject squareHologramPrefab;
    [SerializeField] Camera cam;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] GameObject NPCPlayer;
    [SerializeField] float spawnDistance;

    public float maxUnits;
    float available;
    float deployed;
    public float availableDelay, availableAmount;
    float availableDelayTimer;
    [SerializeField] float startingAvailable;
    [SerializeField] TMP_Text maxUnitsText;
    [SerializeField] Slider availableSlider, deployedSlider, availableTimerSlider;
    [SerializeField] GameObject nextRoundButton;

    [SerializeField] PickEnemyButton[] pickEnemyButtons;

    GameObject squareHologram;
    Vector3 startingCorner;
    Vector2 hologramScale;

    private void Start()
    {
        UpdateSliderMaxValues();
        UpdateUnitAmounts(20);
        available = startingAvailable;
    }

    private void Update()
    {
        /* The multiple-spawn idea (make if have time)
         * 
        if (Input.GetMouseButtonDown(1))
        {
            startingCorner = cam.ScreenToWorldPoint(Input.mousePosition);
            startingCorner.z = 0;
            squareHologram = Instantiate(squareHologramPrefab, startingCorner, Quaternion.identity);
        }
        else if (Input.GetMouseButton(1))
        {
            // hologram
            hologramScale.x = Mathf.Abs(cam.ScreenToWorldPoint(Input.mousePosition).x - startingCorner.x);
            hologramScale.y = Mathf.Abs(cam.ScreenToWorldPoint(Input.mousePosition).y - startingCorner.y);
            squareHologram.transform.localScale = hologramScale;
            squareHologram.transform.position = (startingCorner + cam.ScreenToWorldPoint(Input.mousePosition)) / 2;
        }
        if (Input.GetMouseButtonUp(1))
        {
            // hologram
            Destroy(squareHologram);

            // spawning
        }
        *
        */


        if (Input.GetMouseButtonDown(1))
        {
            if (playerManager.monsterAmount[playerManager.selectedMonster] > 0 && deployed < available)
            {
                if ((MouseWorld() - (Vector2)NPCPlayer.transform.position).magnitude > spawnDistance)
                {
                    Instantiate(playerManager.monsters[playerManager.selectedMonster].prefab, MouseWorld(), Quaternion.identity);
                    playerManager.monsterAmount[playerManager.selectedMonster]--;
                    deployed++;
                    AudioManager.instance.SpawnEnemy();
                }
            }
        }

        if (available < maxUnits && TimeManager.instance.running)
        {
            if (availableDelayTimer > availableDelay)
            {
                availableDelayTimer -= availableDelay;
                available = Mathf.Clamp(available + availableAmount, 0, maxUnits);
            }
            else
            {
                availableDelayTimer += Time.deltaTime;
            }
        }

        if (available == maxUnits && TimeManager.instance.absoluteRunning)
        {
            nextRoundButton.SetActive(true);
            if (deployed == available && FindObjectsOfType<EnemyAI>().Length == 0)
            {
                EndRound();
            }
        }

        availableSlider.value = available;
        deployedSlider.value = deployed;
        availableTimerSlider.value = availableDelayTimer;
        deployedSlider.maxValue = available;
    }

    void UpdateSliderMaxValues()
    {
        maxUnitsText.text = maxUnits.ToString();
        availableSlider.maxValue = maxUnits;
        availableTimerSlider.maxValue = availableDelay;
    }

    public void EndRound()
    {
        if (!TimeManager.instance.absoluteRunning) return;

        nextRoundButton.SetActive(false);
        GameManager.Instance.RoundEnd();
    }

    public void UpdateUnitAmounts(int _maxUnits)
    {
        maxUnits = _maxUnits;
        available = startingAvailable;
        deployed = 0;
        UpdateSliderMaxValues();
        for (int i = 0; i < pickEnemyButtons.Length; i++)
        {
            pickEnemyButtons[i].UpdateActive();
        }
    }

    Vector2 MouseWorld()
    {
        return (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
