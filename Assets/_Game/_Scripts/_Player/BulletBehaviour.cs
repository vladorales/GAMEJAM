using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
	public float dmgAmnt = 1f;
    // Start is called before the first frame update
    void Update()
    {
        Physics.IgnoreLayerCollision(3,3);
    }

	// Update is called once per frame
	 void OnCollisionEnter(Collision collision)
	{
		
		if (collision.gameObject.tag == "Enemy")
		{
			
			EnemyLife enemyScript = collision.gameObject.GetComponent<EnemyLife>();
			enemyScript.Damage(dmgAmnt);
			Destroy(gameObject);
		}
		else
		{
			
			Destroy(gameObject);
		}
			
		}
}
