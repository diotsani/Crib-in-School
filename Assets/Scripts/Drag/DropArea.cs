using System;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    // Script ini memanggil dari script DropCondition
    // Script ini digunakan sebagai area atau tujuan target skill slot
    // Script ini akan otomatis dipanggil oleh script Equipment slot

	public List<DropCondition> DropConditions = new List<DropCondition>();
	public event Action<DraggableComponent> OnDropHandler;

	public bool Accepts(DraggableComponent draggable)
	{
		return DropConditions.TrueForAll(cond => cond.Check(draggable));
    }

    //public bool Accepts_2(DraggableComponent draggable)
    //{
        //return DropConditions.TrueForAll(cond => cond.Check_2(draggable));
    //}

    public void Drop(DraggableComponent draggable)
	{
		OnDropHandler?.Invoke(draggable);
	}
}
