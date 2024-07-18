using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 
public class ZombieMovementg : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;
    [SerializeField] float turnSpeed = 10f;
    Animator animator;
    float distanceToTarget;
    float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTarget();
        ChaseTarget();
        distanceToTarget = Vector3.Distance(transform.position, target.position);
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
        animator.SetBool("isMoving", true);
        navMeshAgent.SetDestination(target.position);
       
    }
    void AttackTarget()
    {
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", true);
        }
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            health -= 50;
            Debug.Log("Hit!");
            if(health <= 0)
            {
                Debug.Log("hello");
                navMeshAgent.speed = 0;
                animator.enabled = false;
            }
        }
    }
}
