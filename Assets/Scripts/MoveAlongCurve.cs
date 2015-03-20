using UnityEngine;
using System.Collections;

public class MoveAlongCurve : MonoBehaviour {

	public GameObject track;
	public CurveHandler trackScript;
	public CurveSegment segmentScript;
	public int speed = 1;
	public int segmentIndex = 0;
	public int positionIndex = 0;
	public int accum = 4;
	public int tempAccum;

	// Use this for initialization
	void Start () {
		segmentScript = track.GetComponent<CurveHandler> ().curve [segmentIndex].GetComponent<CurveSegment> ();
		trackScript = track.GetComponent<CurveHandler> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
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
      transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10.0f);
			positionIndex += speed;
			RealignTrainDirection(transform.position);

		}
		else {

			segmentIndex++;
			segmentScript = segmentScript = track.GetComponent<CurveHandler> ().curve [segmentIndex].GetComponent<CurveSegment> ();
			positionIndex = 1;

			if (segmentIndex == trackScript.curve.Count/2)
				if(!trackScript.disableHalfwayMark)
			{
				trackScript.halfwayMark = true;
				trackScript.disableHalfwayMark = true;
				Debug.Log (trackScript.halfwayMark);
			}

		}
	}

	void RealignTrainDirection (Vector3 curr)
	{
		transform.rotation = Quaternion.LookRotation(curr, Vector3.forward);
	}

	public void SwitchTracks (GameObject track, bool swtichFlag)
	{
		if (swtichFlag) {
			this.track = track;

			this.segmentIndex = 0;
			this.positionIndex = 0;
			this.segmentScript = track.GetComponent<CurveHandler> ().curve [segmentIndex].GetComponent<CurveSegment> ();
			this.trackScript = track.GetComponent<CurveHandler> ();

		}
	}

}
