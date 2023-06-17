using UnityEngine.UI;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    [SerializeField]
    protected Image itemViewImage;
    protected ItemManager itemManager;

    public virtual void SetUp(IngredientName ingredient)
    {
        gameObject.SetActive(true);
        if (itemManager == null)
        {
            itemManager = ItemManager.instance;
        }

        itemViewImage.sprite = itemManager.GetAssetIcon(ingredient);
    }
}
