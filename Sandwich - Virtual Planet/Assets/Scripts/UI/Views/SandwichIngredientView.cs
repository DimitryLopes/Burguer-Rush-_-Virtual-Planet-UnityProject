using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SandwichIngredientView : ItemView
{
    [SerializeField]
    private TextMeshProUGUI itemNameText;

    public override void SetUp(IngredientName ingredient)
    {
        base.SetUp(ingredient);

        string name = ingredient.ToString();
        itemNameText.text = name.Replace('_', ' ');
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }
}
