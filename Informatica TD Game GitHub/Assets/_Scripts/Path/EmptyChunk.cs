using UnityEngine;

public class EmptyChunk : MonoBehaviour
{
    public int pathIndex;
    public int pathNumber;
    public ChunkManager.Directions rotation;

    public Waypoint waypoint;

    private bool isntAlreadyPressed = true;

    private void OnMouseDown()
    {
        if (UIManager.Instance.IsHoveringUI()) return;
        //GameManager.Instance.gameState == GameManager.GameState.ChoseNextChunk &&  
        if (GameManager.Instance.gameState == GameManager.GameState.ChoseNextChunk && isntAlreadyPressed)
        {
            
            
            ChunkManager.Instance.SpawnNewChunk(this);

            ChunkManager.Instance.emptyChunks.Remove(this);

            Destroy(gameObject);
            isntAlreadyPressed = false;
        }
    }

    public void SpawnChunk()
    {
        if (isntAlreadyPressed)
        {
            ChunkManager.Instance.SpawnNewChunk(this);
            Destroy(gameObject);
            isntAlreadyPressed = false;
        }
        
    }
}
