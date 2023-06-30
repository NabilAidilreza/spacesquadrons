using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction_Drone : MonoBehaviour
{
    public GameObject ControlCenter; // passed by control center
    private Vector3 BuildPoint;
    public List<GameObject> BuildableLst; // passed by control center
    private List<GameObject> BuildingLst;
    public LayerMask WhatisBuilding;
    public GameObject Construction_Bar;
    private int checkTimer = 1;
    private float BuildRadius = 15f;
    private int BuildAttempt = 0;
    private bool HasBuilt = false;
    private float BuildTimer = 5f;
    private float currMaxBuild = 15;
    private float buildLevel = 1;
    public GameObject CheckUpgradeableBuilding;
    private float upgradeTime = 10f;
    private float touchTimer = 1;
    private int state = 0;
    public GameObject Upgrade_Bar;
    public GameObject Minus_5_PopOut;
    public GameObject Minus_20_PopOut;
    // Start is called before the first frame update
    void Start()
    {
        // 0 is BUILD
        // 1 is UPGRADE
        BuildPoint = GenerateBuildPoint();
        CheckUpgradeableBuilding = null;
        BuildingLst = ControlCenter.GetComponent<City_Control_Center>().BuildingLst;

    }

    // Update is called once per frame
    void Update()
    {
        BuildingLst = ControlCenter.GetComponent<City_Control_Center>().BuildingLst;
        CheckCityProgress();
        if(state == 0){
            Build();
        }
        else{
            Upgrade();
        }
        //Build();
    }
    private void Upgrade(){
        if(CheckUpgradeableBuilding == null){
            CheckUpgradeableBuilding = FindUpgradeableBuilding();
        }
        if(CheckUpgradeableBuilding != null){
            if(transform.position == CheckUpgradeableBuilding.transform.position){
                if (upgradeTime == 10f && touchTimer == 1){
                    Instantiate(Upgrade_Bar, transform.position + new Vector3(-0.08f, 0.5f, 0f), Quaternion.identity);
                    touchTimer = 0;
                }
                else if(upgradeTime > 0){
                    upgradeTime -= Time.deltaTime;
                }
                else{
                    CheckUpgradeableBuilding.GetComponent<City_Building>().IncreaseLevel();
                    ControlCenter.SendMessage("RemovePoints",20f);
                    Instantiate(Minus_20_PopOut, transform.position + new Vector3(-0.08f, 1f, 0f), Quaternion.identity);
                    upgradeTime = 10f;
                    touchTimer = 1;
                    CheckUpgradeableBuilding = FindUpgradeableBuilding();
                }
            }
            else{
                MoveToUpgradePoint(CheckUpgradeableBuilding);
            }    
        }
    }
    private GameObject FindUpgradeableBuilding(){
        int curr_index = Random.Range(0, BuildingLst.Count);
        GameObject CheckUpgradeableBuilding = BuildingLst[curr_index];
        if(CheckUpgradeableBuilding != null){
            if(CheckUpgradeableBuilding.GetComponent<City_Building>() != null){
                if(CheckUpgradeableBuilding.GetComponent<City_Building>().GetLevel() <= buildLevel){
                    return CheckUpgradeableBuilding;
                }
                else{
                    return null;
                }
            }
            else{
                return FindUpgradeableBuilding();
            }
        }
        else{
            return null;
        }
    }
    private void MoveToUpgradePoint(GameObject Building){
        // Move to upgrade point
        transform.position = Vector3.MoveTowards(transform.position, Building.transform.position, 20f * Time.deltaTime);
        transform.up = Building.transform.position - transform.position;
    } 
    private void Build(){
        if(transform.position != BuildPoint && HasBuilt == false){
            if(BuildPoint != transform.position){
                MoveToBuildPoint();
            }
        }
        else if(transform.position == BuildPoint){
            if (BuildTimer == 5f && checkTimer == 1){
                Instantiate(Construction_Bar, transform.position + new Vector3(-0.08f, 0.5f, 0f), Quaternion.identity);
                checkTimer = 0;
            }
            else if(BuildTimer > 0){
                BuildTimer -= Time.deltaTime;
            }
            else{
                Build_Building();
                HasBuilt = true;
                BuildPoint = GenerateBuildPoint();
                BuildTimer = 5f;
                checkTimer = 1;
            }
        }
    }
    private void CheckCityProgress(){
        int curr_count = BuildingLst.Count; // exclude control center
        float curr_total_level = ControlCenter.GetComponent<City_Control_Center>().GetCurrTotalLevel();
        if(curr_count >= currMaxBuild){
            if(curr_total_level > (currMaxBuild * 1.5f)){
                // City has reached current max build
                // To build more
                buildLevel += 1;
                ControlCenter.SendMessage("AddLevel",buildLevel);
                BuildRadius += 10f;
                currMaxBuild += 10;       
                state = 0;        
            }
            // UPGRADE
            else{
                state = 1;
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
        Collider2D[] Colliders = Physics2D.OverlapCircleAll(randomBulidPoint, 5f, WhatisBuilding);
        if(Colliders.Length == 0){
            BuildAttempt = 0;
            return randomBulidPoint;
        }
        else if(BuildAttempt < 10){
            BuildAttempt++;
            return GenerateBuildPoint();
        }
        else{
            BuildAttempt = 0;
            return transform.position;
        }
    }
    private void MoveToBuildPoint(){
        transform.position = Vector3.MoveTowards(transform.position, BuildPoint, 20f * Time.deltaTime);
        transform.up = BuildPoint - transform.position;
    }
    private void Build_Building(){
        GameObject Building = Instantiate(BuildableLst[Random.Range(0, BuildableLst.Count)], BuildPoint, Quaternion.identity);
        Building.transform.parent = ControlCenter.transform;
        ControlCenter.GetComponent<City_Control_Center>().AddBuilding(Building);
        ControlCenter.SendMessage("RemovePoints",5f);
        Instantiate(Minus_5_PopOut, transform.position + new Vector3(-0.08f, 1f, 0f), Quaternion.identity);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
    public void AddNewBuidable(GameObject Building){
        BuildableLst.Add(Building);
    }
}
