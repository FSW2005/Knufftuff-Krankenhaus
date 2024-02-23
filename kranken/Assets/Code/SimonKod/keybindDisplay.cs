using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class keybindDisplay : MonoBehaviour
{
    [SerializeField] private int keyNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(keyNum!= 6)
        {
            gameObject.GetComponent<TMP_Text>().text = GameObject.FindGameObjectWithTag("Player").GetComponent<SimonsPlayer>().inputs[keyNum].ToString();
        }
        else
        {
            gameObject.GetComponent<TMP_Text>().text = GameObject.FindGameObjectWithTag("FlashLight").GetComponent<Flashlight>().key.ToString();
        }

    }
}
