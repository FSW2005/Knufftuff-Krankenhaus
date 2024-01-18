using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class HomeMenue : MonoBehaviour
{
    [SerializeField]
    GameObject[] buttons;
    [SerializeField]
    GameObject pointer,creditsWindow;
    [SerializeField]
    KeyCode up, down,select;
    private float counter;
    [SerializeField]
    string firstLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!creditsWindow.GetComponent<Image>().enabled)
        {
            CounterChange(up, -1f);
            CounterChange(down, 1);
        }
        

        if (counter < 0)
        {
            counter = buttons.Length - 1;
        }
        else if (counter >= buttons.Length)
        {
            counter = 0;
        }
        PointerMover(pointer.GetComponent<RectTransform>().position, buttons[(int)counter].GetComponent<RectTransform>().position);
        Select();
        //MouseMovePointer();
    }
    private void CounterChange(KeyCode key,float amount)
    {
        if (Input.GetKeyDown(key))
        {
            counter += amount;
        } 
    }
    private void PointerMover(Vector3 pointerPos,Vector3 buttonPos)
    {
        pointer.GetComponent<RectTransform>().position = new Vector3(pointerPos.x, buttonPos.y, pointerPos.z);
    }
    private void Select()
    {
        if (Input.GetKeyDown(select))
        {
            if(counter == 0)
            {
                SceneManager.LoadScene(firstLevel, LoadSceneMode.Single);
            }
            else if (counter == 1)
            {
                if (creditsWindow.GetComponent<Image>().enabled)
                {
                    creditsWindow.GetComponent<Image>().enabled = false;
                    creditsWindow.gameObject.GetComponentInChildren<TextMeshPro>().enabled = false;
                }
                else
                {
                    creditsWindow.GetComponent<Image>().enabled = true;
                    creditsWindow.gameObject.GetComponentInChildren<TextMeshPro>().enabled = true;
                }
            }
            else if(counter == 2)
            {
                //Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }
   /*
    private void MouseMovePointer()
    {
        print((Input.mousePosition).y);
        if(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y)== Mathf.Round(buttons[1].GetComponent<RectTransform>().position.y))
        {
            buttons[1].GetComponent<Image>().color = new Color(200, 200, 200, 255);
        }
    }
   */
}
