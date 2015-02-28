using UnityEngine;
using System.Collections;

public class ParticleTimeScale : MonoBehaviour {

	public ParticleSystem particle;
	public float timeScale;
	public float timeAdjuster;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Time.timeScale = timeScale;
		particle.Simulate (timeScale/timeAdjuster, true, false);
	}
}
