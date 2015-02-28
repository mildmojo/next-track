using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

  public static AudioManager Instance;

  public List<string> SoundNames;
  public List<AudioClip> SoundClips;

  private AudioSource audioSource;

  void Awake() {
    Instance = this;
    audioSource = GetComponent<AudioSource>();
  }

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  public void Play(string soundName) {
    var soundIdx = SoundNames.IndexOf(soundName);
    audioSource.clip = SoundClips[soundIdx];
    audioSource.Play();
  }
}
