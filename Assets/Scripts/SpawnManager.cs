using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float xRange = 15.0f;
    private float powerupXRange = 10.0f;
    private float zRangeOffset = 15.0f;
    private float zRange;
    private float ySpawnPos = 0.5f;
    private float spawnDelay = 1.0f;
    private float powerupSpawnDelay = 1.0f;
    private float spawnInterval = .35f;
    private float powerupSpawnInterval = 4.5f;
    public bool isGameActive;
    public int updatedDifficulty = 2;
    private int score;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI hUD;
    public TextMeshProUGUI titleScreen;
    private PlayerController playerController;

    public GameObject[] enemies;
    public GameObject[] powerups;
    private GameObject player;
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;


    private void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        UpdateZRange(); 
    }
    void SpawnRandomEnemy()
    {
        if (isGameActive)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), ySpawnPos, zRange);
            Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
        }
    }
    public void ToTitle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        Debug.Log(difficulty);
        isGameActive = true;
        UpdateScore(0);
        player.SetActive(true);
        titleScreen.gameObject.SetActive(false);
        hUD.gameObject.SetActive(true);
        playerController = player.GetComponent<PlayerController>();
        InvokeRepeating("SpawnRandomEnemy", spawnDelay, spawnInterval);
        InvokeRepeating("SpawnRandomPowerup", powerupSpawnDelay, powerupSpawnInterval);
    }
    void SpawnRandomPowerup()
    {
        if (isGameActive)
        {
            int powerupIndex = Random.Range(0, powerupPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-powerupXRange, powerupXRange), ySpawnPos, zRange);
            Instantiate(powerupPrefabs[powerupIndex], spawnPos, enemyPrefabs[powerupIndex].transform.rotation);
        }
    }
    void UpdateZRange()
    {
        if (isGameActive)
        {
            zRange = player.transform.position.z + zRangeOffset;
        }
    }
    public void ResetEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemy = enemies[i];
            enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z - playerController.resetDistance);
        }
    }
    public void ResetPowerups()
    {
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
        for (int i = 0; i < powerups.Length; i++)
        {
            GameObject powerup = powerups[i];
            powerup.transform.position = new Vector3(powerup.transform.position.x, powerup.transform.position.y, powerup.transform.position.z - playerController.resetDistance);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void UpdateDifficulty(int difficulty)
    {
        updatedDifficulty = difficulty;
    }
    public void GameOver()
    {
        player.SetActive(false);
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        hUD.gameObject.SetActive(false);

    }

}
