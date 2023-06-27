using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRangeGizmo : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 20f);
    }
}
