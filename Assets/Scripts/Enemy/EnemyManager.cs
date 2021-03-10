using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager instance;

    [SerializeField]
    private GameObject mirrorPrefab;

    public Transform[] mirrorSpawnPoints;

    [SerializeField]
    private int mirrorCount;

    private int initialMirrorCount;

    public float waitBeforeSpawningEnemies = 10f;

    public float killerCount = 0;

    void Awake()
    {
        makeInstance();
    }

    private void Start()
    {
        initialMirrorCount = mirrorCount;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }


    void makeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void SpawnEnemies()
    {
        int index = 0;

        for(int i = 0; i< mirrorCount; i++)
        {
            if(index >= mirrorSpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(mirrorPrefab, mirrorSpawnPoints[index].position, Quaternion.identity);

            index++;
        }

        mirrorCount = 0;
    }

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(waitBeforeSpawningEnemies);

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }

    public void EnemyDied(bool mirror)
    {
        if (mirror)
        {
            mirrorCount++;

            if(mirrorCount > initialMirrorCount)
            {
                mirrorCount = initialMirrorCount;
            }
        }
    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }
}
