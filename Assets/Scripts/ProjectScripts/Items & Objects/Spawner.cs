using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Variables de Spawneo de enemigos")]
    [SerializeField] float timeBetweenSpawns;
    float originalTimeBetweenSpawns;
    float timer;
    [SerializeField] float randomDeviationBetweenSpawns;
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] float maxEnemiesInstantiated;
    List<GameObject> actualEnemiesInstantiated = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        originalTimeBetweenSpawns = timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timeBetweenSpawns < timer && maxEnemiesInstantiated > actualEnemiesInstantiated.Count)
        {
            timer = 0;
            timeBetweenSpawns = Random.Range(originalTimeBetweenSpawns - randomDeviationBetweenSpawns, originalTimeBetweenSpawns + randomDeviationBetweenSpawns);
            GameObject actualEnemy = Instantiate(enemyToSpawn, this.transform.position, this.transform.rotation);
            actualEnemiesInstantiated.Add(actualEnemy);
            if (actualEnemy.GetComponent<EnemyControl_MovementController>() != null)
            {
                ZoneManager.Instance.m_activeRoom.AddEnemyAtRoom(actualEnemy.GetComponent<EnemyControl_MovementController>());
                actualEnemy.GetComponent<EnemyControl_MovementController>().spawnerInstantiatedFrom = this;
            }
        }
    }
    public void RemoveEnemyFromSpawner(GameObject enemy)
    {
        actualEnemiesInstantiated.Remove(enemy);
    }
}
