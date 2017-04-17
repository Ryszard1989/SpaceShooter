using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int[] hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveToClearScreenWait;
    public float levelCompleteTextTime;
    public float nextWaveWait;

    public GUIText scoreText;
    private int score;
    public int[] scoreWeaponLevelValues;
    private int weaponLevel;
    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText levelCompleteText;
    public int waveLevel = 1;

    private bool gameOver;
    private bool restart;
    private bool levelComplete;

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
        levelComplete = false;
        restartText.text = "";
        gameOverText.text = "";
        levelCompleteText.text = "";
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
                SceneManager.LoadScene("Main_Extended");

            }
        }
    }

    IEnumerator SpawnWaves ()
    {
        levelComplete = false;
        levelCompleteText.text = "";
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount[waveLevel]; i++)
            {
                GameObject hazard;
                if(waveLevel == 5) //BOSS level
                {
                    hazard = hazards[4];
                    //TODO - edit speed/size/fire rate for boss unit.
                }
                else
                {
                    hazard = hazards[Random.Range(0, hazards.Length)];
                }                
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveToClearScreenWait);
            if (!gameOver) //TODO - Can't refactor yield WaitForSeconds into void function?
            {
                levelCompleteText.text = "Level " + waveLevel + " Complete!";
                levelComplete = true;
                waveLevel++;
                yield return new WaitForSeconds(levelCompleteTextTime);
                levelCompleteText.text = "";
                yield return new WaitForSeconds(nextWaveWait);
            }
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
        //Level up weapon based on score thresholds. Make sure to make top level weapon score unreachably high.
        if (score >= scoreWeaponLevelValues[weaponLevel])
        {
            playerController.UpgradeWeapon();
            weaponLevel++;
        }

    }

    void UpdateScore ()
    {
        scoreText.text = "Score: " + score;
    }

    IEnumerator ShowLevelCompleteText() //TODO - When I use this times go all messed up. Read up on co-routines.
    {
        levelCompleteText.text = "Level " + waveLevel + " Complete!";
        levelComplete = true;
        waveLevel++;
        yield return new WaitForSeconds(levelCompleteTextTime);
        levelCompleteText.text = "";
        yield return new WaitForSeconds(nextWaveWait);
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
