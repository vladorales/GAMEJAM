using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rammer : MonoBehaviour
{
    public TwinStickMovement player;
    [Header("AgentStats")]
    public Transform target;
    public NavMeshAgent agent;
    public float ramdistance = 20f;

    [Header("Ram")]
   
    bool agentStopped;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= ramdistance)
        {
            BeginRam();
        }
    }
    private void BeginRam()
    {
        transform.LookAt(target);
        float speed = 25f;
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    private void OnCollisionEnter(Collision collision)
    {
if(collision.collider.tag == "Player")
        {
            player.playerHealth = player.playerHealth - 1;
            player.playerSpeed = player.playerSpeed - 2;
            //particle
            Destroy(gameObject, 0.5f);
        }
    }
    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
    private IEnumerator ResetMovement(float delay)
    {
        yield return new WaitForSeconds(delay);

        agentStopped = false;
    }
}
