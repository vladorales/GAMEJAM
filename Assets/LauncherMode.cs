using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LauncherMode : MonoBehaviour
{
    public Rigidbody projectile;
    public float projSpeed = 20f;
    public Transform barrel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Launch();
        }
    }

    public void Launch()
    {
        Debug.Log("Canon launch");
        Rigidbody clone;
        clone = Instantiate(projectile, barrel.transform.position, barrel.transform.rotation);

        clone.velocity = transform.TransformDirection(Vector3.forward * projSpeed);
    }
}
