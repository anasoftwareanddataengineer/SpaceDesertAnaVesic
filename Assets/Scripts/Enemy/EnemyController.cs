using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    patrol,
    chase,
    attack
}

public class EnemyController : MonoBehaviour
{

    private NavMeshAgent navAgent;
    private EnemyState enemyState;

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;

    public float chaseDitance = 7f;
    private float currentChaseDistance;
    public float attackDist = 1.8f;
    public float chaseAfterAttackDist = 2f;

    public float patrolRadiMin = 20f, patrolRadMax = 60f;
    public float patrolForThisATime = 15f;
    private float patrolTimer;

    public float waitBeforeAttack = 2f;
    private float attackTimer;

    private Transform target;
    public GameObject attackPoint;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag(Tags.PlayerTag).transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.patrol;

        patrolTimer = patrolForThisATime;

        attackTimer = waitBeforeAttack;

        currentChaseDistance = chaseDitance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.patrol)
        {
            Patrol();
        }

        if (enemyState == EnemyState.chase)
        {
            Chase();
        }

        if (enemyState == EnemyState.attack)
        {
            Attack();
        }
    }

    void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;

        patrolTimer += Time.deltaTime;

        if(patrolTimer > patrolForThisATime)
        {
            SetNewRandomDestination();
            patrolTimer = 0f;
        }

        //testing the distance between the player and the enemy
        if (Vector3.Distance(transform.position, target.position) <= chaseDitance)
        {
            enemyState = EnemyState.chase;
        }
    }

    void Chase()
    {
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;

        navAgent.SetDestination(target.position);

        //testing the distance between the player and the enemy
        if (Vector3.Distance(transform.position, target.position) <= attackDist)
        {
            enemyState = EnemyState.attack;

            if (chaseDitance != currentChaseDistance)
            {
                chaseDitance = currentChaseDistance;
            }
        }else if(Vector3.Distance(transform.position, target.position) > chaseDitance)
        {
            enemyState = EnemyState.patrol;
            patrolTimer = patrolForThisATime;

            if (chaseDitance != currentChaseDistance)
            {
                chaseDitance = currentChaseDistance;
            }
        }
    }

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attackTimer += Time.deltaTime;

        if(attackTimer > waitBeforeAttack)
        {
            attackTimer = 0f;
        }

        if(Vector3.Distance(transform.position, target.position) > attackDist + chaseAfterAttackDist)
        {
            enemyState = EnemyState.chase;
        }
    }

    void SetNewRandomDestination()
    {
        float randRadius = Random.Range(patrolRadiMin, patrolRadMax);

        Vector3 randDirection = Random.insideUnitSphere * randRadius;

        randDirection += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, randRadius, -1);

        navAgent.SetDestination(navHit.position);
    }

    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);

    }

    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }

    public EnemyState enemystate
    {
        get;
        set;
    }

}
