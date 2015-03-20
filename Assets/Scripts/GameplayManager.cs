using UnityEngine;
using System.Collections;

public class GameplayManager : MonoBehaviour {

  private int STARTING_CLOCK = 30;

  public static GameplayManager Instance;
  public GameObject Player;
  public GameObject particleExplosion;
  public GameObject ExplosionSoundObject;

  private AudioSource trainSound;
  private HudManager hudManager;

  void Awake() {
    Instance = this;
    trainSound = GetComponent<AudioSource>();
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
  }

  public void TimerExpired() {
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
}
