using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sandwich", menuName = "ScriptableObjects/Sandwich")]

public class Sandwich : ScriptableObject
{
    [SerializeField]
    private string name;
    [SerializeField]
    private IngredientName[] requiredIngredients;

    public string Name => name;
    public IngredientName[] RequiredIngredients => requiredIngredients;
}
