using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City_Bunker : MonoBehaviour
{
    public bool IsPlayer;
    public Rigidbody2D Projectile;
    public float EngageRange;
    public Transform ShootPoint;
    private float CoolDownDuration = 1f;
    private float CoolDown;
    private Transform target;
    private float projectileSpeed = 100f;

    private int burstCount;
    private float burstDelay = 0.1f;
    private float spreadAngle; // Control the spread angle of the bullets
    // Start is called before the first frame update
    void Start()
    {
        CoolDown = CoolDownDuration;
        burstCount = 3;
        spreadAngle = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();
        target = FindClosestObject();
        if (target != null)
        {
            float Range = Vector2.Distance(transform.position, target.transform.position);
            if (Range < EngageRange){
                if(CoolDown <= 0){
                    StartCoroutine(ShootBurst(target));
                    CoolDown = CoolDownDuration;
                }
                else{
                    CoolDown -= Time.deltaTime;
                }
            }
        }
    }
    private void UpdateStats(){
        City_Building building = gameObject.GetComponent<City_Building>();
        int currLevel = building.GetLevel();
        if(currLevel == 1){
            burstCount = 1;
            spreadAngle = 10f;
        }
        else if(currLevel == 2){
            burstCount = 2;
            spreadAngle = 20f;
        }
        else if(currLevel == 3){
            burstCount = 3;
            spreadAngle = 30f;
        }
        else{
            burstCount = 3;
            spreadAngle = 30f;
        }
    }
    private void RotateToShootAngle(Transform target){
        Vector3 relativeTarget = (target.transform.position - transform.position).normalized;

        // Calculate the angle between the direction vector and the positive X-axis
        float targetAngle = Mathf.Atan2(relativeTarget.y, relativeTarget.x) * Mathf.Rad2Deg;

        // Add a random angle offset to the target angle
        float angle = Random.Range(-spreadAngle, spreadAngle);
        float finalAngle = targetAngle + angle;

        // Convert the angle back to a direction vector
        Vector3 finalDirection = Quaternion.Euler(0f, 0f, finalAngle) * Vector3.right;

        Quaternion toQuaternion = Quaternion.FromToRotation(Vector3.up, finalDirection);
        ShootPoint.rotation = Quaternion.Slerp(ShootPoint.rotation, toQuaternion, 100f * Time.deltaTime);
    }
    private IEnumerator ShootBurst(Transform target){
        if(target != null){
            for(int i = 0; i < burstCount; i++){
                RotateToShootAngle(target);
                Rigidbody2D projectileInstance;
                projectileInstance = Instantiate(Projectile, transform.position, ShootPoint.rotation) as Rigidbody2D;
                projectileInstance.GetComponent<Rigidbody2D>().velocity = ShootPoint.up * projectileSpeed;
                yield return new WaitForSeconds(burstDelay);
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
