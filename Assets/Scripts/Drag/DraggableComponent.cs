using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableComponent : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //script sound 
    public SoundManager sfx;
    //Script ini digunakan untuk menggerakan atau melakukan drop

	public event Action<PointerEventData> OnBeginDragHandler;
	public event Action<PointerEventData> OnDragHandler;
	public event Action<PointerEventData, bool> OnEndDragHandler;
	public bool FollowCursor { get; set; } = true;
	public Vector3 StartPosition;
    public Vector3 posisiAwal;

	public bool CanDrag { get; set; } = true;

	private RectTransform rectTransform;
	private Canvas canvas;

    public GameObject upgradeButton;

    public int nomorSkill;

    public Sprite spriteChange;
    public GameObject panelbintang;

    public EquipmentSlot es; 


    private void Awake()
	{
        sfx = FindObjectOfType<SoundManager>();
		rectTransform = GetComponent<RectTransform>();
		canvas = GetComponentInParent<Canvas>();
	}

    public void Start()
    {

    }

    public void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
	{
		if (!CanDrag)
		{
			return;
		}

		OnBeginDragHandler?.Invoke(eventData);

        
    }

	public void OnDrag(PointerEventData eventData)
	{
		if (!CanDrag)
		{
			return;
		}

		OnDragHandler?.Invoke(eventData);

		if (FollowCursor)
		{
			rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
		}

        upgradeButton.SetActive(false);
        panelbintang.SetActive(false);
    }

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!CanDrag)
		{
			return;
		}

		var results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);

		DropArea dropArea = null;

		foreach (var result in results)
		{
			dropArea = result.gameObject.GetComponent<DropArea>();

			if (dropArea != null)
			{
                
                break;
            }
		}

		if (dropArea != null)
		{
			if (dropArea.Accepts(this))
			{
				dropArea.Drop(this);
				OnEndDragHandler?.Invoke(eventData, true);
				return;
			}
        }

        rectTransform.anchoredPosition = StartPosition;
        OnEndDragHandler?.Invoke(eventData, false);

        sfx.popupMetohod(0);
        //panelbintang.SetActive(true);
        //upgradeButton.SetActive(true);
        
    }

	public void OnInitializePotentialDrag(PointerEventData eventData)
	{
        StartPosition = posisiAwal;
    }

    public GameObject FindInActiveObjectByName(string name) //fungsi mencari object yang tidak aktif menggunakan nama
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
