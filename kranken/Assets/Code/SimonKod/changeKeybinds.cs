using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class changeKeybinds : MonoBehaviour
{
    private SimonsPlayer playerScript;
    private bool isChangingKey = false;
    private Canvas canvas;
    [SerializeField] Canvas pauseMenue;
    [SerializeField] private TMP_Text header;
    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject pointer;
    private string currentScene;
    KeyCode up = KeyCode.W, down = KeyCode.S, select = KeyCode.E;
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SimonsPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        up = playerScript.inputs[0];
        down = playerScript.inputs[1];

        if (canvas.enabled == true)
        {
            


            if (isChangingKey)
            {
                header.text = "Changing Key";
                header.color = Color.red;
                foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(keyCode) && keyCode != KeyCode.E)
                    {
                        if(counter != 6)
                        {
                        playerScript.inputs[(int)(counter)] = keyCode;
                        }
                        else
                        {
                            GameObject.FindGameObjectWithTag("FlashLight").GetComponent<Flashlight>().key = keyCode;
                        }
                        isChangingKey = false;
                    }
                }
            }
            else
            {
                header.text = "Press E To Change Keybind";
                header.color = Color.white;
                CounterChange(up, -1f);
                CounterChange(down, 1);
                PointerMover(pointer.GetComponent<RectTransform>().position, buttons[(int)counter].GetComponent<RectTransform>().position);
                Select();
            }
            
        }

        else
        {
            isChangingKey = false;
        }

    }
    private void CounterChange(KeyCode key, float amount)
    {
        if (Input.GetKeyDown(key))
        {
            counter += amount;
        }

        if (counter < 0)
        {
            counter = buttons.Length - 1;
        }
        else if (counter >= buttons.Length)
        {
            counter = 0;
        }
    }
    private void PointerMover(Vector3 pointerPos, Vector3 buttonPos)
    {
        pointer.GetComponent<RectTransform>().position = Vector3.MoveTowards(pointerPos, new Vector3(pointerPos.x, buttonPos.y, pointerPos.z), 75);
        if(counter > 3)
        {
            pointer.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        else
        {
            pointer.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
    private void Select()
    {
        if (Input.GetKeyDown(select))
        {
            if (counter != 7)
            {
                isChangingKey = true;
            }
            else
            {
                canvas.enabled = false;
                pauseMenue.gameObject.GetComponent<PauseMenue>().isPaused = false;
            }
        }
    }
}
