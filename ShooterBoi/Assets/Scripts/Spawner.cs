using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Enemy enemy;
    public Enemy enemyGreen;
    public Enemy enemyBlue;


    float spawnTime = 0.5f;
    float nextSpawnTime;

    float currentEnemy = 0;
    private void Update()
    {
        if(Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnTime;
            Enemy spawnEnemy = SpawnEnemy();

            Instantiate(spawnEnemy, RandomPosition(), Quaternion.identity);
            currentEnemy++;
        }
    }

    public Vector3 RandomPosition()
    {
        Vector3 position = new Vector3(Random.Range(0, 10), Random.Range(0, 10), 0f);
        return position;
    }

    Enemy SpawnEnemy()
    {
        if(currentEnemy % 20 == 1)
        {
            return enemyGreen;
        }else if(currentEnemy % 11 == 1)
        {
            return enemyBlue;
        }
        else
        {
            return enemy;
        }
    }

}
