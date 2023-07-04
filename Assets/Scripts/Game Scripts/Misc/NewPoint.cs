using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPoint : MonoBehaviour
{
    private float Cooldown;
    private float resetCooldown;
    public GameObject waypoint;
    public GameObject Squad;
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        resetCooldown = 1.5f;
        Cooldown = resetCooldown;
        radius = 30f;
        Vector2 CirclePoint = Random.insideUnitCircle * radius;
        this.transform.position = waypoint.transform.position + new Vector3(CirclePoint.x, CirclePoint.y, 0);
        if(Mathf.Abs(this.transform.position.x - Squad.transform.position.x) < 5f){
            this.transform.position = waypoint.transform.position + new Vector3(Random.Range(-1,1)*10f, 0, 0);   
        }
        if(Mathf.Abs(this.transform.position.y - Squad.transform.position.y) < 5f){
            this.transform.position = waypoint.transform.position + new Vector3(0, Random.Range(-1,1)*10f, 0);   
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Cooldown <= 0)
        {
            if(waypoint != null)
            {
                Vector2 CirclePoint = Random.insideUnitCircle * radius;
                this.transform.position = waypoint.transform.position + new Vector3(CirclePoint.x, CirclePoint.y, 0);
                if(Mathf.Abs(this.transform.position.x - Squad.transform.position.x) < 5f){
                    this.transform.position = waypoint.transform.position + new Vector3(Random.Range(-1,1)*10f, 0, 0);   
                }
                if(Mathf.Abs(this.transform.position.y - Squad.transform.position.y) < 5f){
                    this.transform.position = waypoint.transform.position + new Vector3(0, Random.Range(-1,1)*10f, 0);   
                }
            }

            Cooldown = resetCooldown;
        }
        else
        {
            Cooldown -= Time.deltaTime;
        }
    }
    public void SetRadius(float rad){
        radius = rad;
    }
}
