using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LobbyPlayerList : MonoBehaviour {

    public GameObject managerReference;
    public List<String> lobbyListNames;

	// Use this for initialization
	void Awake () {
        managerReference = GameObject.FindGameObjectWithTag("Manager").gameObject;
	}
	
    void UpdatePlayerList()
    {
        for (int i = 0; i + 1 <= managerReference.GetComponent<ChatManagerScript>().playerArray.Count; i++)
        {            
            if (!lobbyListNames.Contains((i + 1) + ". " + "<color=#" + ColorUtility.ToHtmlStringRGBA(managerReference.GetComponent<ChatManagerScript>().playerArray[i].playerColor) + ">" + managerReference.GetComponent<ChatManagerScript>().playerArray[i].name + "</color>"))
            {
                lobbyListNames.Add((i+1) + ". "  + "<color=#" + ColorUtility.ToHtmlStringRGBA(managerReference.GetComponent<ChatManagerScript>().playerArray[i].playerColor) + ">" + managerReference.GetComponent<ChatManagerScript>().playerArray[i].name + "</color>");
            }           
        }
        if (lobbyListNames.Count > 33)
        {
            transform.GetChild(1).gameObject.GetComponent<Text>().text = String.Join(Environment.NewLine, lobbyListNames.ToArray(), 0, 33);
        }
        else
        {
            transform.GetChild(1).gameObject.GetComponent<Text>().text = String.Join(Environment.NewLine, lobbyListNames.ToArray(), 0, lobbyListNames.Count);
        }

        if (lobbyListNames.Count > 67) {
            transform.GetChild(2).gameObject.GetComponent<Text>().text = String.Join(Environment.NewLine, lobbyListNames.ToArray(), 33, 67);
        }
        else if(lobbyListNames.Count > 33)
        {
            transform.GetChild(2).gameObject.GetComponent<Text>().text = String.Join(Environment.NewLine, lobbyListNames.ToArray(), 33, lobbyListNames.Count - 33);
        }

        if (lobbyListNames.Count == 100)
        {
            transform.GetChild(3).gameObject.GetComponent<Text>().text = String.Join(Environment.NewLine, lobbyListNames.ToArray(), 67, 100);
        }
        else if (lobbyListNames.Count > 67)
        {
            transform.GetChild(3).gameObject.GetComponent<Text>().text = String.Join(Environment.NewLine, lobbyListNames.ToArray(), 67, lobbyListNames.Count - 67);
        }
    }


	// Update is called once per frame
	void Update () {
		if(SceneManager.GetActiveScene().name == "Lobby")
        {
            UpdatePlayerList();
        }
	}
}
