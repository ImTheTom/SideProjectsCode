using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRecieving : MonoBehaviour {

    public void OnCollisionEnter(Collision objectCollided) {
        Pole.AddToList(objectCollided.gameObject);
    }
}
