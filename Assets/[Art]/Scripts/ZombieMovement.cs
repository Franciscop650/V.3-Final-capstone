using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 
public class ZombieMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;
    [SerializeField] float turnSpeed = 10f;
    Animator animator;
    float distanceToTarget = Mathf.Infinity;
    float health = 200;
    [SerializeField] float range = 10f;
    [Space, SerializeField] private AudioSource audioSourceAttack;
    [Space, SerializeField] private AudioSource audioSourceScream;
    [SerializeField] private float screamDelay = 0.2f;
    private float lastAttack;
    private int screamCounter;
    public Health playerHealth;
    private float damage = 30f;
    [SerializeField] private float attackDelay = 0.2f;
    private float attack;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        screamCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ChaseTarget();
        AttackTarget();
    }

    void LookAtTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void ChaseTarget()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= range)
        {
            if(screamCounter == 0)
            {
                Scream();
                screamCounter++;
            }
            range = 100;
            LookAtTarget();
            navMeshAgent.speed = 2;
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            animator.SetBool("isMoving", false);
            navMeshAgent.speed = 0;
        }
       
    }
    public void AttackTarget()
    {
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true);
            AttackAudio();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            health -= 75;
            Debug.Log("Hit!");
            if(health <= 0)
            {
                navMeshAgent.speed = 0;
                animator.enabled = false;
                StartCoroutine(DelayDeath());
            }
        }
        if (collision.gameObject.tag == "Bullet")
        {
            health -= 50;
            Debug.Log("Hit!");
            if (health <= 0)
            {
                navMeshAgent.speed = 0;
                animator.enabled = false;
                StartCoroutine(DelayDeath());
            }
        }
        if (collision.gameObject.tag == "CircularBullet")
        {
            health -= 20;
            Debug.Log("Hit!");
            if (health <= 0)
            {
                navMeshAgent.speed = 0;
                animator.enabled = false;
                StartCoroutine(DelayDeath());
            }
        }
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    private void Scream()
    {
        audioSourceScream.Play();
    }
    private void AttackAudio()
    {
        if (lastAttack > Time.time) return;

        lastAttack = Time.time + screamDelay;
        audioSourceAttack.Play();
        playerHealth.takeDamage(damage);
    }
    private void AttackDelay()
    {
        if (attack > Time.time) return;

        attack = Time.time + attackDelay;
        playerHealth.takeDamage(damage);
    }
}
