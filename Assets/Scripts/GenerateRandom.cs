using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandom : MonoBehaviour
{
    public GameObject Prefab;
    [Range(1, 50)]
    public int numberOfElements;
    [Range(1f, 100f)]
    public float maxRadius;

    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < numberOfElements; i++)
        {
            GameObject go = Instantiate(Prefab);
            go.SetActive(true);
            go.transform.position = new Vector3(Random.Range(-maxRadius, maxRadius), 0.5f, Random.Range(-maxRadius, maxRadius));
        }
    }
}
