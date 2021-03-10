using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private EnemyController enemyController;
    

    public float health = 100f;

    public bool isPlayer, isMirror;

    private bool isDead;

    private PlayerStats playerStats;

    private int killCount;

    private void Awake()
    {
        if (isMirror)
        {
            enemyController = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
        }
        if (isPlayer)
        {
            playerStats = GetComponent<PlayerStats>();
        }
    }
    public void ApplyDamage(float damage)
    {
        if (isDead)
            return;

        health -= damage;

        if (isPlayer)
        {
            playerStats.DisplayHealthStats(health);
        }
        if (isMirror)
        {
            if(enemyController.enemystate == EnemyState.patrol)
            {
                enemyController.chaseDitance = 50f;
            }
        }

        if(health <= 0f)
        {
            PlayerDied();

            isDead = true;
        }
    }

    void PlayerDied()
    {
        if (isMirror)
        {
            GetComponent<SphereCollider>().isTrigger = false;
            enemyController.enabled = false;
            navAgent.enabled = false;

            EnemyManager.instance.EnemyDied(true);
            killCount++;
            //playerStats.DisplayKillerCount(killCount);
        }

        if (isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.Enemy);

            for(int i=0; i< enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            EnemyManager.instance.StopSpawning();

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Shoot>().enabled = false;
            GetComponent<WeaponChoice>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }

        if(tag == Tags.PlayerTag)
        {
            Invoke("RestartGame", 3f);
        } else
        {
            Invoke("TurnOffGameObject", 3f);
        }
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
    }


    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
}
