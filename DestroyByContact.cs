using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject enemyKilledExplosion;
    public GameObject playerKilledExplosion;
    public GameObject enemyHitExplosion;
    public int[] shotsToKill;
    public int scoreValue;
    private GameController gameController;
    private int shotsTaken;

    void Start ()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        //if(other.tag == "Boundary" || other.tag == "Enemy")
        if(other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return; //Boundary collision impossible based on movement limits. Enemies spawn behind each other so can't collide.
        }
        if(other.CompareTag("Bolt"))
        {
            enemyHit(other);
        }
        if (enemyKilledExplosion != null && shotsTaken >= shotsToKill[gameController.waveLevel])
        {
            enemyKilled(other);
        }
        if (other.CompareTag("Player"))
        {
            playerKilled(other);
        }
        Debug.Log("DestroyByContact:Other: " + other);        
    }

    void enemyHit(Collider other)
    {
        shotsTaken++;
        Instantiate(enemyHitExplosion, transform.position, transform.rotation);
        Destroy(other.gameObject);
    }

    void enemyKilled(Collider other)
    {
        Instantiate(enemyKilledExplosion, transform.position, transform.rotation);
        //*COMMENT TO TEST*    
        gameController.AddScore(scoreValue);
        Destroy(gameObject);
    }

    void playerKilled(Collider other)
    {
        Instantiate(playerKilledExplosion, other.transform.position, other.transform.rotation);
        Destroy(gameObject);
        Destroy(other.gameObject);
        gameController.GameOver();
    }
    

    //Handle Asteroids at spawn if don't want bounce
    //void OnTriggerStay(Collider other)
    //{
    //    Instantiate(explosion, transform.position, transform.rotation);
    //    Destroy(other.gameObject);
    //    Destroy(gameObject);
    //}

}
