using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour {
    public GameObject player;
    public Camera camera;
    public Text goal;
    void OnTriggerEnter(Collider other) {
        camera.enabled = true;
        goal.text = "Fin";
        DestroyObject(player);
    }
}
