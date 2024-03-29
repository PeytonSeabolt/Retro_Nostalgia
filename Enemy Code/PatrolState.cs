﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// inherits our enemy interface.
public class PatrolState : IEnemyAI {
    EnemyStates enemy;
    int nextWayPoint = 0;

    public PatrolState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }
    public void UpdateActions()
    {
        Watch();
        Patrol();

    }



    void Watch()
    {
        //raycasting
        RaycastHit hit;
        if(Physics.Raycast(enemy.transform.position, -enemy.transform.forward, out hit, enemy.patrolRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                //Debug.Log("Gotcha punk!");
                enemy.chaseTarget = hit.transform;
                ToChaseState();
            }
        }
    }


    void Patrol()
    {
        enemy.navMeshAgent.destination = enemy.waypoints[nextWayPoint].position;
        enemy.navMeshAgent.isStopped = false;
        if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) %enemy.waypoints.Length;
        }
    }


    public void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.CompareTag("Player"))
        {
            ToAlertState();
        }
    }

    public void ToPatrolState() {
        Debug.Log("I AM patrolling you idiot.");
            }

    public void ToAttackState() {
        enemy.currentState = enemy.attackState;
    }

    public void ToAlertState() {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState() {
        enemy.currentState = enemy.chaseState;
    }
}
