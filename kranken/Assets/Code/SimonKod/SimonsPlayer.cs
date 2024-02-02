using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimonsPlayer : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float speed,sprintSpeed,maxSprintDuration, crouchHight;
    private float sprintDurationCounter,sprintSpeedMulti=1,defaultHight;
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
    Crouch
    */
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        slider.maxValue = maxSprintDuration;
        defaultHight = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputs[5]))
        {
            transform.localScale = new Vector3(1, crouchHight, 1);
            transform.position -= new Vector3(0, crouchHight, 0);
        }
        if (Input.GetKeyUp(inputs[5]))
        {
            transform.localScale = new Vector3(1, defaultHight, 1);
            transform.position += new Vector3(0, crouchHight, 0);

        }
        Stamina(inputs[4]);
        Movement(inputs[0], transform.forward);
        Movement(inputs[1], -transform.forward);
        Movement(inputs[2], -transform.right);
        Movement(inputs[3], transform.right);
        rb.velocity =(endVelocity * speed*sprintSpeedMulti);
        endVelocity = new Vector3(0, rb.velocity.y, 0);
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
        if (Input.GetKey(key) &&!(Input.GetKeyDown(inputs[5])) && sprintDurationCounter>0)
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
