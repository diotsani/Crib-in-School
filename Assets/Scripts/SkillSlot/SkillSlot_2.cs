using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlot_2 : EquipmentSlot
{
    protected override void Awake()
    {
        base.Awake();
        DropArea.DropConditions.Add(new SkillCondition_2());
    }
}
