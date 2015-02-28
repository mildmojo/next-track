using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour {


    public enum GateDirection { LEFT, RIGHT };

    public GateDirection gateDirection = GateDirection.LEFT;
    public GateDirection previousDirection = GateDirection.LEFT;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (gateDirection == GateDirection.LEFT && previousDirection == GateDirection.RIGHT)
        {
           // Debug.Log("Switch Left");
        }
        if (gateDirection == GateDirection.RIGHT && previousDirection == GateDirection.LEFT)
        {
            //Debug.Log("Switch Right");
        }

        previousDirection = gateDirection;
	
	}
}
