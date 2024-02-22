using UnityEngine;

public class EmptyChunk : MonoBehaviour
{
    private bool isntAlreadyPressed = true;

    private void OnMouseDown()
    {
        //GameManager.Instance.gameState == GameManager.GameState.ChoseNextChunk &&
        if ( isntAlreadyPressed)
        {
            ChunkManager.Instance.SpawnNextChunk(transform.position);
            Destroy(gameObject);
            isntAlreadyPressed = false;
        }
    }
}
