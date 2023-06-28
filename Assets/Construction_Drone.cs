using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction_Drone : MonoBehaviour
{
    public GameObject ControlCenter;
    private Vector3 BuildPoint;
    public List<GameObject> BuildableLst;
    public LayerMask WhatisBuilding;
    private float BuildRadius = 20f;
    private int BuildAttempt = 0;
    private bool HasBuilt = false;
    private float BuildTimer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        BuildPoint = GenerateBuildPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != BuildPoint && HasBuilt == false){
            MoveToBuildPoint();
        }
        else if(transform.position == BuildPoint){
            if(BuildTimer > 0){
                BuildTimer -= Time.deltaTime;
            }
            else{
                Build();
                HasBuilt = true;
                BuildPoint = GenerateBuildPoint();
                BuildTimer = 2f;
            }
        }
    }
    private Vector3 GenerateBuildPoint(){
        // Generate a random build point (x radius from control center)
        // Check if area is buildable
        HasBuilt = false;
        float randomX = Random.Range(-BuildRadius, BuildRadius);
        if(randomX < 10 || randomX > -10){
            randomX = Random.Range(-BuildRadius, BuildRadius);
        }
        float randomY = Random.Range(-BuildRadius, BuildRadius);
        if(randomY < 10 || randomY > -10){
            randomY = Random.Range(-BuildRadius, BuildRadius);
        }
        Vector3 randomBulidPoint = new Vector3(ControlCenter.transform.position.x, ControlCenter.transform.position.y, ControlCenter.transform.position.z) + new Vector3(randomX,randomY, 0f);
        Collider2D[] Colliders = Physics2D.OverlapCircleAll(randomBulidPoint, 2f, WhatisBuilding);
        if(Colliders.Length == 0){
            return randomBulidPoint;
        }
        else if(BuildAttempt < 10){
            BuildAttempt++;
            return GenerateBuildPoint();
        }
        else{
            BuildAttempt = 0;
            return transform.position;
            // Stop
        }
    }
    private void MoveToBuildPoint(){
        transform.position = Vector3.MoveTowards(transform.position, BuildPoint, 20f * Time.deltaTime);
        transform.up = BuildPoint - transform.position;
    }
    private void Build(){
        GameObject Building = Instantiate(BuildableLst[Random.Range(0, BuildableLst.Count)], BuildPoint, Quaternion.identity);
        Building.transform.parent = ControlCenter.transform;
        ControlCenter.GetComponent<City_Control_Center>().AddBuilding(Building);
    }
}
