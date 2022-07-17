using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripShot : MonoBehaviour
{
    public GameObject PowerUpParticle;
    
    public AudioClip sound;

    void Awake()
    {
         
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
        AudioSource.PlayClipAtPoint(sound,transform.position);
           GameObject particle = Instantiate(PowerUpParticle);
           particle.transform.position = gameObject.transform.position;
           S_Shoot bullet = other.gameObject.GetComponent<S_Shoot>();
           bullet.tripShot = true; 
           Destroy(gameObject);
        }
        
    }
}
