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
            actualEnemiesInstantiated.Add(Instantiate(enemyToSpawn, this.transform.position,this.transform.rotation));
        }
        for (int i = 0; i < actualEnemiesInstantiated.Count - 1; i++)
        {
            if (!actualEnemiesInstantiated[i].activeSelf)
            {
                actualEnemiesInstantiated.RemoveAt(i);
            }
        }
    }
}
