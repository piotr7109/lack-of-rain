using UnityEngine;

public class TimeManager : MonoBehaviour {

    #region Singleton

    public static TimeManager instance;

    void Awake() {
        instance = this;
    }

    #endregion

    private float timeScale = 1f;
	
    public void StopTime() {
        Time.timeScale = 0;
    }

    public void RestoreTime() {
        Time.timeScale = timeScale;
    }

    public void ResetTime() {
        timeScale = 1f;
        Time.timeScale = 1f;
    }

    public void SlowDownTime(float _timeScale) {
        timeScale = _timeScale;
        Time.timeScale = _timeScale;
    }
}
