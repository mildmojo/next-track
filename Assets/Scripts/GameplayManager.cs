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

    var minScale = 0.75f;
    if (overspeedSeverity > 0f) {
      var scale = 1 - overspeedSeverity * (1 - minScale);
      RedOverlay.transform.localScale = Vector3.Lerp(RedOverlay.transform.localScale, Vector3.one * scale, 0.3f);
      redOverlayGroup.alpha = Mathf.Lerp(redOverlayGroup.alpha, overspeedSeverity, 0.3f);
    } else {
      redOverlayGroup.alpha = 0f;
      RedOverlay.transform.localScale = Vector3.one;
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
    ExplosionSoundObject.GetComponent<AudioSource>().Play();
    LeanTween.delayedCall(3f, () => {
      Application.LoadLevel("mainmenu");
    });
  }

  public void SetOverspeed(float severity) {
    overspeedSeverity = severity;
    Debug.Log("overspeed severity: " + severity);
  }
}
