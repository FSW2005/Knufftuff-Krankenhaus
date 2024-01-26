using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Marulk2 : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private bool isWalingInCircles;
    private int currentPoint = 0;
    private float plusOrMinus = 1;

    //following player
    [SerializeField] private float followPlayerFor, stalkingSpeed;
    private float followPlayerTimer = 0, defaultSpeed;
    public bool sawPlayer = false;
    [SerializeField] private bool isStunned;

    //Going to the closest point after following the player
    private float currentBestDistance;
    private bool foundNearestPoint = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        defaultSpeed = gameObject.GetComponent<NavMeshAgent>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStunned)
        {
            agent.ResetPath();
            followPlayerTimer = 0;
        }
        else
        {
            //When the player is seen
            if (sawPlayer)
            {
                foundNearestPoint = false;
                followPlayerTimer = followPlayerFor;
                sawPlayer = false;
            }
            else
            {
                followPlayerTimer -= Time.deltaTime;
            }


            if (followPlayerTimer > 0)
            {
                agent.speed = stalkingSpeed;
                agent.SetDestination(target.position);
            }
            else
            {
                agent.speed = defaultSpeed;
                if (!foundNearestPoint)
                {
                    currentBestDistance = 1000000000000;
                    FindNearestPoint();
                }
                NormalMovement();
            }
        }
    }
    private void FindNearestPoint()
    {
        for (int i = 0; i <= movementPoints.Length - 1; i++)
        {
            if (currentBestDistance > Vector3.Distance(transform.position, movementPoints[i].position))
            {
                currentBestDistance = Vector3.Distance(transform.position, movementPoints[i].position);
                currentPoint = i;
            }
        }

        foundNearestPoint = true;

    }
    private void NormalMovement()
    {
        if (Mathf.Round(transform.position.x) == Mathf.Round(movementPoints[currentPoint].position.x) && Mathf.Round(transform.position.z) == Mathf.Round(movementPoints[currentPoint].position.z))
        {
            if (isWalingInCircles)
            {
                if (currentPoint >= movementPoints.Length - 1)
                {
                    currentPoint = 0;
                }
                else
                {
                    currentPoint += 1;
                }

            }
            else
            {
                if (currentPoint >= movementPoints.Length - 1 || currentPoint <= 0f && plusOrMinus < 0)
                {
                    plusOrMinus = plusOrMinus * (-1f);
                }

                currentPoint = (int)((float)currentPoint + plusOrMinus);
            }

        }
        agent.SetDestination(movementPoints[currentPoint].position);
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < movementPoints.Length - 1; i++)
        {
            if (i != movementPoints.Length - 1)
            {
                Gizmos.DrawLine(movementPoints[i].position, movementPoints[i + 1].position);
            }
        }
    }
}