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

    //private float angle;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Round(transform.position.x) == Mathf.Round(movementPoints[currentPoint].position.x) && Mathf.Round(transform.position.z) == Mathf.Round(movementPoints[currentPoint].position.z))
        {
            if(currentPoint >= movementPoints.Length - 1)
            {
                if (isWalingInCircles)
                {
                    currentPoint = 0;
                }
                else
                {

                }
                currentPoint = 0;
            }
            else
            {
                currentPoint += 1;
            }
        }

        transform.LookAt(new Vector3(movementPoints[currentPoint].position.x,0,movementPoints[currentPoint].position.z),transform.up);
        rb.velocity = transform.forward * speed;
        /* angle = Mathf.Atan2(movementPoints[currentPoint].position.z - transform.position.z, movementPoints[currentPoint].position.x - transform.position.x) * Mathf.Rad2Deg;
       transform.rotation = Quaternion.Euler(Vector3.Slerp(transform.position, new Vector3(0, -angle, 0),0.1f));*/


    }
}
