using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleGate : MonoBehaviour {

    public AudioSource audioSourceOne;
    public AudioSource audioSourceTwo;

	// Update is called once per frame
	void Update () {

        if (Input.GetAxisRaw("ToggleGate") < -0.8)
        {
//            GameObject[] trackGates;
//            trackGates = GameObject.FindGameObjectsWithTag("Gate");
//
//            foreach (GameObject go in trackGates)
//            {
//                go.GetComponent<Gate>().gateDirection = Gate.GateDirection.LEFT;
//                go.transform.parent.localEulerAngles = new Vector3(0.0f, 0.0f, 30.0f);
//            }

            audioSourceOne.volume = 1;
            audioSourceTwo.volume = 0;
        }
        if (Input.GetAxisRaw("ToggleGate") > 0.8)
        {
//            GameObject[] trackGates;
//            trackGates = GameObject.FindGameObjectsWithTag("Gate");
//
//            foreach (GameObject go in trackGates)
//            {
//                go.GetComponent<Gate>().gateDirection = Gate.GateDirection.RIGHT;
//                go.transform.parent.localEulerAngles = new Vector3(0.0f, 0.0f, 330.0f);
//            }
            
            audioSourceTwo.volume = 1;
            audioSourceOne.volume = 0;
        }
        
	
	}
}
