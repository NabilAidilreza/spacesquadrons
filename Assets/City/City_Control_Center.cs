using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City_Control_Center : MonoBehaviour
{
    public List<GameObject> BuildingLst;
    private List<GameObject> Construction_Drone_Lst = new List<GameObject>();
    private List<GameObject> Transport_Drone_Lst = new List<GameObject>();
    private List<GameObject> BuildableLst = new List<GameObject>();
    public GameObject ConstructionDrone;
    public GameObject TransportDrone;
    public GameObject PatrolDrone;
    public GameObject MilitaryDrone;
    public GameObject City_Object1;
    public GameObject City_Object2;
    public GameObject City_Object3;
    public GameObject City_Object4;
    public GameObject Bunker;
    public GameObject Bunker2;
    private int checkNumber = 0;
    private int lastNumber = 0;
    public int level;
    private int maxPoints;
    private int totalPoints;

    private int checkDone = 0;
    // Script to manage city drones
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        BuildableLst.Add(City_Object1);
        BuildableLst.Add(City_Object2);
        BuildableLst.Add(City_Object3);
        BuildableLst.Add(City_Object4);
        // Instantiate initial city drones
        BuildConstructionDrone();
        BuildConstructionDrone();
    }

    // Update is called once per frame
    void Update()
    {
        CheckToAddTransport();
        LevelLogic();
        ManageControlCenter();
    }
    private void LevelLogic(){
        if(BuildingLst.Count == 1){
            totalPoints = 20;
        }
        maxPoints = level * 100;
        if(totalPoints >= maxPoints){
            totalPoints = maxPoints;
        }
    }
    private void ManageControlCenter(){
        if(level > 3 && checkDone == 0){
            AddNewBuidableToDrones(Bunker);
            checkDone = 1;
        }
    }
    public void AddBuilding(GameObject Building){
        BuildingLst.Add(Building);
    }
    private void BuildConstructionDrone(){
        // Create Construction Drone
        GameObject C_DRONE = Instantiate(ConstructionDrone, transform.position, transform.rotation);
        C_DRONE.GetComponent<Construction_Drone>().ControlCenter = gameObject;
        C_DRONE.GetComponent<Construction_Drone>().BuildableLst = BuildableLst;
        C_DRONE.transform.parent = transform;
        Construction_Drone_Lst.Add(C_DRONE);
    }
    private void AddNewBuidableToDrones(GameObject Building){
        foreach (GameObject drone in Construction_Drone_Lst){
            drone.GetComponent<Construction_Drone>().AddNewBuidable(Building);
        }
    }
    private void BuildTransportDrone(){
        // Create Transport Drone
        GameObject T_DRONE = Instantiate(TransportDrone, transform.position, transform.rotation);
        T_DRONE.GetComponent<Transport_Drone>().ControlCenter = gameObject;
        T_DRONE.GetComponent<Transport_Drone>().BuildingLst = BuildingLst;
        T_DRONE.transform.parent = transform;
        Transport_Drone_Lst.Add(T_DRONE);
    }
    public float GetCurrTotalLevel(){
        float count = 0;
        for (int i = 0; i < BuildingLst.Count; i++){
            if(BuildingLst[i] != null){
                if(BuildingLst[i].GetComponent<City_Building>() != null){
                    count += BuildingLst[i].GetComponent<City_Building>().GetLevel();
                }
            }
        }
        return count;
    }
    public void CheckToAddTransport(){
        checkNumber = BuildingLst.Count;
        if(BuildingLst.Count % 3 == 0 && checkNumber != lastNumber){
            BuildTransportDrone();
            lastNumber = checkNumber;
        }
    }
    public void AddPoint(){
        totalPoints ++;
    }
    public void RemovePoints(int num){
        totalPoints -= num;
    }
    public void AddPoints(int num){
        totalPoints += num;
    }
    public void AddLevel(int lev){
        level = lev;
    }
}
