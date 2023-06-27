using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielder : MonoBehaviour
{
    private float speed = 0f;
    private bool Status = true;
    private Animator ShielderAnim;
    void Start()
    {
        ShielderAnim = GetComponent<Animator>();
        ShielderAnim.SetBool("Active", false);
    }
    // Update is called once per frame
    void Update()
    {
        // Active
        if (Status == true)
        {
            ShielderAnim.SetBool("Active", true);
            transform.Rotate(0, 0, speed);
            if (speed > 50f)
            {
                speed -= Time.deltaTime * 2;
            }
            else
            {
                speed += Time.deltaTime * 2;
            }
        }
        else
        {
            transform.Rotate(0, 0, speed);
            if (speed <= 0f)
            {
                speed = 0f;
                ShielderAnim.SetBool("Active", false);
            }
            else
            {
                speed -= Time.deltaTime * 2;
            }
        }
    }
    public void SetStatus(bool C)
    {
        Status = C;
    }
}
