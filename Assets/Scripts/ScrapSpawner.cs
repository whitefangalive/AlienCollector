using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapSpawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public Vector3 direction = Vector3.up;
    public int spawnRateMax = 10;
    public int spawnRateMin = 9;
    private int currentSpawnRate;
    public float Offset = 0;
    private PlayerStats ps;
    private bool once = true;
    List<GameObject> gameObjects = new List<GameObject>();
    public Transform parent = null;
    private void Start()
    {
        currentSpawnRate = Random.Range(spawnRateMax, spawnRateMin);
    }
    private void FixedUpdate()
    {
        if (ps == null)
        {
            ps = GameObject.Find("SaveState").GetComponent<PlayerStats>();
        }
        

        if ((Offset + ps.SpawningTimer) % (currentSpawnRate * 60) <= 2)
        {
            if (once)
            {
                once = false;
                currentSpawnRate = Random.Range(spawnRateMax, spawnRateMin);
                gameObjects.Add(Instantiate(ObjectToSpawn, new Vector3(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y +
                    Random.Range(-0.2f, 0.2f), transform.position.z), transform.rotation, parent));

                gameObjects[gameObjects.Count - 1].GetComponent<ConstantlyMove>().Direction = transform.rotation * direction;

            }
        } 
        else
        {
            once = true; 
        }
    }
}
