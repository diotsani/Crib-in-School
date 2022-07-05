using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISkill : MonoBehaviour
{
    public static UISkill Instance { get; private set; }

    private Canvas canvas;
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    private CanvasGroup canvasGroup;
    private Image image;

    public void Awake()
    {
        Instance = this;

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        image = transform.Find("image").GetComponent<Image>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();

        Hide();
    }

    public void Update()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {

    }

    public void SetSprite (Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
