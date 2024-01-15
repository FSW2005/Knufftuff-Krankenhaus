using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    private Vector3 randomCircle;
    [SerializeField]
    private float startPoint, beamLegnth, baseWidth, endWidth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        for (int i = 0; i < 20; i++)
        {
            randomCircle = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, Random.insideUnitCircle.y);
            Debug.DrawRay(transform.forward*startPoint+transform.position+randomCircle*baseWidth, (transform.forward * beamLegnth) + randomCircle*endWidth, Color.green, 0,true);
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
