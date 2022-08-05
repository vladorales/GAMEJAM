using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBot : MonoBehaviour
{
    private LineRenderer lr;
    private Transform player;
    public Transform startpos;
    public Transform origin;
    public Rigidbody projectile;
    public bool isFiring;
    private float fireDelay = 1f;
    private float lastFireTime = 1f;
    public bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Awake()
    {
        player = GameObject.Find("Player Variant").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //isInRange();
    }
    void StartShootLaser()
    {
      
    }
    private void OnTriggerEnter(Collider other)
    {
        isFiring = true;
        StartCoroutine(FollowPlayer());
        Laser();
        
        if (isFiring == true)
        {
            if (Time.time >= lastFireTime)
            {
                lastFireTime = Time.time + 1f / fireDelay;
                
                isFiring = false;
            }
        }
        if(isFiring == false)
        {
            lr.SetPosition(1, origin.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "Player")
        {
   
            inRange = false;
            isFiring = false;
            //set laser to center
            lr.SetPosition(1, origin.position);
        }
    }
    IEnumerator FollowPlayer()
    {
        Debug.Log("following");
        transform.LookAt(player);
        yield return new WaitForSeconds(0);
       
        
    }
    void Laser()
    {
      //makes laser go
        lr.SetPosition(0, startpos.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            isFiring = true;
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
            if (hit.transform.tag == "Player")
            {

                Debug.Log("hit");
                lr.startColor = Color.red;
            }
            else
            //keep going
            {
                lr.SetPosition(1, transform.forward * 500);
            }
        }
        StartCoroutine(ResetFiring());
    }

    IEnumerator ResetFiring()
    {
        yield return new WaitForSeconds(3);
       
    }

}

