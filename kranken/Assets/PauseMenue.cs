using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenue : MonoBehaviour
{
    public bool isPaused;
    private Canvas canvas;
    [SerializeField] Canvas keybindMenue;
    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject pointer;
    private string currentScene;
    KeyCode up = KeyCode.W, down = KeyCode.S, select = KeyCode.E;
    private float counter;

    [SerializeField]
    private Image fadeToBlack;
    private bool startFadingToBlack = false;
    [SerializeField] float fadeToBlackSpeed;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startFadingToBlack)
        {
            Time.timeScale = 1;

            if (fadeToBlack.color.a < 1)
            {
                fadeToBlack.color = new Color(fadeToBlack.color.r, fadeToBlack.color.g, fadeToBlack.color.b, fadeToBlack.color.a + fadeToBlackSpeed * Time.deltaTime);
            }
            else
            {
                currentScene = SceneManager.GetActiveScene().name;
                SceneManager.LoadSceneAsync("Menue");
                SceneManager.UnloadSceneAsync(currentScene);
            }
        }
        else
        {


            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isPaused = !isPaused;
               
                if (isPaused)
                {
                    canvas.enabled = true;
                }
            }

            if (isPaused)
            {
                Time.timeScale = 0;
                if (canvas.enabled)
                {
                    CounterChange(up, -1f);
                    CounterChange(down, 1);
                    PointerMover(pointer.GetComponent<RectTransform>().position, buttons[(int)counter].GetComponent<RectTransform>().position);
                    Select();
                }
                
            }

            else
            {
                Time.timeScale = 1;
                canvas.enabled = false;
                keybindMenue.enabled = false;
            }

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
        pointer.GetComponent<RectTransform>().position = Vector3.MoveTowards(pointerPos, new Vector3(pointerPos.x, buttonPos.y, pointerPos.z),5);
    }
    private void Select()
    {
        if (Input.GetKeyDown(select))
        {
            if (counter == 0)
            {
                isPaused = false;
            }
            else if (counter == 1)
            {
                startFadingToBlack = true;
            }
            else if (counter == 2)
            {
                keybindMenue.enabled = true;
                canvas.enabled = false;
            }
        }
    }
}
