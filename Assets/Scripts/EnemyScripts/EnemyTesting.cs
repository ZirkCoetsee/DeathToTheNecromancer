using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//NB important to not scale enemy Sphear colliders

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyTesting : DamageableEntity
{
    [SerializeField] float enemyDamage = 1f;

    //Set state of enemy to avoid pathfinder setting destination during attack when we disable pathfinder and will give an error
    public enum State { idle, Chasing, Attacking};
    State currentState;


    NavMeshAgent pathfinder;
    Transform target;
    Player player;
    Material skinMaterial;

    Color originalColor;

    //Can make these public if we want to change them from level generator
    //Can play with this to create enemies that shoot by increasing their thershold and creating a type of enemy??
    float attackDistanceThreshold = 0.5f;
    float timeBetweenAttacks = 1f;

    float attackSpeed = 5f;

    float nextAttackTime;

    //Collision radius of player and enemy to not have enimy enter player position
    float myCollisionRadius;
    float targetCollisionRadius;

    // Start is called before the first frame update
    //NB OVERRIDE!!!!! or this start will override the DamageableEnity start and thus the DamageableEntity will never start haha
    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = FindObjectOfType<Player>();

        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = GetComponent<CapsuleCollider>().radius;

        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        //Expensive so we use the square route forms since we ont need the actual distance and are just comparing distances
        //Vector3.Distance();


        //Only check if player is within attack range when within attack time, attack speed basically

        if (Time.time > nextAttackTime)
        {
            //sqrMagnitude is the distance squared
            float squareDistanceToTarget = (target.position - transform.position).sqrMagnitude;
            // Added  + myCollisionRadius + targetCollisionRadius to take the distance from the ede of the enemy and edge of the player
            if (squareDistanceToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack());
            }
        }

    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        //Disable path finder when unit attacks to allow player to doge incomming attacks
        pathfinder.enabled = false;

        //Incode animation of moving enemy to simulate a lunge attack
        Vector3 originalPosition = transform.position;
        //Could spawn particle effect on attack position to show player attacking location and prompt player to move
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - directionToTarget * (myCollisionRadius);

        float percent = 0;

        skinMaterial.color = Color.red;

        while ( percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            //Moses interpolate between 0 and 1
            float interpolation  = (-Mathf.Pow(percent,2) + percent) *4;
            //this original is 0 and 1 is attack position, moving object smoothly
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }
        skinMaterial.color = originalColor;


        //Apply Target Take hit
        player.TakeDamage(enemyDamage);

        currentState = State.Chasing;
        pathfinder.enabled = true;
    }

    IEnumerator UpdatePath()
    {
        //Improved Update to update only 4x a second and not a shit tun
        float refreshRate = 0.25f;

        while ( target != null)
        {
            //Only find path if enemy is in chasing state
            if (currentState == State.Chasing)
            {
                Vector3 directionToTarget = (target.position - transform.position).normalized;
               //Added attackDistanceThreshold / 2 to have enemy positioned a bit further away from player then lunge
                Vector3 targetPosition = target.position - directionToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
                //NB check if enemy is dead before moving or it will break because the enemy has been destroyed thus no object to move
                if (!dead)
                {
                    pathfinder.SetDestination(target.position);

                }
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}
