﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickScript : MonoBehaviour {
    private GameObject player;
    private bool entered;
    public Text goal;
    public GameObject nextItem;
    // Use this for initialization
    void Start() {
        nextItem.GetComponent<BoxCollider>().enabled = false;
    }
    // Update is called once per frame
    void Update() {
        if (entered) {
            if (Input.GetKeyDown(KeyCode.E)) {
                Destroy(gameObject);
                GetNewGoal();
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        entered = true;
    }

    void OnTriggerExit(Collider other) {
        entered = false;

    }

    void GetNewGoal() {
        goal.text = "Find the hammer";
        nextItem.GetComponent<BoxCollider>().enabled = true;
    }
}
