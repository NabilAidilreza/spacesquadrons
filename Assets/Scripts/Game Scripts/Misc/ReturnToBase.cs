using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToBase : MonoBehaviour
{
    private Transform BASE;
    public bool IsPlayer;
    private float speed = 8f;
    // Start is called before the first frame update
    void Start()
    {
        RTSBase[] RTSBases = GameObject.FindObjectsOfType<RTSBase>();
        foreach(RTSBase Base in RTSBases)
        {
            if(Base.GetIsPlayer() == true && IsPlayer == true)
            {
                BASE = Base.GetComponent<Transform>();
            }
            else if (Base.GetIsPlayer() == false && IsPlayer == false)
            {
                BASE = Base.GetComponent<Transform>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, BASE.position, speed * Time.deltaTime);
        Vector3 moveDir = BASE.position - transform.position;
        transform.up = moveDir;
        if(transform.position == BASE.position)
        {
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<RTSBase>())
        {
            Destroy(gameObject);
        }
    }
}
