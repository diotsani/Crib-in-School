using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public GameObject puzzleManager;

    [Header("Timer UI references")]
    [SerializeField] private Image uiFillImage;
    [SerializeField] private Text uiText;

    public int Duration { get; private set; }

    public bool IsPaused { get; private set; }

    private int remainingDuration;

    private UnityAction<bool> onTimerPauseAction;

    private void Awake()
    {
        ResetTimer();
    }

    private void ResetTimer()
    {
        uiText.text = "00:00";
        uiFillImage.fillAmount = 0f;

        Duration = remainingDuration = 0;

        onTimerPauseAction = null;
        IsPaused = false;
    }

    public void SetPaused(bool paused)
    {
        IsPaused = paused;
        if (onTimerPauseAction != null)
        {
            onTimerPauseAction.Invoke(IsPaused);
        }
    }

    public Timer SetDuration(int seconds)
    {
        Duration = remainingDuration = seconds;
        return this;
    }

    public Timer OnPause (UnityAction<bool> action)
    {
        onTimerPauseAction = action;
        return this;
    }

    public void Begin()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while(remainingDuration > 0)
        {
            if (!IsPaused)
            {
                UpdateUI(remainingDuration);
                remainingDuration--;
            }  
            yield return new WaitForSeconds(1f);
        }
        End();
    }

    private void UpdateUI(int seconds)
    {
        uiText.text = string.Format("{0:D2} : {1:D2}", seconds / 60, seconds % 60);
        uiFillImage.fillAmount = Mathf.InverseLerp(0, Duration, seconds);
    }

    public void End()
    {
        ResetTimer();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
