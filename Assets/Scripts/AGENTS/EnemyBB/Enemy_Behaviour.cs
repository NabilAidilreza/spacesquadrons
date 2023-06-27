using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    public int team;

    public enemy_idle idle;
    public enemy_seek seek; //Move to point
    public enemy_patrol patrol; //move to patrol point
    public enemy_attack attack; // Attack point

    public Agent agentScript;

    public Seek seekScript;
    public BoidCohesion boidcoh;
    public BoidSeparation boidsep;
    public Patrol patrolScript;
    public Attack attackScript;

    private float maxSpeed;

    public GameObject target;
    public UnitFSM state;
    public int type;

    public enum UnitFSM
    {
        Idle,
        Seek,
        Attack,
        Patrol
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
        if (Num == 0)
        {
            changeState(UnitFSM.Idle);
        }
        else if (Num == 1)
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

    }
    public void changeState(UnitFSM new_state)
    {
        state = new_state;
        switch (new_state)
        {
            case UnitFSM.Idle:
                if (gameObject.GetComponent<enemy_idle>() == null)
                {
                    idle = gameObject.AddComponent<enemy_idle>();
                }
                Destroy(seek);
                Destroy(attack);
                Destroy(patrol);
                break;
            case UnitFSM.Seek:
                if (gameObject.GetComponent<enemy_seek>() == null)
                {
                    seek = gameObject.AddComponent<enemy_seek>();
                }
                Destroy(idle);
                Destroy(attack);
                Destroy(patrol);
                break;
            case UnitFSM.Attack:
                if (gameObject.GetComponent<enemy_attack>() == null)
                {
                    attack = gameObject.AddComponent<enemy_attack>();
                }
                Destroy(seek);
                Destroy(idle);
                Destroy(patrol);
                break;
            case UnitFSM.Patrol:
                if (gameObject.GetComponent<enemy_patrol>() == null)
                {
                    patrol = gameObject.AddComponent<enemy_patrol>();
                }
                Destroy(seek);
                Destroy(idle);
                Destroy(attack);
                break;
        }
    }
}
