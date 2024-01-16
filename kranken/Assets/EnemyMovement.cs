using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
  
    //Following the player
    [SerializeField]
    private float followPlayerFor;
    private float followPlayerTimer=0;

    //Going to the closest point after following the player
    private float currentBestDistance = 1000000000000000;
    private bool foundNearestPoint= false;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
    }
    

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;

        if (gameObject.GetComponent<EnemySight>().sawPlayer)
        {
            foundNearestPoint = false;
            followPlayerTimer = followPlayerFor;
            Vector3 relativePos = GetComponent<EnemySight>().hit.transform.gameObject.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(new Vector3(relativePos.x,0,relativePos.z), Vector3.up);
            //transform.LookAt(new Vector3(gameObject.GetComponent<EnemySight>().hit.transform.gameObject.transform.position.x, 0, gameObject.GetComponent<EnemySight>().hit.transform.gameObject.transform.position.z), transform.up);

        }
        else
        {
            followPlayerTimer -= Time.deltaTime;
        }
        if(!(followPlayerTimer > 0))
        {
            if (!foundNearestPoint)
            {
            //FindNearestPoint();
            }

            NormalMovement();
        }


    }
    private void FindNearestPoint()
    {
        for (int i = 0; i <= movementPoints.Length-1; i++)
        {
            if(currentBestDistance > Vector3.Distance(movementPoints[i].position, transform.position))
            {
                currentBestDistance = Vector3.Distance(movementPoints[i].position, transform.position);
                currentPoint = i;
                print(currentPoint);
                print(currentBestDistance);
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

        Vector3 relativePos = movementPoints[currentPoint].position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(relativePos.x, 0, relativePos.z), Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, drawLineto.position);
    }*/
}
