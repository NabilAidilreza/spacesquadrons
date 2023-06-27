using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swarmMissile : MonoBehaviour
{
    public GameObject swarmMissileProjectile;
    public GameObject targetUnit;
    public int numProjectiles = 15;
    public float swarmSpeed = 10.0f;
    public float swarmSpread = 3.0f;
    public GameObject Explosion;

    private float timeToDetonate;


    // Launches a capsule //
    // Detonates upon timer //
    // For targets in area in front of it //
    // Assign targets to clusters //
    void Start()
    {
        float dist = Vector3.Distance(this.transform.position, targetUnit.transform.position);
        timeToDetonate = (dist / swarmSpeed) - 2f;  
        Debug.Log(timeToDetonate);
    }
    void Update(){
        transform.position += transform.up * swarmSpeed * Time.deltaTime;
        if (timeToDetonate <= 0.0f){
            FireSwarmMissileCluster();
            DestroySelf();
        }
        else
        {
            timeToDetonate -= Time.deltaTime;
        }
    }
    public void DestroySelf(){
        Destroy(gameObject);
        GameObject explos = Instantiate(Explosion, this.transform.position, Quaternion.identity);
    }
    public void FireSwarmMissileCluster()
    {
        // Get direction of target //
        Vector3 direction = (targetUnit.transform.position - transform.position).normalized;

        // Create 10 projectiles //
        for (int i = 0; i < numProjectiles; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-swarmSpread, swarmSpread), Random.Range(-swarmSpread, swarmSpread), 0);
            Vector3 velocity = (direction + offset) * swarmSpeed;
            // Create projectile //
            GameObject smallerProjectile = Instantiate(swarmMissileProjectile, this.transform.position + offset, Quaternion.identity);
            // Set starting velocity and target //
            smallerProjectile.GetComponent<swarmProjectile>().velocity = velocity;
            smallerProjectile.GetComponent<swarmProjectile>().SetTarget(targetUnit.transform);
        }
    }
}
