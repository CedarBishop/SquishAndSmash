using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyStates { Idle, FollowPlayer, AttackPlayer, Squished, Immobile}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public EnemyStates enemyState;
    private Rigidbody rigidbody;
    private NavMeshAgent agent;
    private int distanceToReact = 5;
    PlayerController playerController;

    float timeToExpand = 10;
    float timeToRemobilise = 5;

    public float randomTargetRadius = 3;

    private float startingSpeed;

    Vector3 target;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        target = transform.position;
        startingSpeed = agent.speed;
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
        enemyState = EnemyStates.Squished;
        StartCoroutine("CoOnSquished");
        transform.localScale = new Vector3(1, 0.1f, 1);
        agent.speed = 0;
        agent.enabled = false;
        target = transform.position;
    }

    IEnumerator CoOnSquished ()
    {
        rigidbody.isKinematic = true;
        yield return new WaitForSeconds(timeToExpand);
        enemyState = EnemyStates.Immobile;
        transform.localScale = new Vector3(1,0.5f,1);
        yield return new WaitForSeconds(timeToRemobilise);
        enemyState = EnemyStates.Idle;
        agent.enabled = true;
        agent.speed = startingSpeed;
        rigidbody.isKinematic = false;

    }
}
