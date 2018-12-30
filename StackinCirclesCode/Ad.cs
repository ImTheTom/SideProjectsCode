using UnityEngine;
using UnityEngine.Advertisements;

public class Ad : MonoBehaviour {

    static int showAddCounter = 0;
    private const int SHOWADDTHRESHOLD = 9;

	void Start () {
        showAddCounter += 1;
        if (showAddCounter > SHOWADDTHRESHOLD) {
            Advertisement.Show();
            showAddCounter = 0;
        }
    }

}
