using UnityEngine.UI;
using UnityEngine;

public class UIAudioButton : Button
{
    [SerializeField, Space, Header("Audio")]
    private AudioClip audioClip;

    private void Awake()
    {
        onClick.AddListener(PlaySound);
    }

    private void OnDestroy()
    {
        onClick.RemoveListener(PlaySound);
    }

    private void PlaySound()
    {
        if (audioClip != null)
        {
            AudioManager.instance.PlayAudio(audioClip);
        }
        else
        {
            AudioManager.instance.PlayButtonAudio();
        }
    }
}
