using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DifficultyButton : MonoBehaviour
{
    private RestartButton restartButton;
    private SpawnManager spawnManager;
    private Button button;

    public int difficulty;
    void Start()
    {
        restartButton = GameObject.Find("Restart Button").GetComponent<RestartButton>();
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SetDifficulty()
    {
        Debug.Log(button.gameObject.name + " was clicked.");
        spawnManager.UpdateDifficulty(difficulty);
    }
}
