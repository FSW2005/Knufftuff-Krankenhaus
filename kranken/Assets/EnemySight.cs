using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    private Vector3 randomCircle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        for (int i = 0; i < 5; i++)
        {
            randomCircle = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, Random.insideUnitCircle.y);
            Debug.DrawRay(transform.forward*0.5f+transform.position+randomCircle*0.2f, (transform.forward * 5) + randomCircle*1.5f, Color.green, Time.deltaTime,true);
        }
    }
    void Update()
    {
        
        /*
        parentTransform = gameObject.GetComponentInParent<Transform>().position;
        print(parentTransform);
        Debug.DrawLine(transform.position, parentTransform,Color.blue,Time.deltaTime,false);
        // Debug.DrawLine(new Vector3(0,0,0), new Vector3(0,0,5), Color.red);
        */



    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(GetComponentInParent<Transform>().position, transform.position);
    }*/
}
