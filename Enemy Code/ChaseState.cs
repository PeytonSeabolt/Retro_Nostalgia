using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyAI {
    EnemyStates enemy;
    
    public ChaseState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }
    public void UpdateActions()
    {
        Watch();
        Chase();
    }

    public void OnTriggerEnter(Collider Enemy)
    {

    }

    void Watch()
    {
        RaycastHit hit;
        if(Physics.Raycast(enemy.transform.position, enemy.vision.forward, out hit, enemy.patrolRange, enemy.raycastMask) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            enemy.lastKnownPosition = hit.transform.position;
        }
        else { ToAlertState(); }
    }

    void Chase()
    {
        Debug.Log(" Ima getcha lil boy!!!");
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false;
        // you're gonna get hit if this'n gets too close lil one
        if(enemy.navMeshAgent.remainingDistance <= enemy.attackRange && enemy.onlyMelee == true) //melee
        {
            enemy.navMeshAgent.isStopped = true;
            ToAttackState();
        } else if(enemy.navMeshAgent.remainingDistance <= enemy.shootRange && enemy.onlyMelee == false) //range
        {
            ToAttackState();
        }
    }


    public void ToPatrolState()
    {
        Debug.Log("I am Patrolling");
    }

    public void ToAttackState()
    {
        Debug.Log("Attack state!");
        enemy.currentState = enemy.attackState;
    }

    public void ToAlertState()
    {
        Debug.Log("I am Alert!");
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        Debug.Log("I am Chasing");
    }
}
