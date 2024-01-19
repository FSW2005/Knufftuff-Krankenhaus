using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeMatGlassWall : MonoBehaviour
{
    private float scaleX;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.localScale.x > gameObject.transform.localScale.z)
        {
            scaleX = gameObject.transform.localScale.x;
        }
        else
        {
            scaleX = gameObject.transform.localScale.z;
        }

        gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(scaleX / 3, transform.localScale.y / 3);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
