using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SandwichView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI sandwichName;
    [SerializeField]
    private GameObject itemsContainer;
    [SerializeField]
    private List<SandwichIngredientView> ingredientIcons;

    public void SetUp(Sandwich sandwich)
    {
        Clear();
        int lastIndex = 0;
        sandwichName.text = sandwich.Name;
        foreach (IngredientName ingredient in sandwich.RequiredIngredients)
        {
            for (; lastIndex  < ingredientIcons.Count; lastIndex++)
            {
                if (!ingredientIcons[lastIndex].gameObject.activeSelf)
                {
                    ingredientIcons[lastIndex].SetUp(ingredient);
                    break;
                }
            }
        }
    }

    public void Clear()
    {
        foreach(SandwichIngredientView view in ingredientIcons)
        {
            view.Clear();
        }
    }
}
