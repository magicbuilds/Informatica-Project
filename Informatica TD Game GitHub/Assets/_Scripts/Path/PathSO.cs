using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Path", menuName = "ScriptableObjects/PathSO")]
public class PathSO : ScriptableObject
{
    public Paths pathType;
    public int weight;

    public List<Vector2> basePathPositions;

    public List<Vector2> baseWaypointPositions;

    public List<Vector2> nextChunkPositions;
    public List<ChunkManager.Directions> nextChunckRotations;

    public List<Vector2> baseEnemySpawnPositions;

    public enum Paths
    {
        Start,
        End,
        Straight,
        RTurn,
        LTurn,
        TJunktionLR,
        TJunktionR,
        TJunktionL,
        Crossroad
    }
}
