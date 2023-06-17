using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ingredient ingredient = collision.gameObject.GetComponent<Ingredient>();
        if (ingredient != null && ingredient.Rigidbody2D.bodyType != RigidbodyType2D.Static)
        {
            GameManager.instance.OnIngredientDropped(transform.position, ingredient);
        }
    }
}
