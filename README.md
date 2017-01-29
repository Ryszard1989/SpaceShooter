# SpaceShooter
Unity tutorial and extension work

##Goals:
* Levels - 
  * More enemies/Faster enemies.
  * Level text "Level 1" "Level 1 Complete!"
  * Enemies take more hits to kill (prereq to bosses). Collision with player an issue?
  * Boss (start with enlarged enemy. Look for asset?).
  * Background - pan out. expand Boundary. Will work well to diminish weapon upgrade benefit and later levels.
  
* Enemies -
  * Firerate - change firerate to random range or make relateable to level (level  fire rate).
  * **DONE 22/01** Make a new enemy with a new asset (good for refresh of tutorial).
  
* Weapon upgrade
  * Laser colour.
  * **DONE 22/01** Splits 1/2 side-by-side/3 split/3 split + 2sbs, 5 split, 5split + 2sbs
  * **DONE 29/01** Weapon upgrade based on number of enemies killed/score (simple)
  * Weapon upgrade based on collecting floating upgrade pickup item.
  
* Effects
  * Background - faster for boss or between levels. "warp in" effect.

##Techdebt:

*Fix rotation issue with tilt and shotspawns (without the hack resetting it
```c#
transform.rotation = Quaternion.Euler(transform.rotation.x, master.transform.rotation.y, transform.rotation.x);
Instantiate(shot, new Vector3(shotSpawn.position.x, 0.0f, shotSpawn.position.z), new Quaternion(shotSpawn.transform.eulerAngles.x, 0.0f, shotSpawn.transform.eulerAngles.z));
```
* **DONE 29/01** Obsolete Level load call warning to be fixed.
