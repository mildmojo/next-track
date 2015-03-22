using UnityEngine;
using UnityEngine.UI;

/*
 * Either get a reference to this component in the game manager or use
 * Send/BroadcastMessage to SetDuration, StartTimer, StopTimer, ResetTimer.
 *
 * On timer expiration, broadcasts TimerExpired message.
 */

public class TimerController : MonoBehaviour {

  public GameplayManager gameplayManager;

  private bool isRunning;
  private float duration;
  private float remaining;
  private float startedAt;
  private Text textComponent;

  // Use this for initialization
  void Start () {
    textComponent = GetComponent<Text>();
    gameplayManager = GameplayManager.Instance;
  }

  // Update is called once per frame
  void Update () {
    if (isRunning) {
      remaining -= Time.unscaledDeltaTime;
    }
    if (remaining <= 0 && isRunning) {
      isRunning = false;
      remaining = 0;
      gameplayManager.GameOver();
    }
    updateText();
  }

  public void SetDuration(float newDuration) {
    duration = newDuration;
  }

  public void AddTime(float seconds) {
    remaining += seconds;
  }

  public void StartTimer() {
    isRunning = true;
  }

  public void StopTimer() {
    isRunning = false;
  }

  public void ResetTimer() {
    textComponent.text = duration.ToString();
    remaining = duration;
  }

  public void PulseTimer() {
    LeanTween.scale(gameObject, Vector3.one * 1.05f, 0.2f)
      .setEase(LeanTweenType.easeInQuad);
    LeanTween.scale(gameObject, Vector3.one, 0.2f)
      .setEase(LeanTweenType.easeOutQuad)
      .setDelay(0.2f);
  }

  void updateText() {
    float time = remaining < 0 ? 0 : remaining;
    textComponent.text = Mathf.Round(time).ToString();
  }
}
