using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Options : MonoBehaviour {

	public GameObject destination1;
	public GameObject destination2;
	public GameObject destination3;
	public GameObject destination4;

	public GameObject mainSelected;
	public GameObject optionsSelected;
	public GameObject creditsSelected;
	public GameObject instructionsSelected;

	public Slider slider;
	public int switchTargets;

	private bool moving;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (moving && switchTargets == 0) {
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, new Vector3(destination1.transform.position.x, destination1.transform.position.y, -10.0f), Time.deltaTime*2);
			EventSystem.current.SetSelectedGameObject(mainSelected);
			if(Vector3.Distance(Camera.main.transform.position, destination1.transform.position) < 10.9f)
			{
				moving = false;

			}
		}

		if (moving && switchTargets == 1) {
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, new Vector3(destination2.transform.position.x, destination2.transform.position.y, -10.0f), Time.deltaTime*2);
			EventSystem.current.SetSelectedGameObject(optionsSelected);
			if(Vector3.Distance(Camera.main.transform.position, destination2.transform.position) < 10.9f)
			{
				moving = false;
			
		}
		}

		if (moving && switchTargets == 2) {
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, new Vector3(destination3.transform.position.x, destination3.transform.position.y, -10.0f), Time.deltaTime*2);
			EventSystem.current.SetSelectedGameObject(creditsSelected);
			if(Vector3.Distance(Camera.main.transform.position, destination3.transform.position) < 10.9f)
			{
				moving = false;
			
		}
		}

		if (moving && switchTargets == 3) {
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, new Vector3(destination4.transform.position.x, destination4.transform.position.y, -10.0f), Time.deltaTime*2);
			EventSystem.current.SetSelectedGameObject(instructionsSelected);
			if(Vector3.Distance(Camera.main.transform.position, destination4.transform.position) < 10.9f)
			{
				moving = false;
			
		}
		}
	}

	public void MoveCamera () {
		moving = true;
	}

	public void MainCounter() {
		switchTargets = 0;
	}

	public void OptionsCounter() {
		switchTargets = 1;
	}

	public void CreditsCounter(){
		switchTargets = 2;
	}

	public void InstructionsCounter(){
		switchTargets = 3;
	}

	public void AdjustVolume()
	{
		AudioListener.volume = slider.value / 10.0f;
	}
}
