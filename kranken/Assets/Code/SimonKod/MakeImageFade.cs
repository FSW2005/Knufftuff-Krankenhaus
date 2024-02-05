using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeImageFade : MonoBehaviour
{
    private Image image;
    [SerializeField]
    private float fadeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (image.color.a > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (fadeSpeed * Time.deltaTime + 0.5f*fadeSpeed*(1-image.color.a)));
        }

        
    }
}
