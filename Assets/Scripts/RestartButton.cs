using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    private SpawnManager spawnManager;
    private Button button;

    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Reset);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Reset()
    {
        Debug.Log(button.gameObject.name + " was clicked.");
        spawnManager.StartGame(spawnManager.updatedDifficulty);
    }
}
