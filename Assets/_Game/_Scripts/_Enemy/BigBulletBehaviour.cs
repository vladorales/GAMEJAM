using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBulletBehaviour : MonoBehaviour
{
	Transform target;
	[SerializeField] float speed = 2.0f;
	// Start is called before the first frame update
	private void Awake()
	{
		target = PlayerManager.instance.player.transform;
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);

    }
}
