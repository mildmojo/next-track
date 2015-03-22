using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour {

  private int STARTING_CLOCK = 30;

  public static GameplayManager Instance;
  public GameObject Player;
  public GameObject particleExplosion;
  public GameObject ExplosionSoundObject;
  public Image RedOverlay;
  public float MinSpeed;
  public float MaxSpeed;

  private AudioSource trainSound;
  private HudManager hudManager;
  private CanvasGroup redOverlayGroup;
  private float overspeedSeverity;
  private bool isGameOver;

  void Awake() {
    Instance = this;
    trainSound = GetComponent<AudioSource>();
    redOverlayGroup = RedOverlay.GetComponent<CanvasGroup>();
    redOverlayGroup.alpha = 0f;
  }

  // Use this for initialization
  void Start () {
    hudManager = HudManager.Instance;
    hudManager.timerController.AddTime(STARTING_CLOCK);
    hudManager.Show().setOnComplete(() => {
      hudManager.timerController.StartTimer();
      Player.GetComponent<MoveAlongCurve>().speed = 1;
      trainSound.Play();
    });
  }

  // Update is called once per frame
  void Update () {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Application.LoadLevel("mainmenu");
    }

    // Show red vision overlay if player's moving too fast.
    var minRedScale = 0.75f;
    if (overspeedSeverity > 0f) {
      var scale = 1 - overspeedSeverity * (1 - minRedScale);
      RedOverlay.transform.localScale = Vector3.Lerp(RedOverlay.transform.localScale, Vector3.one * scale, 1f);
      redOverlayGroup.alpha = Mathf.Lerp(redOverlayGroup.alpha, overspeedSeverity, 1f);
    } else {
      redOverlayGroup.alpha = Mathf.Lerp(redOverlayGroup.alpha, 0f, 1f);
      RedOverlay.transform.localScale = Vector3.Lerp(RedOverlay.transform.localScale, Vector3.one, 1f);
    }

    if (isGameOver) {
      if (Input.anyKeyDown) {
        Application.LoadLevel("mainmenu");
      }
    }
  }

  public void GameOver() {
    // TODO: Show game over.
    // Application.LoadLevel("mainmenu");
    var explosion = Instantiate(particleExplosion) as GameObject;
    explosion.transform.position = Player.transform.position;
    explosion.transform.parent = Player.transform.parent;
    Player.GetComponent<MoveAlongCurve>().speed = 0;
    Time.timeScale = 1;
    trainSound.Stop();
    hudManager.timerController.StopTimer();
    ExplosionSoundObject.GetComponent<AudioSource>().Play();
    LeanTween.value(gameObject, severity => overspeedSeverity = severity, overspeedSeverity, 0f, 1f)
      .setDelay(2f);
    LeanTween.value(Camera.main.gameObject, val => Camera.main.orthographicSize = val, Camera.main.orthographicSize, 500f, 2f)
      .setEase(LeanTweenType.easeInOutExpo)
      .setDelay(2f);
    LeanTween.delayedCall(2f, () => isGameOver = true);
    LeanTween.delayedCall(10f, () => {
      Application.LoadLevel("mainmenu");
    });
  }

  public void SetOverspeed(float severity) {
    overspeedSeverity = severity;
// Debug.Log("overspeed severity: " + severity);
  }

  public void AddTime(float seconds) {
    hudManager.AddTime(seconds);
  }
}
