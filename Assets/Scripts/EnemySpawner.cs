using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public float SpawnRate = 5.0f;
    public GameObject[] EnemyPrefabs;
    float spawnTimer;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer += Time.deltaTime;
        
        if(spawnTimer >= SpawnRate)
        {
            int randomEnemy = Random.Range(0, EnemyPrefabs.Length);
            
            Instantiate(EnemyPrefabs[randomEnemy], transform.position, transform.rotation);
            spawnTimer = 0.0f;
        }

	}
}
