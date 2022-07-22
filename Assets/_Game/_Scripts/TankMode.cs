using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankMode : MonoBehaviour
{
    public Rigidbody TankAOE;
    public Transform barrel;
    public TwinStickMovement player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //add cooldown
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Pulse();
        }
    }
   void Pulse()
    {
        Debug.Log("Tank launch");
        Rigidbody clone;
        clone = Instantiate(TankAOE, barrel.transform.position, barrel.transform.rotation);

        clone.transform.position = barrel.transform.position;
        
    }
}
