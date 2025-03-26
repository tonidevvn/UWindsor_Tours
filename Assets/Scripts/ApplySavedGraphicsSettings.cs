using UnityEngine;

public class ApplySavedGraphicsSettings : MonoBehaviour
{
    void Start()
    {
        Screen.fullScreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality", 2));
    }
}
