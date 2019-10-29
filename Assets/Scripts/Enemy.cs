using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyStates { Idle, FollowPlayer, AttackPlayer, Squished, Immobile}

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public EnemyStates enemyState;

    private NavMeshAgent agent;
    private int distanceToReact = 5;
    PlayerController playerController;

    float timeToExpand = 10;
    float timeToRemobilise = 5;

    public float randomTargetRadius = 3;

    Vector3 target;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        target = transform.position;
    }

    void Update()
    {
        switch (enemyState)
        {
            case EnemyStates.Idle:
                IdleState();
                break;
            case EnemyStates.FollowPlayer:
                FollowPlayerState();
                break;
            case EnemyStates.AttackPlayer:
                AttackPlayerState();
                break;
            case EnemyStates.Squished:
                SquishedState();
                break;
            case EnemyStates.Immobile:
                ImmobileState();
                break;
            default:
                break;
        }
    }

    bool FindPlayer()
    {
        if (GetDistanceToPlayer() < distanceToReact)
        {
            return true;
        }
        return false;
    }

    float GetDistanceToPlayer ()
    {
        return Vector3.Distance(transform.position, playerController.transform.position);
    }

    void IdleState()
    {
        if (FindPlayer())
        {
            enemyState = EnemyStates.FollowPlayer;
        }
        else
        {
            SetTarget();
            agent.SetDestination(target);
        }
    }

    void SetTarget ()
    {
        if (Vector3.Distance(transform.position, target) < 1 || agent.speed < 1 )
        {
            target = new Vector3(Random.Range(transform.position.x - randomTargetRadius, transform.position.x + randomTargetRadius), transform.position.y, Random.Range(transform.position.z - randomTargetRadius, transform.position.z + randomTargetRadius));
        }
    }

    void FollowPlayerState()
    {
        if (FindPlayer())
        {
            agent.SetDestination(playerController.transform.position);
        }
        else
        {
            enemyState = EnemyStates.Idle;
        }
    }

    void AttackPlayerState ()
    {

    }

    void SquishedState ()
    {

    }

    void ImmobileState()
    {

    }

    public void OnSquished ()
    {
        StartCoroutine("CoOnSquished");
    }

    IEnumerator CoOnSquished ()
    {
        enemyState = EnemyStates.Squished;
        yield return new WaitForSeconds(timeToExpand);
        enemyState = EnemyStates.Immobile;
        yield return new WaitForSeconds(timeToRemobilise);
        enemyState = EnemyStates.Idle;
    }
}
