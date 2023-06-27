using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime;
    public float distance;
    public LayerMask WhatisSolid;
    public float dmg;
    public GameObject SmallExp;
    public LayerMask WhatisEnemy;
    public float SplashRadius;
    public bool IsShock;
    private float ran;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDmg();
    }
    //DamageFunc 
    public void CheckDmg()
    {
        //HitInfo
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, WhatisSolid);
        if (hitInfo.collider != null)
        {
            //CheckEnemy
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                if (hitInfo.collider.GetComponent<Turret>() && IsShock == true)
                {
                    ran = Random.Range(0, 10);
                    if(ran >= 2 && ran <= 4)
                    {
                        hitInfo.collider.GetComponent<Turret>().UpdateTurretStatus("Offline");
                    }
                }
                //DamageRegistered
                hitInfo.collider.GetComponent<Health>().TakeDmg(dmg);
            }
            //CheckEnemy
            if (hitInfo.collider.CompareTag("Object"))
            {
                //DamageRegistered
                hitInfo.collider.GetComponent<Health>().TakeDmg(dmg);
            }
            //ProjectileGone
            DestroyProjectile();
        }
    }
    //DestroyFunc
    public void DestroyProjectile()
    {
        Collider2D[] Colliders = Physics2D.OverlapCircleAll(transform.position, SplashRadius, WhatisEnemy);
        foreach (Collider2D collider in Colliders)
        {
            Health UnitHP = collider.GetComponent<Health>();
            UnitHP.TakeDmg(dmg);
        }
        Destroy(gameObject);
        Instantiate(SmallExp, transform.position, Quaternion.identity);
    }
}
