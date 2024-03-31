using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StandardWaves", menuName = "ScriptableObjects/WavesSO")]
public class WavesSO : ScriptableObject
{
    public List<WaveSO> waves;
}
