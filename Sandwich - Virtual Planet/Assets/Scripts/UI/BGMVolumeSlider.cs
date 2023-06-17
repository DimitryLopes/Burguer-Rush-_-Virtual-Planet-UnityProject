public class BGMVolumeSlider : VolumeSlider
{
    protected override void AdjustVolume(float volume)
    {
        audioManager.ChangeBGMVolume(volume);
    }
}
