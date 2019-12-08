using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We are using an interface.  Not a class.  
public interface IEnemyAI{

    // Defining functions.  We will make scripts that utilize these functions.  Still haven't had a poptart.  

    void UpdateActions();

    void OnTriggerEnter(Collider Enemy);

    void ToPatrolState();

    void ToAttackState();

    void ToAlertState();

    void ToChaseState();



}
