using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCondition_3 : DropCondition
{
    public override bool Check(DraggableComponent draggable)
    {
        return draggable.GetComponent<Skill_3>() != null;
    }

    //public override bool Check_2(DraggableComponent draggable)
    //{
        //return draggable.GetComponent<Skill_2>() != null;
    //}
}
