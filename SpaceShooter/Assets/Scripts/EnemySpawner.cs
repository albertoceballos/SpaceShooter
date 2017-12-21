using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject[] enemies;
    private float SpawnTimer = 0;
    private int weakCount = 0;
    private int heavyCount = 0;
    public float Delay = 1.0f;
	
	// Update is called once per frame
	void Update () {
        if (!ButtonController.PausedGame)
        {
            SpawnTimer -= Time.deltaTime;
            if (SpawnTimer <= 0)
            {
                weakCount++;
                heavyCount++;
                SpawnTimer = Delay;
                int randomBetween1nad2 = Random.Range(0, 2);
                Instantiate(enemies[randomBetween1nad2], transform.position, Quaternion.Euler(0, 0, 180));
                if (weakCount == 20)
                {
                    weakCount = 0;
                    Instantiate(enemies[2], transform.position, Quaternion.Euler(0, 0, 180));
                    if (heavyCount == 50 && !Enemy.isBossAround)
                    {
                        heavyCount = 0;
                        Instantiate(enemies[3], transform.position, Quaternion.Euler(0, 0, 180));
                    }
                }
            }
        }
        
	}
}
