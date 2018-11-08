using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ChatManagerScript : MonoBehaviour {


    public List<PlayerScript> playerArray;
    public ModeratorScript[] modArray;
    public string[] playerNames;
    public Sprite[] profilePictures;
    public Sprite[] memePictures;
    

    public GameObject playerPrefab;
    public GameObject memePrefabForPlayers;
    public GameObject modPrefab;
    public GameObject banListObject;

    public List<String> bannedPlayers;
    public List<PlayerScript> playerScores;

    private float timer;
    public float timerTimeLimitInSeconds;
    private float timeLeft;
    public GameObject timerTextBox;

    // Use this for initialization
    void Awake () {

        /*}

        void OnSceneLoaded()
        {*/

        for (int i = 0; i < modArray.Length; i++)
        {
            GameObject mod = Instantiate(modPrefab, new Vector3(6, (i * -2.7f) + 13.2f), Quaternion.identity) as GameObject;
            ModeratorScript script = mod.GetComponent<ModeratorScript>();
            modArray[i] = script;

            int randomNameValue = UnityEngine.Random.Range(0, playerNames.Length);
            int randomPictureValue = UnityEngine.Random.Range(0, profilePictures.Length);

            while (playerNames[randomNameValue] == null)
            {
                randomNameValue = UnityEngine.Random.Range(0, playerNames.Length);
            }

            while (profilePictures[randomPictureValue] == null)
            {
                randomPictureValue = UnityEngine.Random.Range(0, profilePictures.Length);
            }

            script.ModConstructor(playerNames[randomNameValue], profilePictures[randomPictureValue]);

            Array.Clear(playerNames, randomNameValue, 1);
            Array.Clear(profilePictures, randomPictureValue, 1);

        }

    }

    public void AddPlayer(GameObject player)
    {
            PlayerScript script = player.GetComponent<PlayerScript>();
            playerArray.Add(script);

            int randomNameValue = UnityEngine.Random.Range(0, playerNames.Length);
            int randomPictureValue = UnityEngine.Random.Range(0, profilePictures.Length);

            while (playerNames[randomNameValue] == null)
            {
                randomNameValue = UnityEngine.Random.Range(0, playerNames.Length);
            }

            while (profilePictures[randomPictureValue] == null)
            {
                randomPictureValue = UnityEngine.Random.Range(0, profilePictures.Length);
            }

            script.PlayerConstructor(playerNames[randomNameValue], profilePictures[randomPictureValue], memePrefabForPlayers, modArray, this);

            Array.Clear(playerNames, randomNameValue, 1);
            Array.Clear(profilePictures, randomPictureValue, 1);

            playerScores.Add(script);
    }


    public void ScrollMessages()
    {
        if (GameObject.FindGameObjectsWithTag("Message") != null)
        {
            foreach (GameObject message in GameObject.FindGameObjectsWithTag("Message"))
            {
                message.transform.Translate(0, 6, 0);
            }
        }
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        timeLeft = timerTimeLimitInSeconds - timer;
        string minutes = Mathf.Floor((timeLeft) / 60).ToString("00");
        string seconds = Mathf.Floor(((timeLeft) % 60)).ToString("00");
        timerTextBox.GetComponent<TextMesh>().text = minutes + ":" + seconds;
    }

    public void UpdateBanList()
    {
        for(int i = 0; i < playerArray.Count; i++)
        {
            if(playerArray[i].amIBanned == true && !bannedPlayers.Contains(("<color=#" + ColorUtility.ToHtmlStringRGBA(playerArray[i].playerColor) + ">"  + playerArray[i].name + "</color>")))
            {
                bannedPlayers.Add("<color=#" + ColorUtility.ToHtmlStringRGBA(playerArray[i].playerColor) + ">"  + playerArray[i].name + "</color>");
            }
            else if (playerArray[i].amIBanned == false && bannedPlayers.Contains(("<color=#" + ColorUtility.ToHtmlStringRGBA(playerArray[i].playerColor) + ">"  + playerArray[i].name + "</color>")))
            {
                bannedPlayers.Remove("<color=#" + ColorUtility.ToHtmlStringRGBA(playerArray[i].playerColor)  + ">"  + playerArray[i].name + "</color>");
            }
        }
        banListObject.GetComponent<Text>().text =  String.Join(", " , bannedPlayers.ToArray());
    }

    public void UpdateTop10()
    {
        playerScores = playerScores.OrderByDescending(x => x.playerScore).ToList();
    }



	// Update is called once per frame
	void Update () {
        UpdateBanList();
        UpdateTop10();
        UpdateTimer();
	}
}
