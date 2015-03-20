using UnityEngine;
using System.Collections;

public class MoveAlongCurve : MonoBehaviour {

	public GameObject track;
	[HideInInspector]
	public CurveHandler trackScript;
	[HideInInspector]
	public CurveSegment segmentScript;
	public int speed = 1;
	public int segmentIndex = 0;
	public int positionIndex = 0;
	public int accum = 4;
	public int tempAccum;

	private GameplayManager gameplayManager;
	private HudManager hudManager;
	private int segmentsTraveled = 0;

	// Use this for initialization
	void Start () {
		var curve = track.GetComponent<CurveHandler> ().curve;
		segmentScript = curve[segmentIndex].gameObject.GetComponent<CurveSegment> ();
		trackScript = track.GetComponent<CurveHandler> ();
		hudManager = HudManager.Instance;
		gameplayManager = GameplayManager.Instance;
	}

	void FixedUpdate () {
		MoveForward(speed);
		hudManager.SetDistance(segmentsTraveled);
	}


	void MoveForward (int distance)
	{
		if (distance < 0.1f) return;

		if (positionIndex < segmentScript.curveSegment.Count) {
			transform.position = segmentScript.curveSegment [positionIndex];
      transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10.0f);
			positionIndex += distance;
			segmentsTraveled += distance;
			RealignTrainDirection(transform.position);
			CheckSpeed();
		}
		else {
			// Darken visited track segments
			var curve = track.GetComponent<CurveHandler> ().curve;
			var lineRenderer = curve[segmentIndex].gameObject.GetComponent<LineRenderer>();
			var oldColor = lineRenderer.material.color;
			LeanTween.value(track, (color) => { lineRenderer.material.color = color; }, oldColor, new Color(0.1f, 0.1f, 0.1f, 0.9f) , 0.75f);

			segmentIndex++;
			segmentScript = track.GetComponent<CurveHandler> ().curve[segmentIndex].gameObject.GetComponent<CurveSegment> ();
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
			this.segmentScript = track.GetComponent<CurveHandler> ().curve[segmentIndex].gameObject.GetComponent<CurveSegment> ();
			this.trackScript = track.GetComponent<CurveHandler> ();

		}
	}

	void CheckSpeed() {
		var pctOfMaxSpeed = Time.timeScale / (gameplayManager.MinSpeed + trackScript.curve[segmentIndex].speed);
		var pctSegmentPos = (float) positionIndex / segmentScript.curveSegment.Count;
		var pctWarning = 0.7f;
		// If within 30% of top speed for segment, show vignette and add squeal up to 100%
		// If overspeed and over 0.8 along segment, blow up
Debug.Log("pctOfMaxSpeed " + pctOfMaxSpeed + ", pctSegmentPos " + pctSegmentPos);
		if (pctOfMaxSpeed > pctWarning) {
			var severity = Mathf.Clamp01((pctOfMaxSpeed - pctWarning) / (1 - pctWarning));
			gameplayManager.SetOverspeed(severity);
		} else {
			gameplayManager.SetOverspeed(0f);
		}

		if (pctOfMaxSpeed > 1.0f && pctSegmentPos > 0.8f) {
			gameplayManager.GameOver();
		}
	}

}
