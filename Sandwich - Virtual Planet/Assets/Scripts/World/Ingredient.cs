using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private BoxCollider2D collider;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float height;

    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public Rigidbody2D Rigidbody2D => rb;
    public IngredientName IngredientName { get; private set; }
    public BoxCollider2D Collider => collider;
    public float Height => height;

    public void EnableCollider(bool value = true)
    {
        collider.enabled = value;
    }

    public void EnableRigidBody(bool value = true)
    {
        if (value)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    public void SetUp(IngredientName name)
    {
        IngredientName = name;
        ItemManager itemManager = ItemManager.instance;
        spriteRenderer.sprite = itemManager.GetWorldAsset(name);
    }

}
