using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class base_behaviour : MonoBehaviour
{
    public int team;
    public idle_script idle;
    public flee_script flee;

    public seek_script seek; //Move to point
    public patrol_script patrol; //move to patrol point
    public attack_script attack; // Attack point

    public Agent agentScript;

    public Seek seekScript;
    public BoidCohesion boidcoh;
    public BoidSeparation boidsep;
    public Flee fleeScript;
    public Patrol patrolScript;
    public Attack attackScript;

    private float maxSpeed;

    public GameObject target;
    public GameObject squadron;
    public UnitFSM state;

    public int type;

    public enum UnitFSM
    {
        Idle,
        Seek,
        Attack,
        Patrol,
        Flee
    }
    // Start is called before the first frame update
    void Start()
    {
        agentScript = gameObject.AddComponent<Agent>();
        agentScript.maxSpeed = maxSpeed;

        changeState(UnitFSM.Patrol);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        agentScript.maxSpeed = maxSpeed;
    }
    public void SetTarget(GameObject tar){
        target = tar;
    }
    public void SetSpeed(float newSpeed)
    {
        maxSpeed = newSpeed;
    }
    public float GetSpeed()
    {
        return maxSpeed;
    }
    public void changeStateNum(int Num)
    {
        if(Num == 0)
        {
            changeState(UnitFSM.Idle);
        }
        else if(Num == 1)
        {
            changeState(UnitFSM.Seek);
        }
        else if (Num == 2)
        {
            changeState(UnitFSM.Patrol);
        }
        else if (Num == 3)
        {
            changeState(UnitFSM.Attack);
        }
        else if (Num == 4)
        {
            changeState(UnitFSM.Flee);
        }

    }
    public void changeState(UnitFSM new_state)
    {
        state = new_state;
        switch (new_state)
        {
            case UnitFSM.Idle:
                if(gameObject.GetComponent<idle_script>() == null)
                {
                    idle = gameObject.AddComponent<idle_script>();
                }
                Destroy(seek);
                Destroy(attack);
                Destroy(flee);
                Destroy(patrol);
                break;
            case UnitFSM.Seek:
                if (gameObject.GetComponent<seek_script>() == null)
                {
                    seek = gameObject.AddComponent<seek_script>();
                }
                Destroy(idle);
                Destroy(attack);
                Destroy(flee);
                Destroy(patrol);
                break;
            case UnitFSM.Attack:
                if (gameObject.GetComponent<attack_script>() == null)
                {
                    attack = gameObject.AddComponent<attack_script>();
                }
                Destroy(seek);
                Destroy(idle);
                Destroy(flee);
                Destroy(patrol);
                break;
            case UnitFSM.Flee:
                if (gameObject.GetComponent<flee_script>() == null)
                {
                    flee = gameObject.AddComponent<flee_script>();
                }
                Destroy(seek);
                Destroy(idle);
                Destroy(attack);
                Destroy(patrol);
                break;
            case UnitFSM.Patrol:
                if (gameObject.GetComponent<patrol_script>() == null)
                {
                    patrol = gameObject.AddComponent<patrol_script>();
                }
                Destroy(seek);
                Destroy(flee);
                Destroy(idle);
                Destroy(attack);
                break;
        }
    }
}
