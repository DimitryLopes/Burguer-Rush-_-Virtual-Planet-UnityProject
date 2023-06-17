using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    const string ITEM_ICONS_PATH = "Sprites/Items/Icons/";
    const string INGREDIENT_ICON_SUFFIX = "_icon";
    const string ITEM_WORLD_PATH = "Sprites/Items/World/";
    const string INGREDIENT_WORLD_SUFFIX = "_world";

    const string INGREDIENT_PREFAB_PATH = "Prefabs/Items/";


    [SerializeField]
    private GameObject gameContainer;
    [SerializeField]
    private List<Sandwich> sandwichDatabase;
    private Plate plate;

    private GameManager gameManager;

    private Dictionary<IngredientName, List<Ingredient>> InstantiatedIngredients = new Dictionary<IngredientName, List<Ingredient>>();

    #region Singleton
    public static ItemManager instance;
    private void Awake()
    {
        instance = this;

        foreach (IngredientName name in Enum.GetValues(typeof(IngredientName)))
        {
            InstantiatedIngredients.Add(name, new List<Ingredient>());
        }
    }
    #endregion

    private void Start()
    {
        gameManager = GameManager.instance;
        plate = gameManager.GetPlate();
    }

    public Sandwich GetNewSandwich()
    {
        int randomIndex = UnityEngine.Random.Range(0, sandwichDatabase.Count);
        return sandwichDatabase[randomIndex];
    }

    public Ingredient GetIngredient(IngredientName name)
    {
        List<Ingredient> ingredients = InstantiatedIngredients[name];

        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i] != null)
            {
                if (ingredients[i].gameObject.activeSelf == false)
                {
                    ingredients[i].gameObject.SetActive(true);
                    SortItem(ingredients[i]);
                    return ingredients[i];
                }
            }
        }
        Ingredient newIngredient = GetIngredientPrefab(name);
        newIngredient = Instantiate(newIngredient);
        newIngredient.SetUp(name);
        ingredients.Add(newIngredient);
        SortItem(newIngredient);
        return newIngredient;
    }

    public void SortItem(Ingredient ingredient)
    {
        ingredient.SpriteRenderer.sortingOrder = plate.IngredientCount;
    }

    public void EnableActionContainer(bool value = true)
    {
        gameContainer.SetActive(value);
    }


    #region Asset Management
    public Ingredient GetIngredientPrefab(IngredientName ingredient)
    {
        string ingredientName = ingredient.ToString();
        string path = INGREDIENT_PREFAB_PATH + ingredientName;
        Ingredient prefab = Resources.Load<Ingredient>(path);

        return prefab;
    }

    public Sprite GetAssetIcon(IngredientName ingredient)
    {
        string ingredientName = ingredient.ToString();
        string path = ITEM_ICONS_PATH + ingredientName + INGREDIENT_ICON_SUFFIX;
        Sprite icon = Resources.Load<Sprite>(path);

        return icon;
    }

    public Sprite GetWorldAsset(IngredientName ingredient)
    {
        string ingredientName = ingredient.ToString();
        string path = ITEM_WORLD_PATH + ingredientName + INGREDIENT_WORLD_SUFFIX;
        Sprite icon = Resources.Load<Sprite>(path);

        return icon;
    }
    #endregion
}

public enum IngredientName
{
    Lettuce = 0,
    Burguer,
    Egg,
    Tomato,
    Cheese,
    Onion,
    Top_Bun,
    Bottom_Bun,
}
