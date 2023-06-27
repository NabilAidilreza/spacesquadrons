using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DroneController : MonoBehaviour
{
    private BoidAgent agent;
    private float stopDist = 10f;
    private GameObject HealBeam;
    private Transform currTurret;
    private Transform prevTurret = null;
    private float timeCheck;
    private float timeCheckReset = 15f;
    private float timeToPos;
    private float timeToReset = 3f;
    private bool IsHeal;
    private bool HasDrop;
    // Start is called before the first frame update
    void Start()
    {
        HasDrop = false;
        timeToPos = 0;
        IsHeal = false;
        timeCheck = timeCheckReset;
        agent = GetComponent<BoidAgent>();
        HealBeam = this.gameObject.transform.GetChild(0).GetComponent<Laser>().gameObject;
        currTurret = GetLowestTurret();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsHeal == true)
        {
            HealMode();
            agent.DestroyPatrolPoint();
            HasDrop = false;
        }
        else
        { 
            if(HasDrop == false)
            {
                agent.DropPatrolPoint();
                HasDrop = true;
            }

            agent.Move(agent.Patrol());
            // Patrol
            if(timeToPos < 0)
            {
                agent.GetRandomPosition();
                timeToPos = timeToReset;
            }
            else
            {
                timeToPos -= Time.deltaTime;
            }
        }
        
    }
    public void HealMode()
    {
        if (currTurret != null)
        {
            if (currTurret.GetComponent<Health>().currhp >= currTurret.GetComponent<Health>().GetMaxHP())
            {
                currTurret = GetLowestTurret();
            }
            float d = Vector2.Distance(transform.position, currTurret.transform.position);
            if (d < stopDist)
            {
                // Stop and Heal //
                HealBeam.SetActive(true);
                agent.RotateToTurret(currTurret.transform);
                currTurret.GetComponent<Health>().Regenerate();
            }
            else if (d > stopDist)
            {
                HealBeam.SetActive(false);
                // Move to Turret //
                Vector2 arrive = agent.Arrive(currTurret.transform);
                agent.Move(arrive);
            }
        }
        else
        {
            currTurret = GetLowestTurret();
        }
        if(timeCheck < 0)
        {
            prevTurret = currTurret;
            currTurret = GetLowestTurret();
            if(prevTurret == currTurret)
            {
                IsHeal = false;
                Debug.Log("WORKED");
            }
        }
        else
        {
            timeCheck -= Time.deltaTime;
        }

    }
    public Transform GetLowestTurret()
    {
        float low = Mathf.Infinity;
        Turret lowestTurret = null;
        Turret[] turrets = GameObject.FindObjectsOfType<Turret>();
        foreach (Turret tur in turrets)
        {
            if (tur.GetComponent<Player>())
            {
                float currHP = tur.GetComponent<Health>().currhp;
                if (currHP < low)
                {
                    low = currHP;
                    lowestTurret = tur;
                }
            }
        }
        if (lowestTurret == null)
        {
            return null;
        }
        return lowestTurret.GetComponent<Transform>();
    }
}
