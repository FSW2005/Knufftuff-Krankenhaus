using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarulkSight2 : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    private void OnTriggerStay(Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            RaycastHit hit;
            Transform parentPos = gameObject.GetComponentInParent<Transform>();

            if (Physics.Raycast(parentPos.position,other.gameObject.transform.position-parentPos.position,out hit, Mathf.Infinity))
            {
                Debug.DrawRay(parentPos.position, hit.transform.position-parentPos.position,Color.red);
                if(hit.transform.gameObject.tag == "Player")
                {
                    gameObject.GetComponentInParent<Marulk2>().sawPlayer = true;
                }
                
            }
            
            
        }
        
    }
   
   
}
