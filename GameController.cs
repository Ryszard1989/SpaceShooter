﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    private int score;
    public int[] scoreWeaponLevelValues;
    private int weaponLevel;
    public GUIText restartText;
    public GUIText gameOverText;

    private bool gameOver;
    private bool restart;

    private PlayerController playerController;

    void Start ()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        weaponLevel = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    void Update ()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                //TODO - Obsolete
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        if (score >= scoreWeaponLevelValues[weaponLevel])
        {
            playerController.UpgradeWeapon();
            weaponLevel++;
        }
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }       
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();

    }

    void UpdateScore ()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
