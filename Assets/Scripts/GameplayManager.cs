using UnityEngine;
using System.Collections;

public class GameplayManager : MonoBehaviour {

  private int STARTING_CLOCK = 30;

  public GameplayManager Instance;
  public GameObject Player;

  private HudManager hudManager;

  void Awake() {
    Instance = this;
  }

  // Use this for initialization
  void Start () {
    hudManager = HudManager.Instance;
    hudManager.timerController.AddTime(STARTING_CLOCK);
    hudManager.Show().setOnComplete(() => {
      hudManager.timerController.StartTimer();
      Player.SetActive(true);
    });
  }

  // Update is called once per frame
  void Update () {

  }
}
