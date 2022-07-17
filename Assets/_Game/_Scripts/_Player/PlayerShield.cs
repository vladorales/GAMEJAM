using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PowerUpParticle;
    public AudioClip sound;
    

    void Awake()
    {
         
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TwinStickMovement PlayerLife = other.gameObject.GetComponent<TwinStickMovement>();
            AudioSource.PlayClipAtPoint(sound,transform.position);
           if(PlayerLife.playerHealth < 2)
           {
           GameObject particle = Instantiate(PowerUpParticle);
           particle.transform.position = gameObject.transform.position;
           PlayerLife.playerHealth += 1; 
           Destroy(gameObject);
           }
           else
           {
               Destroy(gameObject);
           }
        }
        
    }
}
