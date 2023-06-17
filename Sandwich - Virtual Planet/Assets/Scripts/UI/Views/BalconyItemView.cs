using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BalconyItemView : ItemView
{
    [SerializeField]
    private ItemDragAndDrop dragAndDrop;

    public override void SetUp(IngredientName ingredient)
    {
        base.SetUp(ingredient);
        dragAndDrop.SetIngredient(ingredient);
    }

    public void SetInteractable(bool value = true)
    {
        dragAndDrop.enabled = value;
    }
}
