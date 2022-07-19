using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherMissile : MonoBehaviour
{
    public Transform currentEnemy;
    public ParticleSystem explode;
    private Rigidbody rb;
    public float speed = 5f;
    public float rotatespeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        Destroy(gameObject, 3f);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy")
        {
            currentEnemy = other.transform;
            transform.LookAt(currentEnemy);
            Vector3 direction = currentEnemy.transform.position - rb.transform.position;
            direction.Normalize();
            float rot = Vector3.Cross(direction, transform.forward).y;
            rb.velocity = transform.forward * speed;
            rb.angularVelocity = transform.forward * rot * speed;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            //insert ref to enemy script, damage the enemy
            //explode.Play();
            Debug.Log("GOTTEM!");
        }
    }
}
