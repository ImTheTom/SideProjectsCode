using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Pole : MonoBehaviour {
    public GameObject cue;
    public GameObject billard;
    float radians;
    bool setup = true;
    bool mouseDown = false;
    float minX;
    float minY;
    float maxX;
    float maxY;
    float maxTime = 0;
    float minTime = 0;
    float hit;
    float zOffset = 0;
    float xOffset = 0;
    float mulitplier;
    bool rolling = false;
    bool calculateNextTurn = false;
    bool gameOver = false;
    public string turn = "";
    string Winner = "";
    string[] billardNames = new string[15];
    public GameObject[] billards = new GameObject[15];
    GameObject[] player1Balls = new GameObject[7];
    GameObject[] player2Balls = new GameObject[7];
    List<GameObject> player1BallsSunk = new List<GameObject>();
    List<GameObject> player2BallsSunk = new List<GameObject>();
    List<GameObject> billardsSunk = new List<GameObject>();
    List<GameObject> billardsSunkThisRound = new List<GameObject>();
    static List<GameObject> billardsHit = new List<GameObject>();
    public Text TurnText;
    public Text WinnerText;
    public Text player1Text;
    public Text player2Text;

    // Use this for initialization
    void Start () {
        Vector3 pos = billard.transform.position;
        pos.z += 0.3f;
        cue.transform.position = pos;
        mulitplier = 2.5f;
        turn = "Player1";
        Physics.sleepThreshold = 0.75F;
        int index = 0;
        foreach(GameObject playingBillard in billards) {
            billardNames[index] = playingBillard.name;
            index += 1;
        }
        for(int j =0; j < 7; j++) {
            player1Balls[j] = billards[j];
        }
        index = 0;
        for (int k = 8; k < 15; k++) {
            player2Balls[index] = billards[k];
            index++;
        }
        billardsHit.Clear();
    }

    // Update is called once per frame
    void Update() {
        string player1Balls = "";
        foreach(GameObject player1BallSunk in player1BallsSunk) {
            player1Balls += player1BallSunk.name.Substring(5,2);
            player1Balls += ", ";
        }
        string player2Balls = "";
        foreach (GameObject player2BallSunk in player2BallsSunk) {
            player2Balls += player2BallSunk.name.Substring(5, 2);
            player2Balls += ", ";
        }
        TurnText.text = turn;
        foreach (GameObject playingBillard in billards) {
            if(playingBillard.transform.position.y < 1) {
                playingBillard.GetComponent<Rigidbody>().Sleep();
            }
        }
        player1Text.text = player1Balls;
        player2Text.text = player2Balls;
        if (!gameOver) {
        foreach (GameObject x in billardsHit) {
                int pos = Array.IndexOf(billardNames, x.name);
                if (pos == -1) {
                    billardsHit.Remove(x);
                }
            }
            if (setup) {
            if (calculateNextTurn) {
                    CheckTurn();
                }
                billardsSunkThisRound.Clear();
                billard.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                cue.GetComponent<MeshRenderer>().enabled = true;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                Vector3 pos = hit.point - billard.transform.position;
                radians = Mathf.Atan(pos.z / pos.x);
                float angle = radians * (180 / Mathf.PI);
                zOffset = Mathf.Sin(radians) * 0.3f;
                xOffset = Mathf.Cos(radians) * 0.3f;
                if (pos.x > 0 && pos.z > 0) {
                } else if (pos.x > 0 && pos.z < 0) {
                    angle += 360;
                } else if (pos.x < 0 && pos.z < 0) {
                    angle += 180;
                    zOffset = -zOffset;
                    xOffset = -xOffset;
                } else if (pos.x < 0 && pos.z > 0) {
                    angle += 180;
                    zOffset = -zOffset;
                    xOffset = -xOffset;
                }
                cue.transform.eulerAngles = new Vector3(0, -(angle + 270), 0);
                Vector3 billardPos = billard.transform.position;
                billardPos.z += zOffset;
                billardPos.x += xOffset;
                cue.transform.position = billardPos;
                if (billard.transform.position.y < 4.5) {
                    billard.transform.position = new Vector3(21.75f, 5, 10);
                }
            } else if (mouseDown) {
                maxTime += Time.deltaTime;
                minTime += Time.deltaTime;
                Vector3 pos = Input.mousePosition;
                if (minY > pos.y) {
                    minY = pos.y;
                    minTime = 0;
                }
                if (minX > pos.x) {
                    minX = pos.x;
                    minTime = 0;
                }
                if (maxY < pos.y) {
                    maxY = pos.y;
                    maxTime = 0;
                }
                if (maxX < pos.x) {
                    maxX = pos.x;
                    maxTime = 0;
                }
            } else if (rolling) {
                if (Mathf.Abs(billard.GetComponent<Rigidbody>().velocity.x) <= 0.05 && Mathf.Abs(billard.GetComponent<Rigidbody>().velocity.z) <= 0.05) {
                    int x = 0;
                    foreach (GameObject playingBillard in billards) {
                        if (Mathf.Abs(playingBillard.GetComponent<Rigidbody>().velocity.x) <= 0.05 && Mathf.Abs(playingBillard.GetComponent<Rigidbody>().velocity.z) <= 0.05) {
                            x += 1;
                        }
                        if (playingBillard.transform.position.y < 4.6 && playingBillard.transform.position.y > 1) {
                            if (!billardsSunkThisRound.Contains(playingBillard)) {
                                billardsSunkThisRound.Add(playingBillard);
                            }
                        }

                    }
                    if (x == 15) {
                        rolling = false;
                        setup = true;
                    }
                }
            }
            if (Input.GetMouseButtonDown(0)) {
                setup = false;
                calculateNextTurn = true;
                if (!rolling) {
                    mouseDown = true;
                    billardsHit.Clear();
                }
                minX = Input.mousePosition.x;
                minY = Input.mousePosition.y;
                maxX = Input.mousePosition.x;
                maxY = Input.mousePosition.y;
            } else if (Input.GetMouseButtonUp(0)) {
                cue.GetComponent<MeshRenderer>().enabled = false;
                float actualTime = maxTime > minTime ? maxTime : minTime;
                mulitplier = 2.5f - actualTime;
                mulitplier = mulitplier < 1 ? 1 : mulitplier;
                maxTime = 0;
                minTime = 0;
                mouseDown = false;
                hit = Mathf.Sqrt(((maxX - minX) * (maxX - minX)) + ((maxY - minY) * (maxY - minY)));
                rolling = true;
                billard.GetComponent<Rigidbody>().AddForce(-xOffset * hit * mulitplier, 0, -zOffset * hit * mulitplier);
            }
        }
    }

    public static void AddToList(GameObject a) {
        billardsHit.Add(a);
    }
    
    public void CheckTurn() {
        foreach (GameObject plaingBillard in billards) {
            if (plaingBillard.transform.position.y < 4.6 && plaingBillard.transform.position.y > 1) {
                if (!billardsSunk.Contains(plaingBillard)) {
                    billardsSunk.Add(plaingBillard);
                    plaingBillard.transform.position = new Vector3(35, 1, 12);
                }
            }
        }
        bool skip = false;
        bool hitYourBallFirst = false;
        foreach(GameObject x in billardsSunkThisRound) {
            int pos = Array.IndexOf(player1Balls, x);
            if (pos != -1) {
                player1BallsSunk.Add(x);
            }
            pos = Array.IndexOf(player2Balls, x);
            if (pos != -1) {
                player2BallsSunk.Add(x);
            }
        }
        if(turn == "Player1") {
            if (billardsSunkThisRound.Contains(billards[7]) && player1BallsSunk.Count !=7) {
                gameOver = true;
                Winner = "Player2";
                WinnerText.text = Winner;
            } else if(billardsSunkThisRound.Contains(billards[7]) && player1BallsSunk.Count == 7) {
                gameOver = true;
                Winner = "Player1";
                WinnerText.text = Winner;
            }
            if(billardsHit.Count > 0) {
                int pos = Array.IndexOf(player1Balls, billardsHit[0]);
                if (pos != -1) {
                    hitYourBallFirst = true;
                }
            }
            if (billardsSunkThisRound.Count > 0) {
                int pos = Array.IndexOf(player1Balls, billardsSunkThisRound[0]);
                if (pos != -1 && hitYourBallFirst) {
                    skip = true;
                }
            }
            if (billard.transform.position.y < 4.5) {
                billard.transform.position = new Vector3(21.75f, 5, 10);
                skip = false;
            }
            if (!skip) {
                turn = "Player2";
            }
        } else {
            if (billardsSunkThisRound.Contains(billards[7]) && player2BallsSunk.Count != 7) {
                gameOver = true;
                Winner = "Player1";
                WinnerText.text = Winner;
            } else if (billardsSunkThisRound.Contains(billards[7]) && player2BallsSunk.Count == 7) {
                gameOver = true;
                Winner = "Player2";
                WinnerText.text = Winner;
            }
            if (billardsHit.Count > 0) {
                int pos = Array.IndexOf(player2Balls, billardsHit[0]);
                if (pos != -1) {
                    hitYourBallFirst = true;
                }
            }
            if (billardsSunkThisRound.Count > 0) {
                int pos = Array.IndexOf(player2Balls, billardsSunkThisRound[0]);
                if (pos != -1 && hitYourBallFirst) {
                    skip = true;
                }
            }
            if (billard.transform.position.y < 4.5) {
                billard.transform.position = new Vector3(21.75f, 5, 10);
                skip = false;
            }
            if (!skip) {
                turn = "Player1";
            }
        }
        calculateNextTurn = false;
    }
}
