using UnityEngine;

public class EmptyChunk : MonoBehaviour
{
    public PathSO.Rotations rotation;

    private bool isntAlreadyPressed = true;

    private void OnMouseDown()
    {
        //GameManager.Instance.gameState == GameManager.GameState.ChoseNextChunk &&
        if (isntAlreadyPressed)
        {
            ChunkManager.Instance.SpawnNextChunk(transform.position, rotation);
            Destroy(gameObject);
            isntAlreadyPressed = false;
        }
    }
}
