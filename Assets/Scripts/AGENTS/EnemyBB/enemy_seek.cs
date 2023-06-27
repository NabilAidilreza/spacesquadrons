using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_seek : MonoBehaviour
{
    Enemy_Behaviour bb;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        bb = GetComponent<Enemy_Behaviour>();
        target = bb.target;
        if (bb.seekScript == null)
        {
            bb.seekScript = gameObject.AddComponent<Seek>();
            bb.seekScript.target = target;
            bb.seekScript.weight = 20.0f;
            bb.seekScript.enabled = true;
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
        Destroy(bb.seekScript);
        Destroy(bb.boidcoh);
        Destroy(bb.boidsep);
    }
}
