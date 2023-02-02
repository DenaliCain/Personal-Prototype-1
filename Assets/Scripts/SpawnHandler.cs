using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    private Vector3 spawnPos = Vector3.zero;
    private Renderer cubeRenderer;
    private Color cubeColor;
    public GameObject cubePrefab;
    void Start()
    {
        CubeSetDifficultyColor(MainManager.Instance.NewDifficulty);
        Instantiate(cubePrefab, spawnPos, cubePrefab.transform.rotation); 
    }

    void CubeSetDifficultyColor(string s)
    {
        cubeRenderer = cubePrefab.GetComponent<Renderer>();
        switch (s)
        {
            case "Easy":
                cubeColor = Color.green;
                break;
            case "Medium":
                cubeColor = Color.yellow;
                break;
            case "Hard":
                cubeColor = Color.red;
                break;
        }
        cubeRenderer.sharedMaterial.color = cubeColor;
    }
}
