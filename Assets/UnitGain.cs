using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Gain", menuName = "Unit Gain")]
public class UnitGain : ScriptableObject
{
    public Enemy enemyToGain;
    public int instantGain;
    public int perRoundGain;
    public int rounds;
}
