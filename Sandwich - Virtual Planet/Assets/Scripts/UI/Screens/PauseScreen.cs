using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : UIScreen
{
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button mainMenuButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(Hide);
        quitButton.onClick.AddListener(Quit);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    protected override void EnableButtons()
    {
        base.EnableButtons();
        quitButton.enabled = true;
        mainMenuButton.enabled = true;
    }

    protected override void DisableButtons()
    {
        base.DisableButtons();
        quitButton.enabled = false;
        mainMenuButton.enabled = false;
    }

    private void OnMainMenuButtonClicked()
    {
        animator.SetTrigger(MAIN_MENU_ANIMATION_KEY);
    }

    //Called on animation
    private void OnBeforeShow()
    {
        GameManager.instance.Pause();
    }

    protected override void ShowMainMenu()
    {
        GameManager.instance.FinishSession();
        Close();
    }

    private void Close()
    {
        screenContainer.SetActive(false);
        GameManager.instance.Resume();
    }

    private void Quit()
    {
        Application.Quit();
    }

}
