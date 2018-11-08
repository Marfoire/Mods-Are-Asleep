using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardScript : MonoBehaviour {

    public GameObject[] leaderboardSlots;
    public GameObject managerReference;

    void Awake()
    {
        leaderboardSlots = new GameObject[transform.childCount-1];
        for (int i = 0; i < leaderboardSlots.Length; i++)
        {
            leaderboardSlots[i] = transform.GetChild(i + 1).gameObject;
        }
    }

    void UpdateLeaderboard()
    {
        for (int i = 0; i < leaderboardSlots.Length; i++)
        {
            if (managerReference.GetComponent<ChatManagerScript>().playerScores.Count-1 >= i) {
                leaderboardSlots[i].GetComponent<TextMesh>().text = (i + 1).ToString() + ". " + "<color=#" + ColorUtility.ToHtmlStringRGBA(managerReference.GetComponent<ChatManagerScript>().playerScores[i].playerColor) + ">" +  managerReference.GetComponent<ChatManagerScript>().playerScores[i].name + "</color>";
                leaderboardSlots[i].transform.GetChild(0).GetComponent<TextMesh>().text = "<color=#" + ColorUtility.ToHtmlStringRGBA(managerReference.GetComponent<ChatManagerScript>().playerScores[i].playerColor) + ">" + managerReference.GetComponent<ChatManagerScript>().playerScores[i].playerScore.ToString() + "</color>";
            }
            else if (managerReference.GetComponent<ChatManagerScript>().playerScores.Count-1 < i)
            {
                leaderboardSlots[i].GetComponent<TextMesh>().text = " ";
                leaderboardSlots[i].transform.GetChild(0).GetComponent<TextMesh>().text = " ";
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateLeaderboard();
	}
}
