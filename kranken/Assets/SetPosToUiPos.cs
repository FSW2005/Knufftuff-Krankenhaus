using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosToUiPos : MonoBehaviour
{
    [SerializeField]
    private RectTransform uiPos;
    [SerializeField]
    private Camera cam;
    private float locationFraction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        locationFraction =  (uiPos.anchorMax.x - uiPos.anchorMin.x) / cam.pixelWidth;
        print(uiPos.anchorMin.x);
        print(uiPos.anchorMax.x);

        print(locationFraction);
        // transform.position = cam.ScreenToWorldPoint(uiObj.GetComponent<RectTransform>().position);
        // transform.position = new Vector3(-transform.position.x, transform.position.y,0);
      transform.position = cam.ScreenToWorldPoint(uiPos.anchoredPosition+uiPos.);
    }
}
