using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
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


	
	public void Shoot()
	{
		//MuzzleFlash.Play();
		GameObject bullet = Instantiate(BulletPrefab);
		//Physics.IgnoreCollision(bullet.GetComponent<Collider>(),
		//	bulletSpawn.parent.GetComponent<Collider>());
		bullet.transform.position = bulletSpawn.transform.position;
		Vector3 rotation = bullet.transform.rotation.eulerAngles;
		bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
		bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * bulletSpeed, ForceMode.Impulse);
		


	}
	
	
}
