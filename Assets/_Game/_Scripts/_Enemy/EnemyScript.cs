using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
	[Header("AgentStats")]
	public Transform target;
	public NavMeshAgent agent;

	[Header("Shoot")]
	public ShootPlayer shtPlayer;
	bool agentStopped;
	public float fireRate = 1f;
	public float fasterFireRate = 1f;
	private float NextTimetoFire = 1f;

	



	// Start is called before the first frame update
	void Start()
    {
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent>();
		shtPlayer = GetComponent<ShootPlayer>();
    }

    // Update is called once per frame
    void Update()
	{
		
		TowardsEnemy();
		CheckMovement();
		ShootPlayer();

	}

	private void CheckMovement()
	{
		float distance = Vector3.Distance(target.position, transform.position);


		if (distance <= agent.stoppingDistance)
		{
			agentStopped = true;

		}
		if (distance > agent.stoppingDistance && agentStopped == true)
		{
			StartCoroutine(ResetMovement(5.0f));
		}
	}


	#region Attacking Player
	private void TowardsEnemy()
	{
		float distance = Vector3.Distance(target.position, transform.position);

		if (distance > agent.stoppingDistance && agentStopped == false)
		{
			agent.SetDestination(target.position);
			
		}
		
		if (distance <= agent.stoppingDistance || distance >= agent.stoppingDistance)
		{
			FaceTarget();
			
		}
		if (distance <= agent.stoppingDistance && agentStopped == true)
		{
			agent.SetDestination(agent.gameObject.transform.position);
	
		}
	}

	
	private void ShootPlayer()
	{
		if (Time.time >= NextTimetoFire && agentStopped == false)
		{
			NextTimetoFire = Time.time + 1f / fireRate;
			shtPlayer.Shoot();

		}
		if (Time.time >= NextTimetoFire && agentStopped == true)
		{
			NextTimetoFire = Time.time + 1f / fasterFireRate;
			shtPlayer.Shoot();

		}
	}
	

	private void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}


	

	private IEnumerator ResetMovement(float delay)
	{
		yield return new WaitForSeconds(delay);

		agentStopped = false;
	}
	#endregion

	
}
