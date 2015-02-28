using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class SpeedControl : MonoBehaviour {

    public AudioSource audioSourceOne;
    public AudioSource audioSourceTwo;

    //Project Input Settings needed - Right Stick is now Horizontal and Vertical. Left Stick is ToggleGate.

    private static string[] TURN_INPUTS = new string[] { "up", "down", "left", "right" };
    private KeyCombo[] LeftTurns;
    private KeyCombo[] RightTurns;

    public float currentSpeed;
    public float accelRate;
	// Use this for initialization
	void Start () 
    {
        LeftTurns = new KeyCombo[] {
      new KeyCombo("Horizontal", "Vertical", new string[] {"up", "left", "down"}, TURN_INPUTS),
      new KeyCombo("Horizontal", "Vertical", new string[] {"down", "right", "up"}, TURN_INPUTS)
    };

        RightTurns = new KeyCombo[] {
      new KeyCombo("Horizontal", "Vertical", new string[] {"up", "right", "down"}, TURN_INPUTS),
      new KeyCombo("Horizontal", "Vertical", new string[] {"down", "left", "up"}, TURN_INPUTS)
    };
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        foreach (KeyCombo turn in RightTurns.Concat(LeftTurns))
        {
            turn.HorizontalAxis = "Horizontal";
            turn.VerticalAxis = "Vertical";
        }

        if (RightTurns.Any(turn => turn.Check()))
        {
            currentSpeed -= accelRate;
        }
        if (LeftTurns.Any(turn => turn.Check()))
        {
            currentSpeed += accelRate;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, .5f, 1.5f);

        Time.timeScale = currentSpeed;
        audioSourceOne.pitch = currentSpeed;
        audioSourceTwo.pitch = currentSpeed;
	
	}

    void FixedUpdate()
    {
        this.transform.position += new Vector3(0, .1f, 0);
    }
}
