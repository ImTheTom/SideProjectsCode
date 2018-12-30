using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    //Sets the variables outside so they aren't constantly set in start or update
    private float screenHeight = 0f;
    private float screenWidth = 0f;
    private float moveToRight = 0f;
    private float moveToLeft = 0f;
    private float moveToUp = 0f;
    private float moveToDown = 0f;
    private float middleOfScreenWidth = 0f;
    private float eigthOfScreenWidth = 0f;
    private float eigthOfScreenHeight = 0f;
    private bool rotating = false;
    private float maxSpeed = 25f;
    private float rotateSpeed = 60f;
    private float percentage = 0;
    private float inputX = 0f;
    private float inputY = 0f;
    private Vector3 cameraPos;
    private Terrain terrain;
    private Vector3 terrainSize;
    private Vector3 mouseScreenPosition;
    private Camera cam;
    private GameObject terrainEditor;

    // Use this for initialization
    void Start() {
        //Finds the screen height and width and the points where the camera will move
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        middleOfScreenWidth = screenWidth / 2;
        eigthOfScreenWidth = middleOfScreenWidth / 4;
        eigthOfScreenHeight = (screenHeight/2)/ 4;
        moveToRight = screenWidth - eigthOfScreenWidth;
        moveToLeft = eigthOfScreenWidth;
        moveToUp = screenHeight - eigthOfScreenHeight;
        moveToDown = eigthOfScreenHeight;
        //Finds the terrain and gets the size of it
        var go = GameObject.Find("Land");
        terrain = go.GetComponent<Terrain>();
        terrainSize = terrain.terrainData.size;
        //Gets the camera
        cam = this.GetComponent<Camera>();
        terrainEditor = GameObject.Find("TerrainEditor");
        //terrainEditor.GetComponent<TerrainEditor>().enabled = false;
    }

    private void OnGUI() {
        if (GUI.Button(new Rect(middleOfScreenWidth-50, (screenHeight/2)-30, 100, 20), "Edit Terrain")) {
            TurnOff();
        }
    }

    // Update is called once per frame
    void Update() {
        //Gets the camera pos and the mouse position
        cameraPos = transform.position;
        mouseScreenPosition = Input.mousePosition;
        //Checks to see if rotation will occur due to the right mouse button
        if (Input.GetMouseButtonDown(1)) {
            rotating = true;
        }else if (Input.GetMouseButtonUp(1)) {
            rotating = false;
        }
        //Sees how the camera will rotate if the left control is clicked and not the right mouse button
        //The points where the screen rotate has been determined previously
        if (Input.GetKey(KeyCode.LeftControl) && !rotating) {
            if (mouseScreenPosition.x > moveToRight) {
                //The closer it is to the edge the faster it will move
                percentage = (mouseScreenPosition.x - moveToRight) / eigthOfScreenWidth;
                if (percentage > 1) {
                    percentage = 1;
                }
                //Rotate the camera
                transform.Rotate(0, 1 * (maxSpeed * percentage) * Time.deltaTime, 0, Space.Self);
            } else if (mouseScreenPosition.x < moveToLeft) {
                percentage = Mathf.Abs(mouseScreenPosition.x - moveToLeft) / eigthOfScreenWidth;
                if (percentage > 1) {
                    percentage = 1;
                }
                transform.Rotate(0, -1 * (maxSpeed * percentage) * Time.deltaTime, 0, Space.Self);
            } else if (mouseScreenPosition.y > moveToUp && transform.eulerAngles.x < 80 || transform.eulerAngles.x > 300) {
                percentage = (mouseScreenPosition.y - moveToUp) / eigthOfScreenWidth;
                if (percentage > 1) {
                    percentage = 1;
                }
                transform.Rotate(-1 * (maxSpeed * percentage) * Time.deltaTime, 0, 0, Space.Self);
            } else if (mouseScreenPosition.y < moveToDown && transform.eulerAngles.x < 80 || transform.eulerAngles.x > 300) {
                percentage = Mathf.Abs(mouseScreenPosition.y - moveToDown) / eigthOfScreenWidth;
                if (percentage > 1) {
                    percentage = 1;
                }
                transform.Rotate(1 * maxSpeed * Time.deltaTime, 0, 0, Space.Self);
            } //Resets the camera so it can't go past 90 or 270
            else if (mouseScreenPosition.y < moveToDown && transform.eulerAngles.x < 100) {
                transform.eulerAngles = new Vector3(79, transform.eulerAngles.y);
            } else if (mouseScreenPosition.y > moveToUp && transform.eulerAngles.x > 270) {
                transform.eulerAngles = new Vector3(301, transform.eulerAngles.y);
            }
            //If the camera isn't rotating and the mouse moves to the positions then the camera moves
        } else if (!rotating) {
            if (mouseScreenPosition.x > moveToRight) {
                // closer to the edge of the screen the faster it will move position
                percentage = (mouseScreenPosition.x - moveToRight) / eigthOfScreenWidth;
                if (percentage > 1) {
                    percentage = 1;
                }
                //move the camera
                this.transform.Translate(Vector3.right * (maxSpeed * percentage) * Time.deltaTime, Space.Self);
            } else if (mouseScreenPosition.x < moveToLeft) {
                percentage = Mathf.Abs(mouseScreenPosition.x - moveToLeft) / eigthOfScreenWidth;
                if (percentage > 1) {
                    percentage = 1;
                }
                this.transform.Translate(Vector3.left * (maxSpeed * percentage) * Time.deltaTime, Space.Self);
            }
            if (mouseScreenPosition.y > moveToUp) {
                percentage = (mouseScreenPosition.y - moveToUp) / eigthOfScreenWidth;
                if (percentage > 1) {
                    percentage = 1;
                }
                this.transform.Translate(new Vector3(0,1,1) * (maxSpeed * percentage) * Time.deltaTime, Space.Self);
            } else if (mouseScreenPosition.y < moveToDown) {
                percentage = Mathf.Abs(mouseScreenPosition.y - moveToDown) / eigthOfScreenWidth;
                if (percentage > 1) {
                    percentage = 1;
                }
                this.transform.Translate(new Vector3(0, -1, -1) * (maxSpeed * percentage) * Time.deltaTime, Space.Self);
            }
        //If the rotating is true this will occur
        } else if (rotating) {
            //Shouldn't move if the camera is past this positions
            if (transform.eulerAngles.x < 80 || transform.eulerAngles.x > 300) {
                //Rotate the camera based on the mouse axis
                inputX = -Input.GetAxis("Mouse X");
                inputY = -Input.GetAxis("Mouse Y");
                transform.rotation *= Quaternion.AngleAxis(inputX * (rotateSpeed * Time.deltaTime), Vector3.up);
                transform.rotation *= Quaternion.AngleAxis(inputY * (rotateSpeed * Time.deltaTime), Vector3.right);
            }//Resets the camera
            else if(transform.eulerAngles.x < 100) {
                transform.eulerAngles = new Vector3(79, transform.eulerAngles.y);
            }else if(transform.eulerAngles.x > 270) {
                transform.eulerAngles = new Vector3(301, transform.eulerAngles.y);
            }
        }
        //Zoom in based on scroll wheel, but not past certain positions
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > 4) {
            this.transform.Translate(new Vector3(0, 0, 1f) * maxSpeed * Time.deltaTime, Space.Self);
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y < 25) {
            this.transform.Translate(new Vector3(0, 0, -1f) * maxSpeed * Time.deltaTime, Space.Self);
        }
        //sets the z angle to 0
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }

    private void TurnOff(){
        terrainEditor.GetComponent<JustTerrainEditor>().enabled = true;
        transform.GetComponent<CameraMovement>().enabled = false;
    }
}
