using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Safearea : MonoBehaviour
{
    RectTransform PanelSafeArea;
    public Canvas canvas;

    Rect currentsafearea;
    ScreenOrientation currentOthoritation=ScreenOrientation.AutoRotation;

    // Start is called before the first frame update
    void Awake()
    {
        PanelSafeArea = GetComponent<RectTransform>();

        currentOthoritation = Screen.orientation;
        currentsafearea = Screen.safeArea;
        safeareaMethod();
    }
    void safeareaMethod()
    {
        if (PanelSafeArea==null)
        {
            return;
        }
        Rect safearea = Screen.safeArea;
        Vector2 min = safearea.position;
        Vector2 max = safearea.position + safearea.size;

        min.x /= canvas.pixelRect.width;
        min.y /= canvas.pixelRect.height;

        max.x /= canvas.pixelRect.width;
        max.y /= canvas.pixelRect.height;

        PanelSafeArea.anchorMin = min;
        PanelSafeArea.anchorMax = max;

        currentOthoritation = Screen.orientation;
        currentsafearea = Screen.safeArea;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
