using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float maxEnergy,enemyStunDuration,enemyStunCooldown, startPoint, beamLegnth, baseWidth, endWidth;
    private float enemyStunDurationCounter, enemyStunCooldownCounter;
    private Vector3 randomCircle;
    private RaycastHit hit;
    public bool enemyStunned;
    private bool flashlightOn = true;
    [SerializeField]
    public KeyCode key;
    private Light light;

    // Start is called before the first frame update
    void Start()
    {
        light = gameObject.GetComponent<Light>();
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < 200; i++)
        {
            randomCircle = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, Random.insideUnitCircle.y);
            Debug.DrawRay(transform.forward * startPoint + transform.position + randomCircle * baseWidth, (transform.forward * beamLegnth) + randomCircle * endWidth, Color.green, 0, true);
            if (Physics.Raycast(transform.forward * startPoint + transform.position + randomCircle * baseWidth, (transform.forward * beamLegnth) + randomCircle * endWidth, out hit, beamLegnth))
            {
                if (flashlightOn && hit.transform.gameObject.tag == "Enemy" && enemyStunCooldownCounter <=0)
                {
                    enemyStunDurationCounter = enemyStunDuration;
                    enemyStunCooldownCounter = enemyStunCooldown;
                    print("yay!");
                    
                    i = 201;
                }

            }
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (flashlightOn)
            {
                flashlightOn = false;
                light.enabled = false;
            }
            else
            {
                flashlightOn = true;
                light.enabled = true;
            }
        }
        if (enemyStunDurationCounter > 0)
        {
            enemyStunDurationCounter -= Time.deltaTime;
            enemyStunned = true;
        }
        else
        {
            enemyStunned = false;
        }
        enemyStunCooldownCounter -= Time.deltaTime;
    }
}
