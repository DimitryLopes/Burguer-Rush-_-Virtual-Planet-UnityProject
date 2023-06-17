using UnityEngine;
using UnityEngine.EventSystems;


public class ItemDragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    ItemManager itemManager = ItemManager.instance;

    private Ingredient instantiatedIngredient;
    private IngredientName ingredientName;

    public void SetIngredient(IngredientName ingredient)
    {
        ingredientName = ingredient;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        instantiatedIngredient = itemManager.GetIngredient(ingredientName);
        instantiatedIngredient.EnableRigidBody(false);
        instantiatedIngredient.EnableCollider(false);
        instantiatedIngredient.transform.position = GetMouseOnWorldPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        instantiatedIngredient.transform.position = GetMouseOnWorldPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        instantiatedIngredient.EnableRigidBody(true);
        instantiatedIngredient.EnableCollider();
    }

    private Vector3 GetMouseOnWorldPosition(PointerEventData eventData)
    {
        Vector3 pos =  Camera.main.ScreenToWorldPoint(eventData.position);
        pos.z = 0;
        return pos;
    }
}
