using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number_Popup : MonoBehaviour
{
    private float disappearTimer = 1f;
    private Color colour;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveYSpeed = 3f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if(disappearTimer <= 0){
            Destroy(gameObject);
        }
    }
}
