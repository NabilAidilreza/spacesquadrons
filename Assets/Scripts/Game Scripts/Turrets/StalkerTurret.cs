using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerTurret : Turret
{
    public bool IsPlayer;
    public GameObject Projectile;
    public float EngageRange;
    public Transform ShootPoint1;
    public float CoolDownDuration;
    private float CoolDown;
    private Transform target;
    public float offset;
    private Animator Stalker;
    public float ShootWindowPeriod;
    private float SWP;
    public float OverHeatDuration;
    public GameObject OH_Effect;
    private GameObject OH;
    public GameObject ShockEffect;
    private GameObject Shock;
    // Start is called before the first frame update
    void Start()
    {
        Stalker = gameObject.GetComponent<Animator>();
        SWP = ShootWindowPeriod;
        CoolDown = CoolDownDuration;
        status = "Online";
    }

    // Update is called once per frame
    void Update()
    {
        if (status == "Online")
        {
            MainFunc();
        }
        else if (status == "Offline")
        {
            DisableTurret();
            status = "Null";
        }
    }
    IEnumerator Recover()
    {
        yield return new WaitForSeconds(5f);
        status = "Online";
    }
    public void DisableTurret()
    {
        Shock = Instantiate(ShockEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10), Quaternion.identity);
        Stalker.SetBool("Shooting", false);
        StartCoroutine("Recover");
    }
    IEnumerator ShootAgain()
    {
        Stalker.SetBool("Shooting", true);
        Instantiate(Projectile, ShootPoint1.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Projectile, ShootPoint1.position, Quaternion.identity);
        Stalker.SetBool("Shooting", false);
    }
    IEnumerator Overheat()
    {
        yield return new WaitForSeconds(OverHeatDuration);
        SWP = ShootWindowPeriod;
    }
    public void MainFunc()
    {
        target = FindClosestObject();
        if (target == null)
        {
            Stalker.SetBool("Shooting", false);
        }
        else
        {
            Vector3 relativeTarget = (target.transform.position - transform.position).normalized;
            Quaternion toQuaternion = Quaternion.FromToRotation(Vector3.up, relativeTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, toQuaternion, 3f * Time.deltaTime);
            float Range = Vector2.Distance(transform.position, target.transform.position);
            if (Range < EngageRange)
            {
                if (SWP < 0f)
                {
                    OH = Instantiate(OH_Effect, transform.position, Quaternion.identity);
                    StartCoroutine("Overheat");
                    SWP = 0f;
                }
                else if (SWP > 0f)
                {
                    if (CoolDown <= 0)
                    {
                        StartCoroutine(ShootAgain());
                        CoolDown = CoolDownDuration;
                    }
                    else
                    {

                        CoolDown -= Time.deltaTime;
                    }
                    SWP -= Time.deltaTime;
                }
            }
        }
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
            if(closestEnemy == null)
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
