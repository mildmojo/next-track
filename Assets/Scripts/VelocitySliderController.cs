using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VelocitySliderController : MonoBehaviour {

  public Text velocityText;

  private Slider slider;

  void Awake() {
    slider = GetComponent<Slider>();
  }

  // Use this for initialization
  void Start () {

  }

  // Update is called once per frame
  void Update () {
    velocityText.text = Mathf.Round(Time.timeScale * 100) + " kph";
    slider.value = Time.timeScale;
  }
}
