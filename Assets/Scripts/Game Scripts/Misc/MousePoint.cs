using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour
{
    private List<GameObject> selectedObjects;
    public LayerMask WhatIsTarget;
    public void Start()
    {
        selectedObjects = new List<GameObject>();
        StartCoroutine(Die());
    }
    public void Update()
    {
        Collider2D[] CheckObjectsArray = Physics2D.OverlapCircleAll(this.transform.position, 5.0f, WhatIsTarget);
        foreach (Collider2D collider2D in CheckObjectsArray)
        {
            GameObject OBJECT = collider2D.GetComponent<GameObject>();
            selectedObjects.Add(OBJECT);
        }
        if (selectedObjects.Count != 0)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
