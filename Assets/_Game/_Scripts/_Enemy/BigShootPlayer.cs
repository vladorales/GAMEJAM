using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShootPlayer : MonoBehaviour
{
	// Start is called before the first frame update
	[Header("BulletLifetime")]
	public float damage = 10f;
	public float range = 100f;
	public float impactForce = 30f;
	public float bulletSpeed = 30f;
	public float lifeTime = 3;
	public float nextTimetoFire = 1f;

	[Header("BulletStats")]
	public GameObject BulletPrefab;
	public Transform bulletSpawn;

	[Header("Effects")]
	public ParticleSystem MuzzleFlash;

	// Update is called once per frame
	void Update()
	{


	}


	private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
	{
		yield return new WaitForSeconds(delay);

		Destroy(bullet);
	}
	public void Shoot()
	{
		Instantiate(BulletPrefab,bulletSpawn);
		StartCoroutine(DestroyBulletAfterTime(BulletPrefab,lifeTime));


	}
}
