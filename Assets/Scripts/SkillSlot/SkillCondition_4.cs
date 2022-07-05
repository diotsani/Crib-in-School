using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCondition_4 : DropCondition
{
    public override bool Check(DraggableComponent draggable)
    {
        return draggable.GetComponent<Skill_4>() != null;
    }

    //public override bool Check_2(DraggableComponent draggable)
    //{
        //return draggable.GetComponent<Skill_2>() != null;
    //}
}
