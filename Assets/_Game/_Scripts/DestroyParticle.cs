using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(EndofLife(5.0f));
    }

    IEnumerator EndofLife(float delay)
	{
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}
}
