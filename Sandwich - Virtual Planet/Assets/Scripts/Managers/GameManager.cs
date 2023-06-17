using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField, Header("Balancing")]
    private float minimumDistanceToDropIngredientDistance;
    [SerializeField]
    private float dropDistanceOffset;

    [SerializeField, Space]
    private GameObject balcony;
    [SerializeField]
    private Button nextPlateButton;
    [SerializeField]
    private Plate plate;

    private ItemManager itemManager;
    private UIManager uiManager;
    private ScoreManager scoreManager;
    private TimeManager timeManager;
    private AudioManager audioManager;

    public bool IsPlaying { get; private set; } = false;

    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    void Start()
    {
        nextPlateButton.onClick.AddListener(OnNextPlateButtonClicked);
        itemManager = ItemManager.instance;
        uiManager = UIManager.instance;
        scoreManager = ScoreManager.instance;
        timeManager = TimeManager.instance;
        audioManager = AudioManager.instance;
        CreateBalconyItems();
    }

    public void StartGame()
    {
        uiManager.ClearSandwichOrder();
        scoreManager.RestartCurrentScore();
        uiManager.ShowCoundownScreen(OnCountdownFinished);
        audioManager.PlayCountdownMusic();
        IsPlaying = true;
    }

    private void OnCountdownFinished()
    {
        uiManager.EnableInGameUI();
        audioManager.PlayGameMusic();
        itemManager.EnableActionContainer();
        timeManager.StartTimer();
        plate.CreateNewPlate();
    }

    public void FinishSession()
    {
        uiManager.EnableInGameUI(false);
        uiManager.ShowFinishScreen();
        itemManager.EnableActionContainer(false);
        scoreManager.Save();
        IsPlaying = false;
    }

    private void CreateBalconyItems()
    {
        foreach (IngredientName name in Enum.GetValues(typeof(IngredientName)))
        {
            uiManager.CreateIngredientView(balcony.transform, name);
        }

        for(int i = 0; i < 3; i++)
        {
            uiManager.AddBalconyView(balcony.transform);
        }

        nextPlateButton.transform.SetAsLastSibling();
    }

    public void OnIngredientDropped(Vector3 position, Ingredient ingredient)
    {
        float distance = GetDistanceFromPlate(position);
        if(distance <= minimumDistanceToDropIngredientDistance)
        {
            IngredientName requiredIngredient = plate.GetCurrentRequiredIngredient();
            bool ingredientsMatch = ingredient.IngredientName == requiredIngredient;
            scoreManager.CalculateScore(ingredientsMatch, distance);
            
            plate.AddIngredient(ingredient);
        }
        else
        {
            ingredient.gameObject.SetActive(false);
            scoreManager.RemoveScoreForDroppingIngredient();
        }
    }

    private float GetDistanceFromPlate(Vector3 position)
    {
        Vector3 target = plate.GetLastIngredientPosition();
        if(position.y >= target.y - dropDistanceOffset)
        {
            float distance = Mathf.Abs(position.x - target.x);
            return distance;
        }
        else
        {
            return int.MaxValue;
        }
    }

    public void Pause()
    {
        uiManager.SetItemViewInteractable(false);
        timeManager.Pause();
    }

    public void Resume()
    {
        uiManager.SetItemViewInteractable(true);
        timeManager.Resume();
    }

    private void OnNextPlateButtonClicked()
    {
        if (plate.IsSandwichComplete)
        {
            scoreManager.OnFinishCompleteBurguer();
        }
        else
        {
            scoreManager.OnFinishIncompleteBurguer();
        }
        plate.CreateNewPlate();
    }

    public Plate GetPlate()
    {
        return plate;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartGame();
        }
    }
}
