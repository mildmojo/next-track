using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

	public GameObject destination1;
	public GameObject destination2;
	private bool switchTargets;

	private bool moving;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (moving && switchTargets) {
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, new Vector3(destination1.transform.position.x, destination1.transform.position.y, -10.0f), Time.deltaTime*2);
			if(Vector3.Distance(Camera.main.transform.position, destination1.transform.position) < 10.9f)
				moving = false;
		}
		if (moving && !switchTargets) {
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, new Vector3(destination2.transform.position.x, destination2.transform.position.y, -10.0f), Time.deltaTime*2);
			if(Vector3.Distance(Camera.main.transform.position, destination2.transform.position) < 10.9f)
				moving = false;
		}
	}

	public void MoveCamera () {
		moving = true;
		switchTargets = !switchTargets;
	}
}
