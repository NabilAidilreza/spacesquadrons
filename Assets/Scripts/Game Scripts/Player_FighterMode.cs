﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FighterMode : MonoBehaviour
{
    private RTSController RTSControllerGameObject;

    private float Speed;
    private float AttackRange;
    private float BreakOffRange;
    public GameObject EnemyTarget;
    public GameObject MovePoint;
    public Transform ShootPoint;
    private bool RunAvailable;
    private bool Retreat;
    private Vector3 RP;

    public Rigidbody2D Projectile;
    public float ShootRate;
    private float SR;
    private int i = 0;
    public Transform ShootPoint1;
    public Transform ShootPoint2;
    private float ProjectileSpeed = 100f;
    public bool isHoming;

    [SerializeField] private AudioClip gunSound;

    // Start is called before the first frame update
    void Start()
    {
        // Set the RTSController GameObject //
        Speed = 20f;
        AttackRange = 50f;
        BreakOffRange = 20f;
        RunAvailable = true;
        Retreat = false;
        RP = new Vector3();

        RTSControllerGameObject = GameObject.FindWithTag ("RTSController").GetComponent<RTSController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Attack Pattern //
        if(EnemyTarget != null)
        {
            float dist = Vector3.Distance(transform.position, EnemyTarget.transform.position);
            if(RunAvailable == true){
                if(AttackRange > dist && dist > BreakOffRange){
                    Engage();
                }
                else if(dist < BreakOffRange){
                    // Create a retreat point //
                    RP = CreateRetreatPoint();
                    Retreat = true;
                    RunAvailable = false;
                }
                else{
                    MoveToTarget();
                }
            }

            if(Retreat==true){
                float distRP = Vector3.Distance(EnemyTarget.transform.position, RP);
                if(distRP>AttackRange){
                    Vector2 CirclePoint = Random.insideUnitCircle * (BreakOffRange+10f);
                    RP = EnemyTarget.transform.position + new Vector3(CirclePoint.x, CirclePoint.y, 0);
                }
                Disengage(RP);
            }

        }
        else
        {
            RunAvailable = true;
            Retreat = false;
            RP = new Vector3();
            Hover();
        }
    }
    void Engage(){
        // Shoot //
        Shoot();
        // Move to enemy //
        MoveToTarget();
    }
    void Disengage(Vector3 RP){
        Vector3 moveVector = RP - this.transform.position;
        transform.up = moveVector;
        // Move to point //
        transform.position = Vector3.MoveTowards(transform.position, RP, Speed * Time.deltaTime);    
        if(transform.position == RP){
            Retreat = false;
            RunAvailable=true;
        }
    }
    void Hover(){
        // Face point //
        Vector3 moveVector = ShootPoint.position - this.transform.position;
        transform.up = moveVector;
        // Move to point //
        transform.position = Vector3.MoveTowards(transform.position, ShootPoint.position, 5f * Time.deltaTime);    
    }
    private Vector3 CreateRetreatPoint(){
        Vector3 RetreatPoint = new Vector3();
        Vector2 CirclePoint = Random.insideUnitCircle * BreakOffRange;
        Vector3 backward = (Random.Range(-1,1) * transform.up);
        RetreatPoint = this.transform.position + (backward * (BreakOffRange/2)) + new Vector3(CirclePoint.x, CirclePoint.y, 0);
        return RetreatPoint;
    }
    void MoveToPoint(){
        // Face point //
        Vector3 moveVector = MovePoint.transform.position - this.transform.position;
        transform.up = moveVector;
        // Move to point //
        transform.position = Vector3.MoveTowards(transform.position, MovePoint.transform.position, Speed * Time.deltaTime);
    }
    void MoveToTarget(){
        // Face enemy //
        Vector3 moveVector = EnemyTarget.transform.position - this.transform.position;
        transform.up = moveVector;
        // Move to enemy //
        transform.position = Vector3.MoveTowards(transform.position, EnemyTarget.transform.position, Speed * Time.deltaTime);
    }
    void Shoot(){
        Vector3 relativeTarget = (EnemyTarget.transform.position - transform.position).normalized;
        //Vector3.right if you have a sprite rotated in the right direction
        Quaternion toQuaternion = Quaternion.FromToRotation(Vector3.up, relativeTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, toQuaternion, 90f * Time.deltaTime);
        // Start Shooting
        if (SR < 0)
        {
            if(isHoming == true)
            {
                Instantiate(Projectile, ShootPoint1.position, transform.rotation);
                Instantiate(Projectile, ShootPoint2.position, transform.rotation);
            }
            else
            {
                SoundFXManager.instance.PlaySoundFXClip(gunSound, transform, 0.3f);
                if (i % 2 == 0)
                {
                    Rigidbody2D projectileInstance;
                    projectileInstance = Instantiate(Projectile, ShootPoint1.position, transform.rotation) as Rigidbody2D;
                    projectileInstance.GetComponent<Rigidbody2D>().velocity = transform.up * ProjectileSpeed;
                }
                else
                {
                    Rigidbody2D projectileInstance;
                    projectileInstance = Instantiate(Projectile, ShootPoint2.position, transform.rotation) as Rigidbody2D;
                    projectileInstance.GetComponent<Rigidbody2D>().velocity = transform.up * ProjectileSpeed;
                }
            }
            i++;
            SR = ShootRate;
        }
        else
        {
            SR -= Time.deltaTime;
        }
    }
    void CheckForCommandDuringEngagement(){
        // If command was given to unit, off script and on action script //

    }
    public void SetEnemy(GameObject enemy){
        EnemyTarget = enemy;
    }
    public bool GetEnemy(){
        if(EnemyTarget){
            return false;
        }
        else{
            return true;
        }
    }
}
