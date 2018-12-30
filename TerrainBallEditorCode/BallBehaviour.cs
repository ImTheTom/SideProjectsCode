using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    public float thrust = 1000f;
    private float jumpThrust = 250f;
    private Rigidbody rb;

    private float time = 0f;
    private bool showTime = false;
    private bool countTime = false;
    private bool rolling = false;
    private float maxJumps = 3;

    private GameObject goal;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        goal = GameObject.Find("Goal");
    }

    // Update is called once per frame
    void Update() {
        if (countTime) {
            time += Time.deltaTime;
        }
        if (rolling) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Destroy(gameObject);
            }
            var vel = rb.velocity;
            if (vel.z == 0 && time > 0.5f) {
                rolling = false;
                goal.GetComponent<GoalScript>().BallStopped();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (maxJumps > 0 && rolling) {
                this.rb.AddForce(new Vector3(0, 1, 0) * jumpThrust);
                maxJumps -= 1;
            }
        }
        if (transform.position.y < -5) {
            Destroy(gameObject);
        }
    }

    private void OnGUI() {
        if (showTime) {
            GUI.Label(new Rect(0, 0, 100, 20), time.ToString("0.00"));
        }
    }

    public void Roll(float percent) {
        rb = this.GetComponent<Rigidbody>();
        if (percent > 2.5) {
            percent = 2.5f;
        } else if (percent < 0.5) {
            percent = 0.5f;
        }
        this.rb.AddForce(new Vector3(0, 0, -1) * (thrust * percent));
        countTime = true;
        showTime = true;
        rolling = true;
    }

    public void StopTimer() {
        countTime = false;
    }

}
