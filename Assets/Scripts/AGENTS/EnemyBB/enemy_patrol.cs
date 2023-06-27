using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_patrol : MonoBehaviour
{
    Enemy_Behaviour bb;
    GameObject target;
    private float MaxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        bb = GetComponent<Enemy_Behaviour>();
        MaxSpeed = bb.GetSpeed();
        bb.SetSpeed(10.0f);
        int type = bb.type;
        target = bb.target;
        if (bb.patrolScript == null)
        {
            bb.patrolScript = gameObject.AddComponent<Patrol>();
            bb.patrolScript.target = target;
            bb.patrolScript.weight = 20.0f;
            bb.patrolScript.enabled = true;
            //Boids
            bb.boidcoh = gameObject.AddComponent<BoidCohesion>();
            bb.boidcoh.targets = bb.target.GetComponent<EnemySquadron>().ShipLst;
            bb.boidcoh.weight = 0.1f;
            bb.boidcoh.enabled = true;

            bb.boidsep = gameObject.AddComponent<BoidSeparation>();
            if (type == 0) // SW
            {
                bb.boidsep.desiredSeparation = 15f;
            }
            else if (type == 1) //QU
            {
                bb.boidsep.desiredSeparation = 20f;
            }
            else if (type == 2) //DE
            {
                bb.boidsep.desiredSeparation = 40f;
            }
            else if (type == 3) //SH
            {
                bb.boidsep.desiredSeparation = 25f;
            }
            bb.boidsep.targets = bb.target.GetComponent<EnemySquadron>().ShipLst;
            bb.boidsep.weight = 80f;
            bb.boidsep.enabled = true;
        }
    }
    private void OnDestroy()
    {
        bb.SetSpeed(MaxSpeed);
    }
}

