using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonsPlayer : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private KeyCode[] inputs;
    /*
    Forward
    Backwards
    Left
    Right

    */
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement(inputs[0], transform.forward);
        Movement(inputs[1], -transform.forward);
        Movement(inputs[2], -transform.right);
        Movement(inputs[3], transform.right);

    }
    private void Movement(KeyCode key,Vector3 direction)
    {
        if (Input.GetKey(key))
        {
            rb.AddForce(direction*Time.deltaTime*speed);
        }
    }
}
