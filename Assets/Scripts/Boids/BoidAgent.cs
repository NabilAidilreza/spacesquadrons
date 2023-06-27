using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidAgent : MonoBehaviour
{
    public Transform target;
    public Transform futurePos;
    public GameObject PatrolPoint;
    // Seek Variables //
    [Range(5,20)]
    public float maxSpeed;
    public Vector2 velocity = Vector2.zero;
    private Vector2 desired = Vector2.zero;
    private Vector2 steering = Vector2.zero;
    private Vector2 previous;
    // Arrive Variables //
    private float arriveRadius = 15f;
    private float arriveSpeed;
    // Grouping Variables //
    public List<BoidAgent> NearbyUnits;
    private float NeighbourRadius = 15.0f;
    private float UnitRadius = 1f;


    private float timeReset;
    private float reset;

    // Start is called before the first frame update
    void Start()
    {
        PatrolPoint = transform.gameObject;
        reset = 3;
        timeReset = 0;
        NearbyUnits = new List<BoidAgent>();
        velocity = transform.up;
        arriveSpeed = maxSpeed / 2;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = (Vector2)transform.position - previous;
        previous = transform.position;
        if (timeReset < 0)
        {
            GetAllNeighbours();
            timeReset = reset;
        }
        else
        {
            timeReset -= Time.deltaTime;
        }
        NearbyUnits.RemoveAll(GameObject => GameObject == null);


        if(target != null)
        { 
            /*
            //RotateAroundTurret();
            Vector2 force = Vector2.zero;
            //force += Arrive(target);
            force += Separate(NearbyUnits) * 0.5f;
            force += Allign(NearbyUnits) * 0.1f;
            transform.up = force;
            transform.position += (Vector3)force * Time.deltaTime;
            */
        }
    }
    // Movement Functions //
    public void Move(Vector2 velocity)
    {
        velocity += Separate(NearbyUnits) * 0.5f;
        velocity += Allign(NearbyUnits) * 0.1f;
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
    public Vector2 Seek(Transform target)
    {
        desired = target.transform.position - transform.position;
        desired = desired.normalized * maxSpeed;
        steering = desired - velocity;
        if (steering.sqrMagnitude > (maxSpeed * maxSpeed))
        {
            steering = steering.normalized * maxSpeed;
        }
        return steering;
    }
    public Vector2 Flee(Transform target)
    {
        desired = transform.position - target.transform.position;
        desired = desired.normalized * maxSpeed;
        steering = desired - velocity;
        if (steering.sqrMagnitude > (maxSpeed * maxSpeed))
        {
            steering = steering.normalized * maxSpeed;
        }
        return steering;
    }
    public Vector2 Arrive(Transform target)
    {
        float d = Vector2.Distance(transform.position, target.transform.position);
        if(d < arriveRadius)
        {
            desired = target.transform.position - transform.position;
            desired = desired.normalized * maxSpeed * (d/ arriveRadius);
        }
        else
        {
            desired = target.transform.position - transform.position;
            desired = desired.normalized * maxSpeed;
        }
        steering = desired - velocity;
        if (steering.sqrMagnitude > (maxSpeed * maxSpeed))
        {
            steering = steering.normalized * maxSpeed;
        }
        return steering;
    }
    public Vector2 Pursue(Transform target)
    {
        float d = Vector2.Distance(transform.position, target.transform.position);
        float Foresight = d / maxSpeed;
        futurePos.transform.position = target.transform.position + (Vector3)desired * Foresight;
        return Seek(futurePos);
    }
    public Vector2 Evade(Transform target)
    {
        float d = Vector2.Distance(transform.position, target.transform.position);
        float Foresight = d / maxSpeed;
        futurePos.transform.position = target.transform.position + (Vector3)desired * Foresight;
        return Flee(futurePos);
    }
    public Vector2 Patrol()
    {
        return Arrive(futurePos.transform);
    }
    public void RotateAroundTurret(Transform target)
    {
        Vector3 rotationMask = new Vector3(0, 0, 1); //which axes to rotate around
        float rotationSpeed = 60.0f; //degrees per second
        transform.RotateAround(target.transform.position,
        rotationMask, rotationSpeed * Time.deltaTime);
        RotateToTurret(target);
    }
    public void RotateToTurret(Transform target)
    {
        Vector2 relativeTarget = (target.transform.position - transform.position).normalized;
        Vector3 relTarget = new Vector3(relativeTarget.x, relativeTarget.y, 0);
        Quaternion toQuaternion = Quaternion.FromToRotation(Vector3.up, relTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, toQuaternion, 5f * Time.deltaTime);
    }
    
    // Grouping Functions //
    public Vector2 Separate(List<BoidAgent> Agents)
    {
        float desiredSeparation = UnitRadius * 7f;
        Vector2 sum = new Vector2();
        int count = 0;
        foreach(BoidAgent agent in Agents)
        {
            if(agent != null)
            {
                float d = Vector2.Distance(transform.position, agent.transform.position);
                if ((d > 0) && (d < desiredSeparation))
                {
                    Vector2 diff = agent.transform.position - transform.position;
                    diff.Normalize();
                    diff /= d;
                    sum += diff;
                    count++;
                }
            }
        }
        if(count > 0)
        {
            sum /= count;
            sum *= -1;
            sum.Normalize();
            sum *= maxSpeed;
            steering = sum - velocity;
            return steering;
        }
        else
        {
            return Vector2.zero;
        }

    }
    public Vector2 Allign(List<BoidAgent> Agents)
    {
        Vector2 sum = new Vector2();
        int count = 0;
        foreach (BoidAgent agent in Agents)
        {
            if(agent != null)
            {
                float d = Vector2.Distance(transform.position, agent.transform.position);
                if ((d > 0) && d < NeighbourRadius)
                {
                    sum += agent.velocity;
                    count++;
                }
            }
        }
        if (count > 0)
        {
            sum /= (float)count;
            sum.Normalize();
            sum *= maxSpeed / 3;
            steering = sum - velocity;
            return steering;
        }
        else
        {
            return Vector2.zero;
        }
    }
    public void GetRandomPosition()
    {
        futurePos.transform.position = PatrolPoint.transform.position + (Vector3)Random.insideUnitCircle * 30f;
    }
    public void DropPatrolPoint()
    {
        GameObject Point = Instantiate(new GameObject(), transform.position, Quaternion.identity);
        PatrolPoint = Point;
    }
    public void DestroyPatrolPoint()
    {
        if(PatrolPoint != transform.gameObject)
        {
            Destroy(PatrolPoint);
        }
        
    }
    // Misc Functions //
    public void GetAllNeighbours()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, NeighbourRadius);
        foreach(Collider2D col in colliders)
        {
            if (col.GetComponent<BoidAgent>()) { 

                NearbyUnits.Add(col.gameObject.GetComponent<BoidAgent>());
            }
        }
    }
}
