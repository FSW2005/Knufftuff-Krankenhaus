using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] movementPoints;
    private Rigidbody rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool isWalingInCircles;
    private int currentPoint=0;
    private float plusOrMinus = 1;
    [SerializeField]
    private float rotationSpeed;

    private bool die=false;
    [SerializeField]
    private float timeTillDeath;
    private float timeTillDeathCounter=0;
  
    //Following the player
    [SerializeField]
    private float followPlayerFor,speedMulti;
    private float followPlayerTimer=0,speedMultiCounter;
    private bool isStunned;
    private Vector3 playerPos;

    //Going to the closest point after following the player
    private float currentBestDistance = 1000000000000000;
    private bool foundNearestPoint= false;

    //Animation
    [SerializeField]
    Animator animator;
    [SerializeField]
    RuntimeAnimatorController[] aniClips;
    /*
     idle
    walk
    sprint
    attack
     */


    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (die)
        {
            timeTillDeathCounter += Time.deltaTime;
            if (timeTillDeathCounter >= timeTillDeath)
            {
                SceneManager.LoadScene("Menue", LoadSceneMode.Single);
            }
        }
        else
        {

            isStunned = GameObject.FindGameObjectWithTag("FlashLight").GetComponent<Flashlight>().enemyStunned;
            if (isStunned)
            {
                rb.velocity = new Vector3(0, 0, 0);
                followPlayerTimer = 0;
                animator.runtimeAnimatorController = aniClips[0];
            }
            else
            {

                if (gameObject.GetComponent<EnemySight>().sawPlayer)
                {
                    foundNearestPoint = false;
                    followPlayerTimer = followPlayerFor;
                    playerPos = GetComponent<EnemySight>().hit.transform.gameObject.transform.position - transform.position;

                    //transform.LookAt(new Vector3(gameObject.GetComponent<EnemySight>().hit.transform.gameObject.transform.position.x, 0, gameObject.GetComponent<EnemySight>().hit.transform.gameObject.transform.position.z), transform.up);

                }
                else
                {
                    followPlayerTimer -= Time.deltaTime;
                }

                if (followPlayerTimer > 0)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(playerPos.x, 0, playerPos.z), Vector3.up);
                    speedMultiCounter = speedMulti;
                }
                else
                {
                    speedMultiCounter = 1;

                    if (!foundNearestPoint)
                    {
                        currentBestDistance = 1000000000000;
                        FindNearestPoint();
                    }
                    NormalMovement();
                }
                rb.velocity = transform.forward * speed * speedMultiCounter;

            }
        }
    }
    private void FindNearestPoint()
    {
        for (int i = 0; i <= movementPoints.Length-1; i++)
        {
            if(currentBestDistance > Vector3.Distance(transform.position, movementPoints[i].position))
            {
                currentBestDistance = Vector3.Distance(transform.position, movementPoints[i].position);
                currentPoint = i;
            }
        }
        
        foundNearestPoint = true;

    }
    private void NormalMovement()
    {
        if(followPlayerTimer > 0)
        {
            animator.runtimeAnimatorController = aniClips[2];
        }
        else
        {
        animator.runtimeAnimatorController = aniClips[1];
        }


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

        Vector3 relativePos = movementPoints[currentPoint].position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePos.x, 0, relativePos.z), Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 0; i < movementPoints.Length-1; i++)
        {
            if(i != movementPoints.Length - 1)
            {
                Gizmos.DrawLine(movementPoints[i].position, movementPoints[i + 1].position);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.runtimeAnimatorController = aniClips[3];
            die = true;
        }
    }
}
