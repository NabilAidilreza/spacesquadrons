using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    private Transform target;
    public float speed;
    public float offset;
    public bool IsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        target = FindClosestObject();
    }

    // Update is called once per frame
    void Update()
    {
        target = FindClosestObject();
        if(target == null)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else
        {
            Vector3 difference = target.transform.position - transform.position;
            float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotz + offset);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
    public Transform FindClosestObject()
    {
        if (IsPlayer == true)
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
            return closestEnemy.GetComponent<Transform>();
        }
        else
        {
            float DtoC = Mathf.Infinity;
            Player closestPlayer = null;
            Player[] lall = GameObject.FindObjectsOfType<Player>();
            foreach (Player curr in lall)
            {
                float DtoE = (curr.transform.position - this.transform.position).sqrMagnitude;
                if (DtoE < DtoC)
                {
                    DtoC = DtoE;
                    closestPlayer = curr;
                }
            }
            return closestPlayer.GetComponent<Transform>();
        }
    }

}

