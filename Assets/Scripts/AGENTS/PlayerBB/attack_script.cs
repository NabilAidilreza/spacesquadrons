using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_script : MonoBehaviour
{
    base_behaviour bb;
    GameObject target;
    GameObject squadron;
    //private float MaxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        bb = GetComponent<base_behaviour>();
        int type = bb.type;
        target = bb.target;
        bb.SetSpeed(15f);
        if (bb.attackScript == null)
        {
            bb.attackScript = gameObject.AddComponent<Attack>();
            bb.attackScript.target = target;
            bb.attackScript.weight = 20.0f;
            bb.attackScript.enabled = true;
            if(bb.boidcoh == null){
                //Boids
                bb.boidcoh = gameObject.AddComponent<BoidCohesion>();
                bb.boidcoh.targets = bb.squadron.GetComponent<SquadronLeader>().ShipLst;
                bb.boidcoh.weight = 0.1f;
                bb.boidcoh.enabled = true;
            }
            if(bb.boidsep == null){
                bb.boidsep = gameObject.AddComponent<BoidSeparation>();
                if(type == 0) // SW
                {
                    bb.boidsep.desiredSeparation = 15f;
                }
                else if(type == 1) //QU
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
                bb.boidsep.targets = bb.squadron.GetComponent<SquadronLeader>().ShipLst;
                bb.boidsep.weight = 120f;
                bb.boidsep.enabled = true;
            }
        }
    }
    private void OnDestroy()
    {
        Destroy(bb.attackScript);
        //Destroy(bb.boidcoh);
        //Destroy(bb.boidsep);
    }
}
