using UnityEngine;
using System.Collections;
using UnityEngine.AI;
//other enemy scripts will inherit the values of this script.
public class Enemy : MonoBehaviour
{
    public Sprite deadmfer;
    public int maxHealth;
    float health;

    EnemyStates es;  // **** turning this off on death
    NavMeshAgent nma; // **** turning this off on death
    SpriteRenderer sr; // all 4 of these objects get values from components attached to the enemy.
    BoxCollider bc; //to resize the box collider on death
    Animator an;

    public void Start()
    {
       
        health = maxHealth;
        es = GetComponent<EnemyStates>();
        nma = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider>();
        an = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0) //death.  
        {
            an.enabled = false;
            es.enabled = false; //they no longer move.  Death consumes us all eventually.  
            nma.enabled = false;
            sr.sprite = deadmfer;
            
            

        }
        
    }

    void AddDamage(float damage)
    {
        health -= damage;

    }
}

