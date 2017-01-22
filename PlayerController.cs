using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

[System.Serializable]
public class WeaponSelector
{
    public Transform[] shotSpawns;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    public float tilt;
    public Boundary boundary;

    public WeaponSelector[] weaponSelector;
    public GameObject shot;
    public float fireRate;
    public int weaponLevel;



    public GameObject shotSpawnRotationFix; //TODO - Need to find how to ignore the tilted rotation during Instantiate.
    private float nextFire;
    private int weaponLevelMax;

    private Rigidbody rb;
    private AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Finds and assigns the child of the player named "ShotSpawns".
        shotSpawnRotationFix = transform.Find("ShotSpawns").gameObject;
        audioSource = GetComponent<AudioSource>();
        weaponLevelMax = weaponSelector.Length;
        Debug.Log("weaponLevelMax: " + weaponLevelMax);
    }

    void Update ()
    {
        //Hack to upgrade weapons in game
        if(Input.GetKeyDown(KeyCode.B))
        {
            if (weaponLevel >= weaponLevelMax-1)
            {
                weaponLevel = 0;
            }
            else
            {
                weaponLevel++;
            }
        }

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            foreach (var shotSpawn in weaponSelector[weaponLevel].shotSpawns) {
                //TODO - Work out how to ignore y rotation during instantiate.
                //new Vector3(shotSpawn.position.x, 0.0f, shotSpawn.position.z)
                //customRotation = Quaternion.Euler(shotSpawn.rotation.x, 0.0f, shotSpawn.rotation.z);
                Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            }
            audioSource.Play();
        }
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

        //If the child was found, lock the shotspawn rotation
        if (shotSpawnRotationFix != null)
        {
            shotSpawnRotationFix.transform.rotation = Quaternion.identity;
        }
        else Debug.Log("No child with the name 'ShotSpawns' attached to the player");



    }



}
