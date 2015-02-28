using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HudManager : MonoBehaviour {

  public static HudManager Instance;

  public GameObject timerHandle;
  public GameObject velocityHandle;
  public Text timerText;
  public Text velocityText;
  public Slider velocitySlider;

  private float origTimerHandleX;
  private float origVelocityHandleX;
  private float offscreenTimerHandleX;
  private float offscreenVelocityHandleX;
  public TimerController timerController;
  // private VelocitySliderController velocityController;

  void Awake() {
    Instance = this;

    // origTimerHandleX = timerHandle.transform.position.x;
    // origVelocityHandleX = velocityHandle.transform.position.x;
    origTimerHandleX = timerHandle.transform.localPosition.x;
    origVelocityHandleX = velocityHandle.transform.localPosition.x;

    offscreenTimerHandleX = origTimerHandleX + Screen.width;
    offscreenVelocityHandleX = origVelocityHandleX - Screen.width;

    timerController = timerText.GetComponent<TimerController>();
    // velocityController = velocitySlider.GetComponent<VelocitySliderController>();
  }

  void Start () {
    timerHandle.transform.position = new Vector2(offscreenTimerHandleX,
      timerHandle.transform.position.y);
    velocityHandle.transform.position = new Vector2(offscreenVelocityHandleX,
      velocityHandle.transform.position.y);
  }

  void Update () {
  }

  public LTDescr Show() {
    LeanTween.moveLocalX(velocityHandle, origVelocityHandleX, 1f).setEase(LeanTweenType.easeOutSine);
    return LeanTween.moveLocalX(timerHandle, origTimerHandleX, 1f).setEase(LeanTweenType.easeOutSine);
  }

  public LTDescr Hide() {
    LeanTween.moveLocalX(velocityHandle, offscreenVelocityHandleX, 1f).setEase(LeanTweenType.easeInSine);
    return LeanTween.moveLocalX(timerHandle, offscreenTimerHandleX, 1f).setEase(LeanTweenType.easeInSine);
  }

  public void AddTime(float seconds) {
    timerController.AddTime(seconds);
  }
}

