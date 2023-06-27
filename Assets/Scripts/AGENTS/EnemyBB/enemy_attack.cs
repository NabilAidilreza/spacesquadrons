using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_attack : MonoBehaviour
{
    Enemy_Behaviour bb;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        bb = GetComponent<Enemy_Behaviour>();
        target = bb.target;
        if (bb.attackScript == null)
        {
            bb.attackScript = gameObject.AddComponent<Attack>();
            bb.attackScript.target = target;
            bb.attackScript.weight = 20.0f;
            bb.attackScript.enabled = true;
            //Boids
            bb.boidcoh = gameObject.AddComponent<BoidCohesion>();
            bb.boidcoh.targets = bb.target.GetComponent<EnemySquadron>().ShipLst;
            bb.boidcoh.weight = 0.1f;
            bb.boidcoh.enabled = true;

            bb.boidsep = gameObject.AddComponent<BoidSeparation>();
            bb.boidsep.targets = bb.target.GetComponent<EnemySquadron>().ShipLst;
            bb.boidsep.weight = 120f;
            bb.boidsep.enabled = true;
        }
    }
    private void OnDestroy()
    {
        Destroy(bb.attackScript);
        Destroy(bb.boidcoh);
        Destroy(bb.boidsep);
    }
}