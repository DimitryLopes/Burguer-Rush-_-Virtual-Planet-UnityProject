using UnityEngine;
using UnityEngine.UI;

public class UIScreen : MonoBehaviour
{
    protected const string HIDE_ANIMATION_KEY = "hide";
    protected const string SHOW_ANIMATION_KEY = "show";
    protected const string MAIN_MENU_ANIMATION_KEY = "main_menu";

    [SerializeField]
    protected GameObject screenContainer;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected Button closeButton;

    [SerializeField, Header("Sound")]
    private AudioClip openAudioClip;
    [SerializeField]
    private AudioClip closeAudioClip;

    public bool IsShowing => screenContainer.activeSelf;

    public virtual void Start()
    {
        closeButton.onClick.AddListener(Hide);
    }

    public void Hide()
    {
        DoHideAnimation();
        PlayAudio(closeAudioClip);
    }

    public void Show()
    {
        DoShowAnimation();
        PlayAudio(openAudioClip);
    }

    private void PlayAudio(AudioClip clip)
    {
        AudioManager.instance.PlayAudio(clip);
    }

    protected virtual void DoHideAnimation()
    {
        animator.SetTrigger(HIDE_ANIMATION_KEY);
    }

    protected virtual void DoShowAnimation()
    {
        animator.SetTrigger(SHOW_ANIMATION_KEY);
    }

    //called by animation
    public virtual void OnBeforeShow() { }

    //called by animation
    public virtual void OnAfterShow()
    {
        EnableButtons();
    }

    //called by animation
    public virtual void OnBeforeHide()
    {
        DisableButtons();
    }

    //Called on animation
    protected virtual void ShowMainMenu()
    {
        UIManager.instance.ShowMainMenu();
    }

    protected virtual void EnableButtons()
    {
        closeButton.enabled = true;
    }

    protected virtual void DisableButtons()
    {
        closeButton.enabled = false;
    }
}
