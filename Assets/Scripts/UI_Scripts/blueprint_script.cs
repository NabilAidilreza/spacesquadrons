using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueprint_script : MonoBehaviour
{
    Vector3 movePoint;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        movePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movePoint.z = 0;
        transform.position = movePoint;
    }

    // Update is called once per frame
    void Update()
    {
        movePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movePoint.z = 0;
        transform.position = movePoint;
        if (Input.GetMouseButton(0))
        {
            // CheckCollision
            Collider2D COLLIDER = Physics2D.OverlapCircle(transform.position, 3f);
            if(COLLIDER == null)
            {
                Instantiate(prefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            //
        }
    }
}
