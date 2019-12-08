using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

//regular monobehavior

public class EnemyStates : MonoBehaviour {

    public Transform[] waypoints;
    public int patrolRange;
    public int shootRange;
    public int attackRange;
    public Transform vision;

    //for the ranged enemies :)
    public GameObject missile;
    public float missileDamage;
    public float missileSpeed;
    public float stayAlertTime;
    // can they shoot
    public bool onlyMelee = false;
    public float meleeDamage;
    //delay their attacks
    public float attackDelay;

    [HideInInspector]
    public AlertState alertState;
    [HideInInspector]
    public AttackState attackState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public PatrolState patrolState;
    [HideInInspector]
    public IEnemyAI currentState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Transform chaseTarget;
    [HideInInspector]
    public Vector3 lastKnownPosition;

    public LayerMask raycastMask;


    private void Awake()
    {
        //this object
        alertState = new AlertState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this);
        patrolState = new PatrolState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Use this for initialization
    void Start () {
        //we want the enemy to patrol.  
        currentState = patrolState;
	}
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateActions();
	}
    private void OnTriggerEnter(Collider otherObj)
    {
        currentState.OnTriggerEnter(otherObj);
    }

    void HiddenShot(Vector3 shotPostion)
    {
        Debug.Log("Someone Shot!");
        lastKnownPosition = shotPostion;
        currentState = alertState;
    }
}
