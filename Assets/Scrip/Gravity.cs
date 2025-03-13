using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.PlayerLoop;
using FixedUpdate = Unity.VisualScripting.FixedUpdate;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    private Rigidbody rb;

    private const float G = 0.00667f;
    public static List<Gravity> gravityObjectList;
    //orbit
    [SerializeField] bool planets = false;
    [SerializeField] private int orbitSpeed = 1000;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (gravityObjectList == null)
        {
            gravityObjectList = new List<Gravity>();
        }
        
        gravityObjectList.Add(this);
        
        //orbiting
        if (!planets)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
    }

    private void FixedUpdate()
    {
        foreach (var obj in gravityObjectList)
        {
            //call Attract
            if (obj != this)
            Attract(obj);

        }
        
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;
        
        float forceMagnitude = G * (rb.mass * otherRb.mass/Mathf.Pow(distance, 2) );
        Vector3 gavityForce = forceMagnitude * direction.normalized;
        
        otherRb.AddForce(gavityForce);
        
    }
}
