using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING,WAITING, COUNTING};
    [System.Serializable]
    public class Wave 
    {
        public string name;
        //public Transform enemy;
        public List<Transform> enemy = new List<Transform> ();
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;

    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        
        if (state == SpawnState.WAITING)
        {
            //Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                //Begin a new round
                WaveCompleted(waves);
            }
            else
            {
                return;
            }
        }
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -=Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
            return false;
            }
        }
         

        return true;

        
    }

    void WaveCompleted(Wave[] _wave)
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            for (int i = 0; i < _wave.Length; i++)
            {
                _wave[i].count += 1;
            }
            
            Debug.Log ("ALL WAVES COMPLETE! looping");
        }

        nextWave++;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave" + _wave.name);
        state = SpawnState.SPAWNING;
        //Spawn
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }
        state=SpawnState.WAITING;
        yield break;
    }
    void SpawnEnemy(List<Transform> _enemy)
    {
       // Instantiate(_enemy,transform.position, transform.rotation);
       int randomNum = Random.Range(0,_enemy.Count);
       Instantiate(_enemy[randomNum],transform.position,Quaternion.identity);
         
       // Debug.Log("Spawning Enemy: " + _enemy.name);
    }
}
