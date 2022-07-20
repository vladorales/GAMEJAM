using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public float EHealth = 1f;
	public ParticleSystem Explode;

	public List<Transform> items = new List<Transform> ();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       CheckHealth(); 
    }
    #region Taking Damage
	private void CheckHealth()
	{
		if (EHealth <= 0)
		{
			int randomNum = Random.Range(0,items.Count);
			Debug.Log(randomNum);
			//Instantiate(items[randomNum],transform.position,Quaternion.identity);
			Instantiate(Explode,transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
	public void Damage(float damage)
	{
		
		EHealth = EHealth - damage;
	}
	#endregion
}
