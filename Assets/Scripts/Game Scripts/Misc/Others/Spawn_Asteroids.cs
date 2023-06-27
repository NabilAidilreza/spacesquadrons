using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Asteroids : MonoBehaviour
{
    public GameObject Asteroid1;
    public GameObject Asteroid2;
    public GameObject Asteroid3;
    public float TimeBetweenSpawn;
    private float SpawnTime;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    // Start is called before the first frame update
    void Start()
    {
        minX = -150;
        maxX = 150;
        minY = -150;
        maxY = 150;
        SpawnTime = TimeBetweenSpawn;
        for(int i = 0; i < 60; i++)
        {
            int ran = Random.Range(0, 2);
            if(ran == 0)
            {
                Instantiate(Asteroid1,new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),0),Quaternion.identity);
            }
            else if (ran == 1)
            {
                Instantiate(Asteroid2, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
            }
            else if (ran == 2)
            {
                Instantiate(Asteroid3, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Asteroid[] Asteroids = GameObject.FindObjectsOfType<Asteroid>();
        if(Asteroids.Length < 60)
        {
            if(SpawnTime < 0)
            {
                int ran = Random.Range(0, 2);
                if (ran == 0)
                {
                    Instantiate(Asteroid1, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
                }
                else if (ran == 1)
                {
                    Instantiate(Asteroid2, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
                }
                else if (ran == 2)
                {
                    Instantiate(Asteroid3, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
                }
                SpawnTime = TimeBetweenSpawn;
            }
            else
            {
                SpawnTime -= TimeBetweenSpawn;
            }
        }
    }
}
