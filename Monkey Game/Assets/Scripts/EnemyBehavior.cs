using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public GameObject player;
    public Animator animator;

    int m_CurrentWaypointIndex;
    public float range = 15.0f;

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if(InRange()) {
            navMeshAgent.SetDestination(player.transform.position); navMeshAgent.speed = 6;
            animator.SetBool("Attack", true);
            navMeshAgent.speed = 6;
            if (InAttackRange())
            {             
                //play attack animation
                //do attack stuff?
     
                //CAN RESET GAME HERE
                //TEST COLLISION HERE?
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        } else if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            animator.SetBool("Attack", false);
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        } else if (!InAttackRange())
        {
            navMeshAgent.speed = 4;
            animator.SetBool("Attack", false);
        }
    }

    bool InRange()
    {
        return Vector3.Distance(navMeshAgent.nextPosition, player.transform.position) <= range;
    }

    bool InAttackRange()
    {
        return Vector3.Distance(navMeshAgent.nextPosition, player.transform.position) <= 3;

    }
}
