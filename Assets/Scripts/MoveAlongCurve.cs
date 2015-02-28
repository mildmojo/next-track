using UnityEngine;
using System.Collections;

public class MoveAlongCurve : MonoBehaviour {

	public GameObject curve;
	public CurveSegment segmentScript;
	public int speed = 1;
	private int segmentIndex = 0;
	private int positionIndex = 0;

	// Use this for initialization
	void Start () {
		segmentScript = curve.GetComponent<CurveHandler> ().curve [segmentIndex].GetComponent<CurveSegment> ();
	}

	// Update is called once per frame
	void Update () {
		//if(Input.GetKeyDown(KeyCode.RightArrow))
		if (true)
		{
			MoveForward(speed);
			//segmentScript = curve.GetComponent<CurveSegment> ();

		}
	}


	void MoveForward (int distnce)
	{
		if (positionIndex < segmentScript.curveSegment.Count) {
		transform.position = segmentScript.curveSegment [positionIndex];
		positionIndex += speed;
		}
		else {

			segmentIndex++;
			segmentScript = curve.GetComponent<CurveHandler> ().curve [segmentIndex].GetComponent<CurveSegment> ();
			positionIndex = 1;
		}
	}

	void RealignTrainDirection (int currentCurveIndex)
	{
		//transform.rotation = Quaternion.LookRotation(new Vector3(CurveHandler.curve[currentCurveIndex+1] - CurveHandler.curve[currentCurveIndex-1]));
	}

}
