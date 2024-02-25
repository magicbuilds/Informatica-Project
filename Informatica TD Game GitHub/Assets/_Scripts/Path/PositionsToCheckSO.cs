using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PositionsToCheck", menuName = "ScriptableObjects/PositionsToCheckSO")]
public class PositionsToCheckSO : ScriptableObject
{
    public List<Vector2> positionsToCheck;
    public List<ChunkManager.Rotations> correspondingRotations;

}
