﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyAI {
    EnemyStates enemy;
    float clock = 0;
    public AlertState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }
    public void UpdateActions()
    {
        Search();
        Watch();
        if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
            Scan();
    }

    public void Search()
    {
        enemy.navMeshAgent.destination = enemy.lastKnownPosition;
        enemy.navMeshAgent.isStopped = false;
    }

    public void Watch()
    {

        RaycastHit hit;
        if(Physics.Raycast(enemy.transform.position, enemy.vision.forward, out hit, enemy.patrolRange)
            && hit.collider.CompareTag("Player"))
                {
            enemy.chaseTarget = hit.transform;
            enemy.navMeshAgent.destination = hit.transform.position;
            ToChaseState();
                }
    }


    void Scan()
    {
        clock += Time.deltaTime;
        if(clock >= enemy.stayAlertTime)
        {
            clock = 0;
            ToPatrolState();
        }
    }



    public void OnTriggerEnter(Collider Enemy)
    {

    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void ToAttackState()
    {
        
    }

    public void ToAlertState()
    {
        Debug.Log("Im alert!");
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }
}
