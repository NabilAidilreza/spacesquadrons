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

    public Transform Enemy;

    private float AttackRange = 40f;

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
        MilitaryDrone_Lst.RemoveAll(GameObject => GameObject == null);
        if(Enemy == null){
            PatrolArea();
        }
        FindAndSetTarget();
    }
    private void PatrolArea(){
        transform.position = Vector3.MoveTowards(transform.position, PatrolPoint.position, 15f * Time.deltaTime);
    }
    private void FindAndSetTarget(){
        Transform target = FindClosestEnemy();
        if(target != null){
            float Range = Vector2.Distance(transform.position, target.transform.position);
            if (Range < AttackRange){
                Enemy = target;
                foreach (GameObject curr in MilitaryDrone_Lst){
                    curr.GetComponent<Military_Drone>().EnemyTarget = Enemy.gameObject;
                }
            }
        }

    }
    private void CheckStatus(){
        if(MilitaryDrone_Lst.Count == 0){
            Destroy(gameObject);
        }
    }



    public Transform FindClosestEnemy()
    {
        float DtoC = Mathf.Infinity;
        Enemy closestEnemy = null;
        Enemy[] lall = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy curr in lall)
        {
            float DtoE = (curr.transform.position - this.transform.position).sqrMagnitude;
            if (DtoE < DtoC)
            {
                DtoC = DtoE;
                closestEnemy = curr;
            }
        }
        if(closestEnemy == null)
        {
            return null;
        }
        return closestEnemy.GetComponent<Transform>();
    }
}
