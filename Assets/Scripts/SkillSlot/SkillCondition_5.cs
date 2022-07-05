using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCondition_5 : DropCondition
{
    public override bool Check(DraggableComponent draggable)
    {
        return draggable.GetComponent<Skill_5>() != null;
    }

    //public override bool Check_2(DraggableComponent draggable)
    //{
        //return draggable.GetComponent<Skill_2>() != null;
    //}
}
