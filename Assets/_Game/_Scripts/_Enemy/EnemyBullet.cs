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
	void OnTriggerEnter(Collider collision)
	{

		if (collision.gameObject.tag == "Player")
		{
			TwinStickMovement PlayerLife = collision.gameObject.GetComponent<TwinStickMovement>();
			PlayerLife.playerHealth -= 1;
			Destroy(gameObject);
		}
		else if (collision.gameObject.tag == "Wall")
		{
			
			Destroy(gameObject);
		}
		else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Big Enemy")
		{
			Collider friend = collision.gameObject.GetComponent<Collider>();

			Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), friend);
		}
	}
}
