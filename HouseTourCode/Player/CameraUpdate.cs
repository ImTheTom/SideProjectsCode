using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUpdate : MonoBehaviour {
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivy = 5.0f;
    public float smoothing = 2.0f;
    public Camera finalCam;
    public Camera fpsCam;

    GameObject character;
    // Use this for initialization
    void Start() {
        character = this.transform.parent.gameObject;
        finalCam.enabled = false;
        fpsCam.enabled = true;

    }

    // Update is called once per frame
    void Update() {
        var mouseMovementDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseMovementDelta = Vector2.Scale(mouseMovementDelta, new Vector2(sensitivy * smoothing, sensitivy * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, mouseMovementDelta.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseMovementDelta.y, 1f / smoothing);
        mouseLook += smoothV;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
