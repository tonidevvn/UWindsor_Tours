using UnityEngine;

public class ApplySavedGraphicsSettings : MonoBehaviour
{
    void Start()
    {
        Screen.fullScreen = PlayerPrefs.GetInt("fullscreen", 0) == 1;
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality", 2));
    }
}
