using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class ChatManagerScript : MonoBehaviour {

    //shared variables
    public List<PlayerScript> playerArray;
    public string[] playerNames;
    public Sprite[] profilePictures;
    public GameObject playerPrefab;
    public List<PlayerScript> playerScores;
    private float timer;
    public float timerTimeLimitInSeconds;
    private float timeLeft;
    public GameObject timerTextBox;
    public bool timeIsPaused;
    public int flashCount;

    //spam variables
    public ModeratorScript[] modArray;
    public Sprite[] memePictures;    
    public GameObject memePrefabForPlayers;
    public GameObject modPrefab;
    public GameObject banListObject;
    public List<String> bannedPlayers;

    //vote variables
    public GameObject voteMessagePrefab;
    public string[] voteStrings;
    public int voteRoundMax;
    public int voteRoundCount;
    public VoteModeratorScript votingModerator;
    public GameObject voteModPrefab;
    public int memeDankness;
    private List<int> usedMemeIndexes = new List<int>();
    private bool resultsPhase;

    

    /// <summary>
    /// ///////////////////////////// SHARED GAME MODE FUNCTIONS ////////////////////////////////
    /// </summary>

    void Awake () {

        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "ChatRoom")
        {
            timer = 0;
            timerTimeLimitInSeconds = 60;
            for(int i = 0; i < GameObject.FindGameObjectsWithTag("Dynamic Text").Length; i++)
            {
                if (GameObject.FindGameObjectsWithTag("Dynamic Text")[i].name == "Timer")
                {
                    timerTextBox = GameObject.FindGameObjectsWithTag("Dynamic Text")[i];
                }

                if (GameObject.FindGameObjectsWithTag("Dynamic Text")[i].name == "BannedPlayersText")
                {
                    banListObject = GameObject.FindGameObjectsWithTag("Dynamic Text")[i];
                }
            }

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

        if (scene.name == "AltChatRoom")
        {
            voteRoundCount = 0;
            timer = 0;
            timerTimeLimitInSeconds = 5;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Dynamic Text").Length; i++)
            {
                if (GameObject.FindGameObjectsWithTag("Dynamic Text")[i].name == "VoteTimer")
                {
                    timerTextBox = GameObject.FindGameObjectsWithTag("Dynamic Text")[i];
                }
            }

            GameObject voteMod = Instantiate(voteModPrefab, new Vector3(6, 13.2f), Quaternion.identity) as GameObject;
            VoteModeratorScript script = voteMod.GetComponent<VoteModeratorScript>();
            votingModerator = script;

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

            script.VoteModConstructor(playerNames[randomNameValue], profilePictures[randomPictureValue]);

            RandomizeSpotlightMeme();
        }
    }

    public void AddPlayer(GameObject player)
    {
            PlayerScript script = player.GetComponent<PlayerScript>();
            playerArray.Add(script);

            //int randomNameValue = UnityEngine.Random.Range(0, playerNames.Length);
            int randomPictureValue = UnityEngine.Random.Range(0, profilePictures.Length);

            /*while (playerNames[randomNameValue] == null)
            {
                randomNameValue = UnityEngine.Random.Range(0, playerNames.Length);
            }*/

            while (profilePictures[randomPictureValue] == null)
            {
                randomPictureValue = UnityEngine.Random.Range(0, profilePictures.Length);
            }

            script.PlayerConstructor(/*playerNames[randomNameValue],*/ profilePictures[randomPictureValue], memePrefabForPlayers, voteMessagePrefab, modArray, this);

            //Array.Clear(playerNames, randomNameValue, 1);
            Array.Clear(profilePictures, randomPictureValue, 1);

            playerScores.Add(script);
    }

    public void ScrollMessages()
    {
        if (SceneManager.GetActiveScene().name == "ChatRoom")
        {
            if (GameObject.FindGameObjectsWithTag("Message") != null)
            {
                foreach (GameObject message in GameObject.FindGameObjectsWithTag("Message"))
                {
                    message.transform.Translate(0, 6, 0);
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "AltChatRoom")
        {
            if (GameObject.FindGameObjectsWithTag("Message") != null)
            {
                foreach (GameObject message in GameObject.FindGameObjectsWithTag("Message"))
                {
                    message.transform.Translate(0, 4, 0);
                }
            }
        }
    }

    public void UpdateTop10()
    {
        playerScores = playerScores.OrderByDescending(x => x.playerScore).ToList();
    }


    /// <summary>
    /// ////////////////////////////// SPAM GAME MODE FUNCTIONS /////////////////////////////////
    /// </summary>

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        timeLeft = timerTimeLimitInSeconds - timer;
        string minutes = Mathf.Floor((timeLeft) / 60).ToString("00");
        string seconds = Mathf.Floor(((timeLeft) % 60)).ToString("00");

        timerTextBox.GetComponent<TextMesh>().text = minutes + ":" + seconds;

        if (timeLeft <= 0)
        {
            for(int i = 0; modArray.Length > i; i++){
                if(playerNames.Contains(null) == true)
                {
                    playerNames[Array.FindLastIndex(playerNames, null)] = modArray[i].transform.GetChild(0).GetComponent<TextMesh>().text;
                }

                if (profilePictures.Contains(null) == true)
                {
                    profilePictures[Array.FindLastIndex(profilePictures, null)] = modArray[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
                }

            }
            SceneManager.LoadScene("ResultScreen", LoadSceneMode.Single);
        }

        if (timeLeft <= 10)
        {
            flashCount = 10;
            for (int i = 0; i < flashCount; i++)
            {
                timerTextBox.GetComponent<TextMesh>().color = Color.Lerp(Color.green, Color.red, Mathf.PingPong(Time.time, 1));
                timerTextBox.GetComponent<TextMesh>().color = Color.Lerp(Color.red, Color.green, Mathf.PingPong(Time.time, 1));
            }

        }

    }

    public void UpdateBanList()
    {
        for (int i = 0; i < playerArray.Count; i++)
        {
            if (playerArray[i].amIBanned == true && !bannedPlayers.Contains(("<color=#" + ColorUtility.ToHtmlStringRGBA(playerArray[i].playerColor) + ">" + playerArray[i].name + "</color>")))
            {
                bannedPlayers.Add("<color=#" + ColorUtility.ToHtmlStringRGBA(playerArray[i].playerColor) + ">" + playerArray[i].name + "</color>");
            }
            else if (playerArray[i].amIBanned == false && bannedPlayers.Contains(("<color=#" + ColorUtility.ToHtmlStringRGBA(playerArray[i].playerColor) + ">" + playerArray[i].name + "</color>")))
            {
                bannedPlayers.Remove("<color=#" + ColorUtility.ToHtmlStringRGBA(playerArray[i].playerColor) + ">" + playerArray[i].name + "</color>");
            }
        }
        banListObject.GetComponent<Text>().text = String.Join(", ", bannedPlayers.ToArray());
    }

    /// <summary>
    /// /////////////////////////////// VOTE GAME MOD FUNCTIONS /////////////////////////////////
    /// </summary> 

    public void RandomizeSpotlightMeme()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("SpotlightMeme").Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag("SpotlightMeme")[i].name == "SpotlightMeme")
            {
                int randomMemeValue = UnityEngine.Random.Range(0, memePictures.Length);

                if (usedMemeIndexes.Count > 0)
                {
                    while (usedMemeIndexes.Contains(randomMemeValue) == true)
                    {
                        randomMemeValue = UnityEngine.Random.Range(0, memePictures.Length);
                    }
                }

                GameObject.FindGameObjectsWithTag("SpotlightMeme")[i].GetComponent<SpriteRenderer>().sprite = memePictures[randomMemeValue];
                usedMemeIndexes.Add(randomMemeValue);
            }
        }
    }

    private void UpdateVoteTimer()
    {
        if (timeIsPaused != true)
        {
            timer += Time.deltaTime;
            timeLeft = timerTimeLimitInSeconds - timer;
            string seconds = Mathf.Clamp(Mathf.Floor(((timeLeft) % 60)), 0, 10).ToString("00");
            timerTextBox.GetComponent<TextMesh>().text = seconds;

            if (timeLeft <= 0)
            {
                timer = 0;
                timeIsPaused = true;
            }

            if (voteRoundCount >= voteRoundMax)
            {
                usedMemeIndexes.Clear();
                SceneManager.LoadScene("ResultScreen", LoadSceneMode.Single);
            }
        }
    }

    public void VoteResults()
    {
        if(timeIsPaused == true && resultsPhase == false)
        {
            memeDankness = UnityEngine.Random.Range(0,2);
            if(memeDankness == 1)
            {
                resultsPhase = true;
                StartCoroutine(DankMeme());
                
            }

            if (memeDankness == 0)
            {
                resultsPhase = true;
                StartCoroutine(DanklessMeme());
            }



        }
    }

    IEnumerator DankMeme()
    {
        yield return new WaitForSeconds(1);

        GameObject stamp = null;

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("SpotlightMeme").Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag("SpotlightMeme")[i].name == "DankStamp")
            {
                stamp = GameObject.FindGameObjectsWithTag("SpotlightMeme")[i];
                stamp.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
        }

        for (int i = 0; i < playerArray.Count; i++)
        {
            if (playerArray[i].votedYes == true)
            {
                playerArray[i].playerScore++;
            }
            playerArray[i].votedYes = false;
        }

        yield return new WaitForSeconds(3);

        stamp.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        RandomizeSpotlightMeme();
        voteRoundCount++;
        timeIsPaused = false;
        resultsPhase = false;
        yield return null;
    }

    IEnumerator DanklessMeme()
    {
        yield return new WaitForSeconds(1);

        GameObject stamp = null;

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("SpotlightMeme").Length; i++)
        {
            if (GameObject.FindGameObjectsWithTag("SpotlightMeme")[i].name == "DanklessStamp")
            {
                stamp = GameObject.FindGameObjectsWithTag("SpotlightMeme")[i];
                stamp.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            }
        }

        for (int i = 0; i < playerArray.Count; i++)
        {
            if (playerArray[i].votedYes == false)
            {
                playerArray[i].playerScore++;
            }
            playerArray[i].votedYes = false;
        }

        yield return new WaitForSeconds(3);

        stamp.GetComponent<SpriteRenderer>().color = new Color(255,255,255, 0);
        RandomizeSpotlightMeme();
        voteRoundCount++;
        timeIsPaused = false;
        resultsPhase = false;
        yield return null;
    }



    /// <summary>
    /// ///////////////////////////////////// UPDATE ////////////////////////////////////////////
    /// </summary>

    void Update () {
        if (SceneManager.GetActiveScene().name == "ChatRoom")
        {
            UpdateBanList();
            UpdateTop10();
            UpdateTimer();
        }

        if(SceneManager.GetActiveScene().name == "AltChatRoom")
        {
            UpdateVoteTimer();
            VoteResults();
            UpdateTop10();
        }

	}
}
