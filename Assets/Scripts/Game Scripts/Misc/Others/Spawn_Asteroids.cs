using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Asteroids : MonoBehaviour
{
    public Transform Environment;
    public GameObject Asteroid1;
    public GameObject Asteroid2;
    public GameObject Asteroid3;
    public float TimeBetweenSpawn;
    private float SpawnTime;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTime = TimeBetweenSpawn;
        for(int i = 0; i < 60; i++)
        {
            int ran = Random.Range(0, 2);
            if(ran == 0)
            {
                GameObject a = Instantiate(Asteroid1,new Vector3(Random.Range(minX,maxX),Random.Range(minY,maxY),0),Quaternion.identity);
                a.transform.parent = Environment;
            }
            else if (ran == 1)
            {
                GameObject a = Instantiate(Asteroid2, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
                a.transform.parent = Environment;
            }
            else if (ran == 2)
            {
                GameObject a = Instantiate(Asteroid3, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), Quaternion.identity);
                a.transform.parent = Environment;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Asteroid[] Asteroids = GameObject.FindObjectsOfType<Asteroid>();
        if(Asteroids.Length < 100)
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
