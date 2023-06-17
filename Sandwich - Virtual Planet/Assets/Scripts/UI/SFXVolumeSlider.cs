
public class SFXVolumeSlider : VolumeSlider
{
    protected override void AdjustVolume(float volume)
    {
        audioManager.ChangeSFXVolume(volume);
    }
}
