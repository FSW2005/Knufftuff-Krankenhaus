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
    private Transform drawLineto;

    //private float angle;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Movement();
    }
    private void Movement()
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
        rb.velocity = transform.forward * speed;

    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, drawLineto.position);
    }*/
}
