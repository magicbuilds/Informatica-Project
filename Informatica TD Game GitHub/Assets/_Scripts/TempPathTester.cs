using UnityEngine;

public class TempPathTester : MonoBehaviour
{
    [SerializeField] private int number;

    [SerializeField] private float timePassed;
    private void Update()
    {
        
        if (number <= 5000)
        {
            timePassed += Time.deltaTime;
            if (timePassed > .5f) 
            {
                EmptyChunk[] chunks = FindObjectsOfType<EmptyChunk>();
                foreach (EmptyChunk chunk in chunks)
                {
                    chunk.SpawnChunk();
                    number++;
                }

                timePassed = 0;
            }
        }
    }
}
