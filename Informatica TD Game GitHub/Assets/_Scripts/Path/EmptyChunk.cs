using UnityEngine;

public class EmptyChunk : MonoBehaviour
{
    public int pathIndex;
    public int pathNumber;
    public PathSO.Rotations rotation;

    private bool isntAlreadyPressed = true;

    private void OnMouseDown()
    {
        //GameManager.Instance.gameState == GameManager.GameState.ChoseNextChunk &&  
        if (isntAlreadyPressed)
        {
            ChunkManager.Instance.SpawnNextChunk(transform.position, rotation, pathIndex, pathNumber);
            Destroy(gameObject);
            isntAlreadyPressed = false;
        }
    }
}
