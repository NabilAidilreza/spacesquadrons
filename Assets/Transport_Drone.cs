using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport_Drone : MonoBehaviour
{
    public GameObject ControlCenter;
    public List<GameObject> BuildingLst; // passed by control center
    private List<Vector3> waypoints = new List<Vector3>();
    private int i = 0;

    // Upon instantiate, control center will pass building list to this script, will update if got changed
    private void Start()
    {
        ScanBuildings();
    }
    private void UpdateBuildings(){
        BuildingLst = ControlCenter.GetComponent<City_Control_Center>().BuildingLst;
    }
    private void ScanBuildings()
    {
        // Simulate scanning buildings and generating random waypoints
        if(BuildingLst != null){
            waypoints = GenerateRandomWaypoints();
        }
    }
    private void ShuffleBuildings<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    private List<Vector3> GenerateRandomWaypoints()
    {
        ShuffleBuildings(BuildingLst);
        List<Vector3> randomWaypoints = new List<Vector3>();
        
        // Replace this logic with your actual implementation to generate random waypoints based on scanned buildings
        for (int i = 0; i < BuildingLst.Count; i++)
        {
            Vector3 randomWaypoint = new Vector3(BuildingLst[i].transform.position.x,BuildingLst[i].transform.position.y,BuildingLst[i].transform.position.z);
            randomWaypoints.Add(randomWaypoint);
        }

        return randomWaypoints;
    }
    private void Update(){
        Navigate();
    }
    private void Navigate()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[i], 20f * Time.deltaTime);
        transform.up = waypoints[i] - transform.position;
        if(transform.position == waypoints[i]){
            i++;
            if(i == waypoints.Count){
                i = 0;
                UpdateBuildings();
                ScanBuildings(); // Recalculate waypoints after visiting all buildings
            }
        }
    }

}
