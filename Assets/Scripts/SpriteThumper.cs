using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent (typeof (AudioSource))]
public class SpriteThumper : MonoBehaviour {

  public List<string> tagWhitelist;
  public AudioClip thumpAudioClip;

  private bool isDying;

  void Update () {
  }

  void OnTriggerEnter(Collider c) {
    if (!isDying && tagWhitelist.Any(c.gameObject.CompareTag)) {
      isDying = true;
      DoThump();
    }
  }

  void DoThump() {
    LeanTween
      .scale(gameObject, Vector3.one * 2f, 0.3f)
      .setOnComplete(() => {
        Destroy(this);
      });

    LeanTween.alpha(gameObject, 0, 0.3f).setEase(LeanTweenType.easeInExpo);

    if (thumpAudioClip) {
      var source = Camera.main.gameObject.AddComponent<AudioSource>();
      source.clip = thumpAudioClip;
      source.Play();
      Destroy(source, thumpAudioClip.length);
    }
  }
}
