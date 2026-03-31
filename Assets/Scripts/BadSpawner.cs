using UnityEngine;

public class SpawnerBad : MonoBehaviour
{
    public GameObject cubePrefab;
    public int count = 1000;

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(cubePrefab);
        }
    }
}