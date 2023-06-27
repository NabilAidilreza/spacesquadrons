using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100;
    public Transform laserFirePoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;
    private void Awake()
    {
        m_transform = GetComponent<Transform>();
       
    }
    private void Update()
    {
        ShootLaser();
    }
    void ShootLaser()
    {
        if (Physics2D.Raycast(m_transform.position, transform.up))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.up);
            Draw2Ray(laserFirePoint.position,_hit.point);
        }
        else
        {
            Draw2Ray(laserFirePoint.position, laserFirePoint.transform.up * defDistanceRay);
        }
    }
    void Draw2Ray(Vector2 startPosPoint,Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPosPoint);
        m_lineRenderer.SetPosition(1, endPos);
    }
}
