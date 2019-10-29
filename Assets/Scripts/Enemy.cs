using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyStates { Idle, FollowPlayer, AttackPlayer, AttackCooldown, Squished, Immobile}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public EnemyStates enemyState;
    private Rigidbody rigidbody;
    private NavMeshAgent agent;
    private int distanceToReact = 10;
    PlayerController playerController;

    float timeToExpand = 10;
    float timeToRemobilise = 5;

    public float randomTargetRadius = 6;

    private float startingSpeed;

    Vector3 randomOrigin;

    Vector3 target;

    bool canMove;

    void Start()
    {
        canMove = true;
        Dialogue.DialogueEvent += (bool answer) => canMove = !answer;
        randomOrigin = transform.position;
        playerController = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();
        target = transform.position;
        startingSpeed = agent.speed;
    }

    private void OnDestroy()
    {
        Dialogue.DialogueEvent -= (bool answer) => canMove = !answer;
    }

    void Update()
    {
        if (canMove == false)
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
                case EnemyStates.AttackCooldown:
                    AttackCooldownState();
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

    void AttackCooldownState ()
    {
        target = transform.position;
        
    }

    IEnumerator CoAttackCooldown ()
    {
        agent.speed = 0;
        yield return new WaitForSeconds(5);
        enemyState = EnemyStates.Idle;
    }

    void SetTarget ()
    {
        if (Vector3.Distance(transform.position, target) < 1 || agent.speed < 1 )
        {
            target = new Vector3(Random.Range(randomOrigin.x - randomTargetRadius, randomOrigin.x + randomTargetRadius), transform.position.y, Random.Range(randomOrigin.z - randomTargetRadius, randomOrigin.z + randomTargetRadius));
        }
    }

    void FollowPlayerState()
    {
        if (FindPlayer())
        {
            if (GetDistanceToPlayer() < 1.5f)
            {
                enemyState = EnemyStates.AttackPlayer;
            }
            agent.SetDestination(playerController.transform.position);
        }
        else
        {
            enemyState = EnemyStates.Idle;
        }
    }

    void AttackPlayerState ()
    {
        if (GetDistanceToPlayer() < 2)
        {
            Attack();
        }
        else
        {
            enemyState = EnemyStates.Idle;        
        }
    }

    void Attack ()
    {

        // attack player

        enemyState = EnemyStates.AttackCooldown;
    }

    void HitPlayerEvent()
    {
        playerController.PlayerHit();
    }

    void SquishedState ()
    {

    }

    void ImmobileState()
    {

    }

    public void OnSquished ()
    {
        AudioManager.instance.Play("EnemySquish");
        enemyState = EnemyStates.Squished;
        StopAllCoroutines();
        StartCoroutine("CoOnSquished");
        transform.localScale = new Vector3(0.85f, 0.2f, 0.5f);
        agent.speed = 0;
        agent.enabled = false;
        target = transform.position;
        randomOrigin = transform.position;
        transform.position -= new Vector3(0, 0.1f, 0);
    }

    IEnumerator CoOnSquished ()
    {
        rigidbody.isKinematic = true;
        yield return new WaitForSeconds(timeToExpand);
        AudioManager.instance.Play("ZombieExpand");
        transform.position += new Vector3(0, 0.1f, 0);
        enemyState = EnemyStates.Immobile;
        transform.parent = null;
        transform.localScale = new Vector3(1,1,1);
        yield return new WaitForSeconds(timeToRemobilise);
        enemyState = EnemyStates.Idle;
        agent.enabled = true;
        agent.speed = startingSpeed;
        rigidbody.isKinematic = false;

    }
}
