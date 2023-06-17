using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inGameUIContainer;
    [SerializeField]
    private SandwichView currentOrder;
    [SerializeField]
    private GameObject balconyView;
    [SerializeField]
    private BalconyItemView balconyIngredientViewPrefab;

    [SerializeField, Space, Header("Screens")]
    private FinishScreen finishScreen;
    [SerializeField]
    private MainMenuScreen mainMenuScreen;
    [SerializeField]
    private CountdownScreen countdownScreen;
    [SerializeField]
    private PauseScreen pauseScreen;

    private List<ItemView> balconyItemViews = new List<ItemView>();

    #region Singleton
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.instance.IsPlaying)
            {
                ShowPauseScreen();
            }
        }
    }

    public void ClearSandwichOrder()
    {
        currentOrder.Clear();
    }

    public void ChangeSandwichOrder(Sandwich sandwich)
    {
        currentOrder.SetUp(sandwich);
    }

    public BalconyItemView CreateIngredientView(Transform parent, IngredientName ingredientName)
    {
        BalconyItemView view = Instantiate(balconyIngredientViewPrefab, parent);
        balconyItemViews.Add(view);
        view.SetUp(ingredientName);
        return view;
    }

    public void AddBalconyView(Transform parent)
    {
        Instantiate(balconyView, parent);
    }

    public void SetItemViewInteractable(bool value = true)
    {
        foreach(BalconyItemView item in balconyItemViews)
        {
            item.SetInteractable(value);
        }
    }

    public void EnableInGameUI(bool value = true)
    {
        inGameUIContainer.gameObject.SetActive(value);
    }

    public void ShowFinishScreen()
    {
        if (!finishScreen.IsShowing)
        {
            finishScreen.SetUp();
            finishScreen.Show();
        }
    }

    public void ShowMainMenu()
    {
        mainMenuScreen.Show();
    }

    public void ShowCoundownScreen(Action callback)
    {
        countdownScreen.StartCountDown(callback);
    }

    public void ShowPauseScreen()
    {
        if (!pauseScreen.IsShowing)
        {
            pauseScreen.Show();
        }
    }
}
