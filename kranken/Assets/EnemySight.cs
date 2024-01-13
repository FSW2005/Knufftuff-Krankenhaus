using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    private Vector3 parentTransform = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        parentTransform = gameObject.GetComponentInParent<Transform>().position;
        print(parentTransform);
        Debug.DrawLine(parentTransform, transform.position,Color.red);
        Debug.DrawLine(new Vector3(0,0,0), new Vector3(0,0,5), Color.red);


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(GetComponentInParent<Transform>().position, transform.position);
    }
}
