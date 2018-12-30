using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainEditor : MonoBehaviour {
    private float screenHeight = 0f;
    private float screenWidth = 0f;

    private float middleOfScreenHeight = 0f;
    private Terrain terrain;
    private Vector3 mouseScreenPosition;
    public Texture circleTexture;
    public Texture squareTexture;
    private GameObject mainCamera;

    public Texture sand;
    public Texture grass;


    public Slider mainSlider;

    private bool changeHeight = true;

    private bool holdHeight = false;

    private Terrain terr; // terrain to modify
    private int hmWidth; // heightmap width 
    private int hmHeight; // heightmap height

    private int posXInTerrain; // position of the game object in terrain width (x axis)
    private int posYInTerrain; // position of the game object in terrain height (z axis)

    private int size = 50; // the diameter of terrain portion that will raise under the game object 
    private float desiredHeight = 0; // the height we want that portion of terrain to be

    private float speed = 50;

    private float[,] originalHeights;
    Vector3 newPosition;

    enum Way {
        Up,
        Down
    };

    enum Brush {
        Square,
        Circle
    };

    enum WhatChange {
        Height,
        Ground
    };

    private WhatChange whatChange;

    private Way way;

    private Brush brush;
    // Use this for initialization
    void Start() {
        var go = GameObject.Find("Land");
        terrain = go.GetComponent<Terrain>();
        terr = Terrain.activeTerrain;
        mouseScreenPosition = Input.mousePosition;
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        middleOfScreenHeight = screenHeight / 2;
        mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraMovement>().enabled = false;
        hmWidth = terrain.terrainData.heightmapWidth;
        hmHeight = terrain.terrainData.heightmapHeight;
        this.originalHeights = this.terrain.terrainData.GetHeights(0, 0, this.terrain.terrainData.heightmapWidth, this.terrain.terrainData.heightmapHeight);
        brush = Brush.Circle;
        whatChange = WhatChange.Ground;
    }

    // Update is called once per frame
    void Update() {
        if (whatChange == WhatChange.Height) {
            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                holdHeight = true;
            } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
                holdHeight = false;
            }
            if (Input.GetMouseButtonUp(0)) {
                changeHeight = true;
            } else if (Input.GetMouseButtonUp(1)) {
                changeHeight = true;
            }
            if (Input.GetMouseButton(0)) {
                way = Way.Up;
                Change();
                changeHeight = false;
            } else if (Input.GetMouseButton(1)) {
                way = Way.Down;
                Change();
                changeHeight = false;
            }

            if (changeHeight && !holdHeight) {
                desiredHeight = Terrain.activeTerrain.SampleHeight(transform.position);
                desiredHeight = desiredHeight / 5;
                if (desiredHeight > 1) {
                    desiredHeight = 1f;
                }
            }
        } else if (whatChange == WhatChange.Ground) {
            if (Input.GetMouseButton(0)) {
                if (brush == Brush.Square) {
                    SquareTerrainGround();
                } else if (brush == Brush.Circle) {
                    CircleTerrainGround();
                }
            }
        }
        mouseScreenPosition = Input.mousePosition;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            newPosition = hit.point;
            transform.position = newPosition;
        }
    }

    private void OnGUI() {
        if (brush == Brush.Square) {
            GUI.DrawTexture(new Rect(mouseScreenPosition.x - size / 2, -(mouseScreenPosition.y - screenHeight) - size / 2, size, size), squareTexture);
        } else if (brush == Brush.Circle) {
            GUI.DrawTexture(new Rect(mouseScreenPosition.x - size / 2, -(mouseScreenPosition.y - screenHeight) - size / 2, size, size), circleTexture);
        }
        GUI.Label(new Rect(0, 20, 100, 20), "Brush Size : " + size);
        GUI.Label(new Rect(0, 50, 100, 20), "Brush Shape");
        size = (int)GUI.HorizontalSlider(new Rect(0, 40, 100, 10), size, 1.0f, 100.0f);
        if (GUI.Button(new Rect(0, 90, 100, 20), "Square")) {
            brush = Brush.Square;
        } else if (GUI.Button(new Rect(0, 70, 100, 20), "Circle")) {
            brush = Brush.Circle;
        } else if (GUI.Button(new Rect(screenWidth - 100, 0, 100, 20), "Back")) {
            TurnOff();
        } else if (GUI.Button(new Rect(0, 0, 100, 20), "Change Height")) {
            whatChange = WhatChange.Height;
        } else if (GUI.Button(new Rect(105, 0, 100, 20), "Change Ground")) {
            whatChange = WhatChange.Ground;
        }
        if(whatChange == WhatChange.Ground) {
            GUI.Label(new Rect(0, 110, 100, 20), "Ground");

            if (GUI.Button(new Rect(0, 130, 100, 20), "Grass")) {
                Debug.Log("yes");
            } else if (GUI.Button(new Rect(0, 150, 100, 20), "Sand")) {
                Debug.Log("yes2");
            }
        }
    }

    private void Change() {
        if (brush == Brush.Square) {
            SquareTerrain();
        } else if (brush == Brush.Circle) {
            CircleTerrain();
        }
    }

    private void SquareTerrain() {
        try {
            // get the normalized position of this game object relative to the terrain
            Vector3 tempCoord = (transform.position - terr.gameObject.transform.position);
            Vector3 coord;
            coord.x = tempCoord.x / terr.terrainData.size.x;
            coord.y = tempCoord.y / terr.terrainData.size.y;
            coord.z = tempCoord.z / terr.terrainData.size.z;
            // get the position of the terrain heightmap where this game object is
            posXInTerrain = (int)(coord.x * hmWidth);
            posYInTerrain = (int)(coord.z * hmHeight);
            // we set an offset so that all the raising terrain is under this game object
            int offset = size / 2;
            // get the heights of the terrain under this game object
            float[,] heights = terr.terrainData.GetHeights(posXInTerrain - offset, posYInTerrain - offset, size, size);
            // we set each sample of the terrain in the size to the desired height
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    if (way == Way.Up) {
                        if (heights[i, j] < desiredHeight) {
                            heights[i, j] = desiredHeight;
                        }
                    } else if (way == Way.Down) {
                        if (heights[i, j] > desiredHeight) {
                            heights[i, j] = desiredHeight;

                        }
                    }
                }
            }
            if (way == Way.Up && !holdHeight) {
                // go raising the terrain slowly
                desiredHeight += Time.deltaTime * speed / 100;
            } else if (way == Way.Down && !holdHeight) {
                desiredHeight -= Time.deltaTime * speed / 100;
                if (desiredHeight < 0) {
                    desiredHeight = 0;
                }
            }
            // set the new height
            terr.terrainData.SetHeights(posXInTerrain - offset, posYInTerrain - offset, heights);
        } catch {
            Debug.Log("Clicked Outside Of Terrain");
        }
    }

    private void CircleTerrain() {
        size = size * 2;
        try {
            // get the normalized position of this game object relative to the terrain
            Vector3 tempCoord = (transform.position - terr.gameObject.transform.position);
            Vector3 coord;
            coord.x = tempCoord.x / terr.terrainData.size.x;
            coord.y = tempCoord.y / terr.terrainData.size.y;
            coord.z = tempCoord.z / terr.terrainData.size.z;
            // get the position of the terrain heightmap where this game object is
            posXInTerrain = (int)(coord.x * hmWidth);
            posYInTerrain = (int)(coord.z * hmHeight);
            // we set an offset so that all the raising terrain is under this game object
            int offset = size / 2;
            // get the heights of the terrain under this game object
            float[,] heights = terr.terrainData.GetHeights(posXInTerrain - offset, posYInTerrain - offset, size, size);
            int halfSize = size / 2;
            int radius = halfSize / 2;
            for (float y = -radius; y <= radius; y++) {
                for (float x = -radius; x <= radius; x++) {
                    if (x * x + y * y <= radius * radius) {
                        if (way == Way.Up) {
                            if (heights[(int)x + halfSize, (int)y + halfSize] < desiredHeight) {
                                heights[(int)x + halfSize, (int)y + halfSize] = desiredHeight;
                            }
                        } else if (way == Way.Down) {
                            if (heights[(int)x + halfSize, (int)y + halfSize] > desiredHeight) {
                                heights[(int)x + halfSize, (int)y + halfSize] = desiredHeight;
                            }
                        }
                    }
                }
            }
            if (way == Way.Up && !holdHeight) {
                // go raising the terrain slowly
                desiredHeight += Time.deltaTime * speed / 100;
            } else if (way == Way.Down && !holdHeight) {
                desiredHeight -= Time.deltaTime * speed / 100;
                if (desiredHeight < 0) {
                    desiredHeight = 0;
                }
            }
            // set the new height
            terr.terrainData.SetHeights(posXInTerrain - offset, posYInTerrain - offset, heights);
        } catch {
            Debug.Log("Clicked Outside Of Terrain");
        }
        size = size / 2;
    }
        private void OnDestroy() {
        try {
            this.terrain.terrainData.SetHeights(0, 0, this.originalHeights);
        } catch {
            Debug.Log("Didn't edit terrain");
        }
    }

    private void SquareTerrainGround() {

    }

    private void CircleTerrainGround() {

    }


    private void TurnOff() {
        mainCamera.GetComponent<CameraMovement>().enabled = true;
        transform.GetComponent<TerrainEditor>().enabled = false;
    }
}