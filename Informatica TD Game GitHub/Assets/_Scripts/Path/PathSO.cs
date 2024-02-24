using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Path", menuName = "ScriptableObjects/PathSO")]
public class PathSO : ScriptableObject
{
    public string pathName;
    public int weight;

    public List<Vector2> basePathPositions;
    public List<Vector2> baseWaypointPositions;

    public List<Vector2> nextChunkPositions;
    public List<Rotations> nextChunckRotations;

    public List<Vector2> baseEnemySpawnPositions;

    public enum Rotations
    {
        None, Right, Backwards, Left 
    }
}
