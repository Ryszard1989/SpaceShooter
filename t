[33mdiff --git i/DestroyByContact.cs w/DestroyByContact.cs[m
[33mindex 8b11eeb..0c5185e 100644[m
[33m--- i/DestroyByContact.cs[m
[33m+++ w/DestroyByContact.cs[m
[35m@@ -6,7 +6,7 @@[m[37m [mpublic class DestroyByContact : MonoBehaviour[m
[37m     public GameObject enemyKilledExplosion;[m
[37m     public GameObject playerKilledExplosion;[m
[37m     public GameObject enemyHitExplosion;[m
[1;31m-    public int shotsToKill;[m
[32m+[m[32m    public int[] shotsToKill;[m
[37m     public int scoreValue;[m
[37m     private GameController gameController;[m
[37m     private int shotsTaken;[m
[35m@@ -26,6 +26,7 @@[m[37m [mpublic class DestroyByContact : MonoBehaviour[m
[37m [m
[37m     void OnTriggerEnter(Collider other)[m
[37m     {[m
[32m+[m[41m        [m
[37m         //if(other.tag == "Boundary" || other.tag == "Enemy")[m
[37m         if(other.CompareTag("Boundary") || other.CompareTag("Enemy"))[m
[37m         {[m
[35m@@ -35,7 +36,7 @@[m[37m [mpublic class DestroyByContact : MonoBehaviour[m
[37m         {[m
[37m             enemyHit(other);[m
[37m         }[m
[1;31m-        if (enemyKilledExplosion != null && shotsTaken >= shotsToKill)[m
[32m+[m[32m        if (enemyKilledExplosion != null && shotsTaken >= shotsToKill[gameController.waveLevel])[m
[37m         {[m
[37m             enemyKilled(other);[m
[37m         }[m
[33mdiff --git i/GameController.cs w/GameController.cs[m
[33mindex a0ef1cf..8af83fe 100644[m
[33m--- i/GameController.cs[m
[33m+++ w/GameController.cs[m
[35m@@ -6,7 +6,7 @@[m[37m [mpublic class GameController : MonoBehaviour[m
[37m {[m
[37m     public GameObject[] hazards;[m
[37m     public Vector3 spawnValues;[m
[1;31m-    public int hazardCount;[m
[32m+[m[32m    public int[] hazardCount;[m
[37m     public float spawnWait;[m
[37m     public float startWait;[m
[37m     public float waveToClearScreenWait;[m
[35m@@ -20,7 +20,7 @@[m[37m [mpublic class GameController : MonoBehaviour[m
[37m     public GUIText restartText;[m
[37m     public GUIText gameOverText;[m
[37m     public GUIText levelCompleteText;[m
[1;31m-    private int levelNumberText = 1;[m
[32m+[m[32m    public int waveLevel = 1;[m
[37m [m
[37m     private bool gameOver;[m
[37m     private bool restart;[m
[35m@@ -70,7 +70,7 @@[m[37m [mpublic class GameController : MonoBehaviour[m
[37m         yield return new WaitForSeconds(startWait);[m
[37m         while (true)[m
[37m         {[m
[1;31m-            for (int i = 0; i < hazardCount; i++)[m
[32m+[m[32m            for (int i = 0; i < hazardCount[waveLevel]; i++)[m
[37m             {[m
[37m                 GameObject hazard = hazards[Random.Range(0, hazards.Length)];[m
[37m                 Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);[m
[35m@@ -81,9 +81,9 @@[m[37m [mpublic class GameController : MonoBehaviour[m
[37m             yield return new WaitForSeconds(waveToClearScreenWait);[m
[37m             if (!gameOver) //TODO - Can't refactor yield WaitForSeconds into void function?[m
[37m             {[m
[1;31m-                levelCompleteText.text = "Level " + levelNumberText + " Complete!";[m
[32m+[m[32m                levelCompleteText.text = "Level " + waveLevel + " Complete!";[m
[37m                 levelComplete = true;[m
[1;31m-                levelNumberText++;[m
[32m+[m[32m                waveLevel++;[m
[37m                 yield return new WaitForSeconds(levelCompleteTextTime);[m
[37m                 levelCompleteText.text = "";[m
[37m                 yield return new WaitForSeconds(nextWaveWait);[m
[35m@@ -117,9 +117,9 @@[m[37m [mpublic class GameController : MonoBehaviour[m
[37m [m
[37m     IEnumerator ShowLevelCompleteText() //TODO - When I use this times go all messed up. Read up on co-routines.[m
[37m     {[m
[1;31m-        levelCompleteText.text = "Level " + levelNumberText + " Complete!";[m
[32m+[m[32m        levelCompleteText.text = "Level " + waveLevel + " Complete!";[m
[37m         levelComplete = true;[m
[1;31m-        levelNumberText++;[m
[32m+[m[32m        waveLevel++;[m
[37m         yield return new WaitForSeconds(levelCompleteTextTime);[m
[37m         levelCompleteText.text = "";[m
[37m         yield return new WaitForSeconds(nextWaveWait);[m
