using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_Shoot : MonoBehaviour
{
	[Header("Bullet Stats")]
	public float bulletSpeed = 30f;
	public float lifeTime = 3;
	public bool tripShot = false;
	

	[Header("Gun and Bullet")]
	public GameObject BulletPrefab;
	public Transform bulletSpawn;
	public Transform secSpawn;
	public Transform tertSpawn;

	public AudioSource clip;
	public AudioSource heavyClip;

    public float fireRate = 1f;
    private float NextTimetoFire = 1f;

    // Update is called once per frame
    void Update()
	{
        //update bullet count
        //
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartShoot();
        }
    }

    private void StartShoot()
    {
        if (Time.time >= NextTimetoFire)
        {
            NextTimetoFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
	{
		yield return new WaitForSeconds(delay);

		Destroy(bullet);
	}
	public void Shoot()
    {
		if (tripShot == false)
		{
			SingleShot();
		}


		else if (tripShot == true)
		{
        WideShot();
		}


    }

	private void SingleShot()
	{
		GameObject bullet = Instantiate(BulletPrefab);
        bullet.transform.position = bulletSpawn.transform.position;
        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * bulletSpeed, ForceMode.Impulse);
		clip.Play();
		StartCoroutine(DestroyBulletAfterTime(bullet, lifeTime));

	}
    private void WideShot()
    {
        GameObject bullet = Instantiate(BulletPrefab);		
        bullet.transform.position = bulletSpawn.transform.position;
        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * bulletSpeed, ForceMode.Impulse);

		GameObject bullet1 = Instantiate(BulletPrefab);
        bullet1.transform.position = secSpawn.transform.position;
        Vector3 rotation1 = bullet1.transform.rotation.eulerAngles;
        bullet1.transform.rotation = Quaternion.Euler(rotation1.x, transform.eulerAngles.y, rotation1.z);
        bullet1.GetComponent<Rigidbody>().AddForce(secSpawn.transform.forward * bulletSpeed, ForceMode.Impulse);

		GameObject bullet2 = Instantiate(BulletPrefab);
        bullet2.transform.position = tertSpawn.transform.position;
        Vector3 rotation2 = bullet2.transform.rotation.eulerAngles;
        bullet2.transform.rotation = Quaternion.Euler(rotation2.x, transform.eulerAngles.y, rotation2.z);
        bullet2.GetComponent<Rigidbody>().AddForce(tertSpawn.transform.forward * bulletSpeed, ForceMode.Impulse);
		
		heavyClip.Play();

        StartCoroutine(DestroyBulletAfterTime(bullet, lifeTime));
		StartCoroutine(DestroyBulletAfterTime(bullet1, lifeTime));
		StartCoroutine(DestroyBulletAfterTime(bullet2, lifeTime));
    }
}
