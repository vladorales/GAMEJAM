using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	public float dmgAmnt = 1f;
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(DestroyBulletAfterTime(gameObject, 5.0f));
	}

	private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
	{
		yield return new WaitForSeconds(delay);

		Destroy(bullet);
	}
	// Update is called once per frame
	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{
            Debug.Log("hit player");
            float healthLoss;
            healthLoss = dmgAmnt;
			TwinStickMovement PlayerLife = other.gameObject.GetComponent<TwinStickMovement>();
            PlayerLife.LoseHealth(healthLoss);
			//Destroy(gameObject);
		}
		else if (other.gameObject.tag == "Wall")
		{
			
			Destroy(gameObject);
		}
		else if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Big Enemy")
		{
			Collider friend = other.gameObject.GetComponent<Collider>();

			Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), friend);
		}
	}
}
