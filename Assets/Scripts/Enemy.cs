using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates { Idle, FollowPlayer, AttackPlayer, Squished, Immobile}
public class Enemy : MonoBehaviour
{
    public EnemyStates enemyState;
    private int distanceToReact = 5;
    PlayerController playerController;

    float timeToExpand = 10;
    float timeToRemobilise = 5;


    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
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
        if (Vector3.Distance(transform.position, playerController.transform.position) < distanceToReact)
        {
            return true;
        }
        return false;
    }

    void IdleState()
    {
        if (FindPlayer())
        {

        }
        else
        {
            enemyState = EnemyStates.FollowPlayer;
        }
    }

    void FollowPlayerState()
    {

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
