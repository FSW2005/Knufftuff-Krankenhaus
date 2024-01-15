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
    private float followPlayerFor;
    private float followPlayerTimer=0;
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
            followPlayerTimer = followPlayerFor;
            transform.LookAt(new Vector3(gameObject.GetComponent<EnemySight>().hit.transform.gameObject.transform.position.x, 0, gameObject.GetComponent<EnemySight>().hit.transform.gameObject.transform.position.z), transform.up);

        }
        else
        {
            followPlayerTimer -= Time.deltaTime;
        }
        if(!(followPlayerTimer > 0))
        {
            NormalMovement();
        }


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

        transform.LookAt(new Vector3(movementPoints[currentPoint].position.x,0,movementPoints[currentPoint].position.z), transform.up);

    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, drawLineto.position);
    }*/
}
