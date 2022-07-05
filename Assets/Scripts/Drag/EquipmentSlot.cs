using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour
{
    // Script ini digunakan sebagai mengatur posisi dari target skill slot biar bisa tepat ditengah
    // Script ini akan otomatis memanggil script DropArea
    // Script yang aku komenkan itu script yang tidak digunakan atau hasil copas yang tidak kuhapus wkwkw

	protected DropArea DropArea;
    protected DraggableComponent CurrentItem = null;

    private DisableDropCondition disableDropCondition;
    public Selection select;

    public int nomer;

    private Vector3 resize = new Vector3(259f, 247f, 0f);
    public GameObject[] objectImage;
    public Image[] imageSkill;

    protected virtual void Awake()
	{
		DropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
		DropArea.OnDropHandler += OnItemDropped;
        disableDropCondition = new DisableDropCondition();
    }

    public void Start()
    {
        select = FindInActiveObjectByName("Selection Slot Skill").GetComponent<Selection>();

        objectImage = GameObject.FindGameObjectsWithTag("Sprite");
        imageSkill = new Image[objectImage.Length];
        for (int i = 0; i < objectImage.Length; i++)
        {
            imageSkill[i] = objectImage[i].GetComponent<Image>();
        }
    }

    public void Initialize(DraggableComponent currentItem)
    {
        if (currentItem == null)
        {
            Debug.LogError("Tried to initialize the slot with an null item!");
            return;
        }

        OnItemDropped(currentItem);
    }

    private void OnItemDropped(DraggableComponent draggable)
	{
        Debug.Log("Skill Dropped");

        draggable.transform.position = transform.position;
        CurrentItem = draggable;

        DropArea.DropConditions.Add(disableDropCondition);
        draggable.OnBeginDragHandler += CurrentItemOnBeginDrag;

        draggable.upgradeButton.SetActive(false);
        draggable.sfx.popupMetohod(0);
        nomer = draggable.nomorSkill;
    }



    //Current item is being dragged so we listen for the EndDrag event
    private void CurrentItemOnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin drag handler");
        CurrentItem.OnEndDragHandler += CurrentItemEndDragHandler;

    }

    private void CurrentItemEndDragHandler(PointerEventData eventData, bool dropped)
    {
        Debug.Log("end drag handler");

        CurrentItem.OnEndDragHandler -= CurrentItemEndDragHandler;
        DropArea.DropConditions.Remove(disableDropCondition); //We dropped the component in another slot so we can remove the DisableDropCondition
        CurrentItem.OnBeginDragHandler -= CurrentItemOnBeginDrag; //We make sure to remove this listener as the item is no longer in this slot
        CurrentItem = null; //We no longer have an item in this slot, so we remove the refference

        nomer = 0;

        //if (!dropped)
        //{

        // return;

        //}
    }

    GameObject FindInActiveObjectByName(string name) //fungsi mencari object yang tidak aktif menggunakan nama
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
