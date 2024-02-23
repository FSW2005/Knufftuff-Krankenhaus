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
    KeyCode up=KeyCode.W, down=KeyCode.S,select=KeyCode.E;
    private float counter;
    [SerializeField]
    string firstLevel;
    [SerializeField]
    GameObject textMeshObj;
    [SerializeField]
    private Image fadeToBlack;
    private bool startFadingToBlack = false;
    [SerializeField]
    private float fadeToBlackSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFadingToBlack)
        {
            if(fadeToBlack.color.a < 1)
            {
                fadeToBlack.color = new Color(fadeToBlack.color.r, fadeToBlack.color.g, fadeToBlack.color.b, fadeToBlack.color.a + fadeToBlackSpeed*Time.deltaTime);
            }
            else
            {
                SceneManager.LoadScene(firstLevel);
                SceneManager.UnloadScene("Menue");
            }
        }
        else
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
        pointer.GetComponent<RectTransform>().position = Vector3.MoveTowards(pointerPos, new Vector3(pointerPos.x, buttonPos.y, pointerPos.z),10);
    }
    private void Select()
    {
        if (Input.GetKeyDown(select))
        {
            if(counter == 0)
            {
                startFadingToBlack = true;
            }
            else if (counter == 1)
            {
                if (creditsWindow.GetComponent<Image>().enabled)
                {
                    creditsWindow.GetComponent<Image>().enabled = false;
                    textMeshObj.GetComponent<TMP_Text>().enabled = false;
                }
                else
                {
                    creditsWindow.GetComponent<Image>().enabled = true;
                    textMeshObj.GetComponent<TMP_Text>().enabled = true;
                }
            }
            else if(counter == 2)
            {
                Application.Quit();
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
