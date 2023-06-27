using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flak : MonoBehaviour
{
    public bool IsPlayer;
    public LayerMask WhatisEnemy;
    public float SplashRadius;
    public GameObject LargeExp;
    public void Start()
    {
        StartCoroutine(Die());
    }
    // Update is called once per frame
    void Update()
    {
        Transform Enemy = FindClosestObject();
        float Dist = Vector3.Distance(transform.position, Enemy.position);
        if(Dist < Random.Range(1,5))
        {
            float dmg = Random.Range(1, 8);
            Collider2D[] Colliders = Physics2D.OverlapCircleAll(transform.position, SplashRadius, WhatisEnemy);
            foreach(Collider2D collider in Colliders)
            {
                Health UnitHP = collider.GetComponent<Health>();
                UnitHP.TakeDmg(dmg);
            }
            Destroy(gameObject);
            Instantiate(LargeExp, transform.position, Quaternion.identity);
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    public Transform FindClosestObject()
    {
        if (IsPlayer == true)
        {
            float DtoC = Mathf.Infinity;
            Enemy closestEnemy = null;
            Enemy[] lall = GameObject.FindObjectsOfType<Enemy>();
            foreach (Enemy curr in lall)
            {
                float DtoE = (curr.transform.position - this.transform.position).sqrMagnitude;
                if (DtoE < DtoC)
                {
                    DtoC = DtoE;
                    closestEnemy = curr;
                }
            }
            if (closestEnemy == null)
            {
                return null;
            }
            return closestEnemy.GetComponent<Transform>();
        }
        else
        {
            float DtoC = Mathf.Infinity;
            Player closestPlayer = null;
            Player[] lall = GameObject.FindObjectsOfType<Player>();
            foreach (Player curr in lall)
            {
                float DtoE = (curr.transform.position - this.transform.position).sqrMagnitude;
                if (DtoE < DtoC)
                {
                    DtoC = DtoE;
                    closestPlayer = curr;
                }
            }
            if (closestPlayer == null)
            {
                return null;
            }
            return closestPlayer.GetComponent<Transform>();
        }
    }
}
