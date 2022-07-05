using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSetting : MonoBehaviour
{
    private Vector2 sizeSkill_1 = new Vector2(79f, 67f);
    private Vector2 sizeSkill_2 = new Vector2(79f, 67f);

    public GameObject[] objectDrag;
    public DraggableComponent[] dragComponent;

    public GameObject[] objectImage;
    public Image[] image;

    public GameObject[] objectSkillButton;
    public SkillButton[] skillButton;

    public GameObject[] objectSkillManager;
    public SkillManager[] skillManager;

    public void Start()
    {

    }

    public void Update()
    {
        objectDrag = GameObject.FindGameObjectsWithTag("Skill");
        dragComponent = new DraggableComponent[objectDrag.Length];
        for (int i = 0; i < objectDrag.Length; i++)
        {
            dragComponent[i] = objectDrag[i].GetComponent<DraggableComponent>();
        }

        objectImage = GameObject.FindGameObjectsWithTag("Sprite");
        image = new Image[objectImage.Length];
        for (int i = 0; i < objectImage.Length; i++)
        {
            image[i] = objectImage[i].GetComponent<Image>();
        }

        objectSkillButton = GameObject.FindGameObjectsWithTag("Sprite");
        skillButton = new SkillButton[objectSkillButton.Length];
        for (int i = 0; i < objectSkillButton.Length; i++)
        {
            skillButton[i] = objectSkillButton[i].GetComponent<SkillButton>();
        }

        dragComponent[0].enabled = false;
       
        //dragComponent[1].enabled = false;

        if (dragComponent[0].enabled == false)
        {
            //skillButton[0].enabled = false;
            image[0].sprite = dragComponent[0].spriteChange;
            image[0].rectTransform.sizeDelta = sizeSkill_1;
        }

        //if (dragComponent[1].enabled == false)
        //{
            //skillButton[1].enabled = false;
            //image[1].sprite = dragComponent[1].spriteChange;
            //image[1].rectTransform.sizeDelta = sizeSkill_2;
            //image[1].rectTransform.sizeDelta = new Vector2(79f, 67f);
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

    GameObject FindInActiveObjectByTag(string tag) //fungsi mencari object yang tidak aktif menggunakan tag
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag(tag))
                {

                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
