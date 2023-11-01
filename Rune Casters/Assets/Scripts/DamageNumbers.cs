using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageNumbers : MonoBehaviour
{
    public GameObject parent;
    private Transform p;
    public float yIncrease = 0f;
    void Start()
    {
        StartCoroutine(DestroyAfter());
        p = parent.GetComponent<Transform>(); 
    }

    private void Update()
    {
        Transform t = GetComponent<Transform>();
        
        if (t != null)
        {
            t.position = p.position + new Vector3(0, yIncrease);
        }

        yIncrease += 0.005f;
    }

    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(.75f);
        Destroy(gameObject);
    }
}
