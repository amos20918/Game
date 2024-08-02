using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value = AudioListener.volume;

        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    public void OnVolumeChange(float value)
    {
        AudioListener.volume = value;
    }

    void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(OnVolumeChange);
    }
}
