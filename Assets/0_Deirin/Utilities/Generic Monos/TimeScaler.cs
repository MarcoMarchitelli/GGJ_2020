using UnityEngine;

public class TimeScaler : MonoBehaviour {
    [Header("Parameters")]
    public float targetValue = 0;

    public void SetTimeScale ( float value ) {
        Time.timeScale = value;
    }

    public void SetTimeScale () {
        Time.timeScale = targetValue;
    }
}