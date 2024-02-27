using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TempPathTester : MonoBehaviour
{
    [SerializeField] private int number = 1000;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            number = 0;
        }

        if (number <= 100)
        {
            EmptyChunk[] chunks = FindObjectsOfType<EmptyChunk>();
            foreach (EmptyChunk chunk in chunks)
            {
                Task.Delay(200);
                chunk.SpawnChunk();
                number++;
            }
        }
    }
}
