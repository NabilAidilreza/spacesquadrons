using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City_Control_Center : MonoBehaviour
{
    public List<GameObject> BuildingLst;
    public GameObject Builder_Drone;
    public GameObject TransportDrone;
    public GameObject PatrolDrone;
    public GameObject City_Object1;
    public GameObject City_Object2;
    public GameObject City_Object3;
    public GameObject City_Object4;
    // Script to manage city drones
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddBuilding(GameObject Buiilding){
        BuildingLst.Add(Buiilding);
    }
}
