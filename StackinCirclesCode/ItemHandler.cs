using System;
using UnityEngine;

// This class handles the interaction of the circles after being launched
public class ItemHandler : MonoBehaviour {

	public GameObject currentCircleBeingLaunched;
    private Rigidbody currentCircleBeingLaunchedRigidbody;
    private bool currentCircleHasRigidbody = false;
    private bool isGameRunning = true;
    public double timeAliveFor = 0;

    private const float VELOCITYTHRESHOLD = 0.5f;
    private const double YPOSITIONTHRESHOLD = 0.5;
    private const double YPOSITIONTHRESHOLDUNDERTIMETHRESHOLD = 1.5;
    private const double TIMEALIVETHRESHOLD = 0.1;

    void Update () {
        if (currentCircleHasRigidbody && isGameRunning) {
            if (timeAliveFor < TIMEALIVETHRESHOLD) {
                timeAliveFor += Time.deltaTime;
            }
            if (currentCircleBeingLaunched.transform.position.y < YPOSITIONTHRESHOLDUNDERTIMETHRESHOLD && timeAliveFor < TIMEALIVETHRESHOLD) {
				isGameRunning = false;
                Score.RunEndGameFunctions();
            } else if(currentCircleBeingLaunched.transform.position.y < YPOSITIONTHRESHOLD) {
                isGameRunning = false;
                Score.RunEndGameFunctions();
            }
            if (Math.Abs(currentCircleBeingLaunchedRigidbody.velocity.x) < VELOCITYTHRESHOLD) {
                StopCurrentCircleFromRolling();
            }
		}
    }

    private void StopCurrentCircleFromRolling() {
        currentCircleBeingLaunchedRigidbody.velocity = new Vector3(0, currentCircleBeingLaunchedRigidbody.velocity.y);
        currentCircleBeingLaunchedRigidbody.angularVelocity = new Vector3(0, currentCircleBeingLaunchedRigidbody.angularVelocity.y);
    }

    // Handles how the circle ineracts with other objects in the game
	private void OnCollisionEnter(Collision otherObject) {
		if (otherObject.gameObject.tag == "Ground" && isGameRunning) {
			isGameRunning = false;
            Score.RunEndGameFunctions();
        }
        if (otherObject.gameObject.tag == "Ball" || otherObject.gameObject.tag == "Pillar" && isGameRunning) {
            AttachCircleToOtherObject(otherObject);
        }
    }

    // This function attaches a fixed joint to the other object it had collided with
    private void AttachCircleToOtherObject(Collision otherObject) {
        currentCircleBeingLaunchedRigidbody.velocity = new Vector3(0, 0);
        currentCircleBeingLaunchedRigidbody.angularVelocity = new Vector3(0, 0);
        otherObject.gameObject.AddComponent<FixedJoint>();
        otherObject.gameObject.GetComponent<FixedJoint>().connectedBody = currentCircleBeingLaunchedRigidbody;
    }

    // This function sets the variables for the circle after being launched which are used throughout the class.
    // This function is called from Launch.cs
	public void InstaniateItemHandlerVariables(GameObject currentObject) {
		currentCircleBeingLaunched = currentObject;
		currentCircleBeingLaunchedRigidbody = GetComponent<Rigidbody>();
		currentCircleHasRigidbody = true;
	}

	public bool GetGameRunning() {
		return isGameRunning;
	}
}
