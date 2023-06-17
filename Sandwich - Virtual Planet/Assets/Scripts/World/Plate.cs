using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField]
    private Vector2 defaultcolliderOffset;
    [SerializeField]
    private Vector2 defaultColliderSize;
    [SerializeField]
    private BoxCollider2D collider;

    private Stack<Ingredient> ingredientRenderers = new Stack<Ingredient>();
    private int currentSandwichIngredientIndex;
    private float currentSandwichOffset;

    public Sandwich CurrentSandwich { get; private set; }
    public bool IsSandwichComplete => currentSandwichIngredientIndex == CurrentSandwich.RequiredIngredients.Length;
    public int IngredientCount => ingredientRenderers.Count;

    public void CreateNewPlate()
    {
        while(ingredientRenderers.Count > 0)
        {
            ingredientRenderers.Pop().gameObject.SetActive(false);
        }

        ingredientRenderers.Clear();
        currentSandwichOffset = 0;
        collider.size = defaultColliderSize;
        collider.offset = defaultcolliderOffset;
        currentSandwichIngredientIndex = 0;
        CurrentSandwich = ItemManager.instance.GetNewSandwich();
        UIManager.instance.ChangeSandwichOrder(CurrentSandwich);
    }
    
    public void AddIngredient(Ingredient ingredient)
    {
        ingredient.transform.SetParent(transform, true);
        currentSandwichOffset += ingredient.Collider.bounds.size.y;

        float sizeDifference = collider.size.y + ingredient.Collider.size.y;
        Vector2 newColliderSize = new Vector2(collider.size.x, sizeDifference);

        Vector2 newColliderOffset = new Vector2(collider.offset.x, collider.offset.y + ingredient.Height);
        collider.size = newColliderSize;
        collider.offset = newColliderOffset;

        ingredientRenderers.Push(ingredient);
        if (ingredient.IngredientName == CurrentSandwich.RequiredIngredients[currentSandwichIngredientIndex])
        {
            Debug.Log("Ingredient Name: " + ingredient.IngredientName);
            currentSandwichIngredientIndex++;
        }
    }

    public IngredientName GetCurrentRequiredIngredient() 
    {
        return CurrentSandwich.RequiredIngredients[currentSandwichIngredientIndex];
    }

    public Vector3 GetLastIngredientPosition()
    {
        Vector3 offsetPosition = transform.position;
        offsetPosition.x += currentSandwichOffset;
        return offsetPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ingredient ingredient = collision.gameObject.GetComponent<Ingredient>();
        if(ingredient != null && ingredient.Rigidbody2D.bodyType != RigidbodyType2D.Static)
        {
            ingredient.EnableRigidBody(false);
            ingredient.EnableCollider(false);
            GameManager.instance.OnIngredientDropped(collision.transform.position, ingredient);
        }

    }

}
