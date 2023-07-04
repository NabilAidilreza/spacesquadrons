using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage_Military_Drones : MonoBehaviour
{
    private List<GameObject> MilitaryDrone_Lst = new List<GameObject>();
    public GameObject MilitaryDrone;
    public Transform SpawnPoint;
    public Transform SpawnPoint2;
    public Transform SpawnPoint3;
    public Transform SpawnPoint4;

    public Transform PatrolPoint;
    // Start is called before the first frame update
    void Start()
    {
        GameObject MilitaryDrone1 = Instantiate(MilitaryDrone, SpawnPoint.position, transform.rotation);
        MilitaryDrone1.GetComponent<Military_Drone>().MovePoint = SpawnPoint;
        GameObject MilitaryDrone2 = Instantiate(MilitaryDrone, SpawnPoint2.position, transform.rotation);
        MilitaryDrone2.GetComponent<Military_Drone>().MovePoint = SpawnPoint2;
        GameObject MilitaryDrone3 = Instantiate(MilitaryDrone, SpawnPoint3.position, transform.rotation);
        MilitaryDrone3.GetComponent<Military_Drone>().MovePoint = SpawnPoint3;
        GameObject MilitaryDrone4 = Instantiate(MilitaryDrone, SpawnPoint4.position, transform.rotation);
        MilitaryDrone4.GetComponent<Military_Drone>().MovePoint = SpawnPoint4;
        MilitaryDrone_Lst.Add(MilitaryDrone1);
        MilitaryDrone_Lst.Add(MilitaryDrone2);
        MilitaryDrone_Lst.Add(MilitaryDrone3);
        MilitaryDrone_Lst.Add(MilitaryDrone4);
    }

    // Update is called once per frame
    void Update()
    {
        PatrolArea();
    }
    private void PatrolArea(){
        transform.position = Vector3.MoveTowards(transform.position, PatrolPoint.position, 15f * Time.deltaTime);
    }
    private void FindAndSetTarget(){

    }
    private void CheckStatus(){
        if(MilitaryDrone_Lst.Count == 0){
            Destroy(gameObject);
        }
    }
}
