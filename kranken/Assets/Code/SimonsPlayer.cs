using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonsPlayer : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float speed,sprintSpeed,maxSprintDuration;
    private float sprintDurationCounter,sprintSpeedMulti=1;
    [SerializeField]
    private KeyCode[] inputs;
    private Vector3 endVelocity;
    [SerializeField]
    private Slider slider;
    /*
    Forward
    Backwards
    Left
    Right
    Sprint

    */
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        slider.maxValue = maxSprintDuration;
    }

    // Update is called once per frame
    void Update()
    {
        Stamina(inputs[4]);
        Movement(inputs[0], transform.forward);
        Movement(inputs[1], -transform.forward);
        Movement(inputs[2], -transform.right);
        Movement(inputs[3], transform.right);
        rb.velocity =(endVelocity * Time.deltaTime * speed*sprintSpeedMulti);
        endVelocity = new Vector3(0, 0, 0);
    }
    private void Movement(KeyCode key,Vector3 direction)
    {
        if (Input.GetKey(key))
        {
            endVelocity += direction;
            
        }
    }
    private void Stamina(KeyCode key)
    {
        if (Input.GetKey(key) && sprintDurationCounter>0)
        {
            sprintSpeedMulti = sprintSpeed;
            sprintDurationCounter -= Time.deltaTime;
        }
        else
        {
            sprintSpeedMulti = 1;
        }
        if(!Input.GetKey(key) && sprintDurationCounter < maxSprintDuration)
        {
            sprintDurationCounter += Time.deltaTime;
        }
        slider.value = sprintDurationCounter;
    }
}
