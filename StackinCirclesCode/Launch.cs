using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class handles the launching of the circle and creating a new circle after launching one
public class Launch : MonoBehaviour {

    public GameObject currentCircle;
    public GameObject defaultCirclePrefab;
    public Rigidbody currentCircleRigidbody;
    public ItemHandler currentCircleItemHandler;
    public List<GameObject> ListOfCircles = new List<GameObject>();

    public Vector3 initalLaunchPositionVector = new Vector3(0, 0, 0);
    public Vector3 finalLaunchPositionVector = new Vector3(0, 0, 0);

    private bool isGameOver = false;

    private const int TIMEDELAY = 1;

    public delegate void LaunchDelegate(List<GameObject> gameObjectList);

    LaunchDelegate RunCheck;

    // This function loops through the list of launched objects and checks their Item Handler variable isGameRunning is set to
    // False, if it is it updates the launch variable isGameOver to true
    public void CheckIfGameOver(List<GameObject> gameObjectList) {
        foreach (GameObject individualGameObject in gameObjectList) {
            if (!individualGameObject.GetComponent<ItemHandler>().GetGameRunning()) {
                isGameOver = true;
            }
        }
    }
    
    void Start() {
        RunCheck = CheckIfGameOver;
    }

    void Update() {
        if (!isGameOver) {
            RunCheck(ListOfCircles);
        }
    }

    // This function checks if isGameOver is set to false and if it is, it sets the intialLaunchPositionVector to
    // Where the mouse position was.
    public void OnMouseDown() {
        if (!isGameOver) {
            initalLaunchPositionVector = Input.mousePosition;
        }
    }

    // This function checks if isGameOver is set to false and if it is, it sets the finalLaunchPositionVector to where
    // the mouse position was. Two functions are called. Then the circle is launched using the two position vectors.
    // Then a couroutine is started to delay the next circle being created. If the isGameOver is set to true the game is restarted.
    public void OnMouseUp() {
        if (!isGameOver) {
            if (currentCircle == null) {
                currentCircle = GameObject.Find("CurrentObject");
            }
            finalLaunchPositionVector = Input.mousePosition;
            AddComponentsToCurrentCircle();
            AddCircleToList();
            LaunchCurrentCircle(finalLaunchPositionVector - initalLaunchPositionVector);
            currentCircle.name = "PreviousItem";
            Score.UpdateCurrentScore();
            StartCoroutine(WaitToSpawnNewItem());
        } else {
            SceneManager.LoadScene("Main");
        }
    }

    // This function adds vital components to the circle and some variables to the components are added.
    public void AddComponentsToCurrentCircle() {
        currentCircleItemHandler = currentCircle.GetComponent<ItemHandler>();
        currentCircleRigidbody = currentCircle.AddComponent<Rigidbody>();
        currentCircleRigidbody.angularDrag = 0.99f;
        currentCircle.AddComponent<SphereCollider>();
        currentCircleItemHandler.InstaniateItemHandlerVariables(currentCircle);
    }

    public void AddCircleToList() {
        ListOfCircles.Add(currentCircle);
    }

    // This function uses the calculateLaunchVector and adds a force that is returned to launch the circle
    public void LaunchCurrentCircle(Vector3 distanceMouseTraveled) {
        Vector3 launchVector = CalculateLaunchVector(distanceMouseTraveled);
        currentCircleRigidbody.AddForce(new Vector3(launchVector[0] * 0.4f, launchVector[1]));
    }

    public Vector3 CalculateLaunchVector(Vector3 distanceMouseTraveled) {
        return (distanceMouseTraveled * 0.75f);
    }

    // This function waits for the TIMEDELAY period and then calls a function
    IEnumerator WaitToSpawnNewItem() {
        yield return new WaitForSeconds(TIMEDELAY);
        SpawnNewItem();
    }

    // This function is used to spawn a new item using the defaultCirclePrefab after the WaitToSpawnNewItem calls it
    public void SpawnNewItem() {
        currentCircle = Instantiate(defaultCirclePrefab, new Vector3(2, 1.5f, 0), Quaternion.identity);
        currentCircle.name = "CurrentObject";
    }

}
