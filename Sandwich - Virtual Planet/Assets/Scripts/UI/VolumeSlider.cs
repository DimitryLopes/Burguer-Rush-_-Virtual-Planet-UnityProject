using UnityEngine;
using UnityEngine.UI;

public abstract class VolumeSlider : MonoBehaviour
{
    protected AudioManager audioManager;

    [SerializeField]
    private Slider slider;

    private void Start()
    {
        audioManager = AudioManager.instance;
        slider.onValueChanged.AddListener(AdjustVolume);
    }

    protected abstract void AdjustVolume(float volume);
}
