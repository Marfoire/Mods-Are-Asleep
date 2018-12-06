using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    //variables for all game modes
    public string playerName;
    public Sprite playerProfilePicture;
    public int playerScore;
    public ChatManagerScript managerScriptReference;           
    private HFTInput myHFTInput;
    public Color playerColor;

    //variables for spam game mode
    public GameObject memeMessagePrefab;
    public bool amIBanned;
    private float banDuration;
    private float banTimeStart;
    public ModeratorScript[] chatModerators;

    //variables for vote game mode
    public GameObject voteMessagePrefab;
    public bool votedYes;
    

    /// <summary>
    /// ///////////////////////////// SHARED GAME MODE FUNCTIONS ////////////////////////////////
    /// </summary>

    public void PlayerConstructor(/*string givenName,*/ Sprite givenPicture, GameObject memefab, GameObject votefab, ModeratorScript[] mods, ChatManagerScript cm)
    {
        DontDestroyOnLoad(this);
        banDuration = 5;
        amIBanned = false;        
        playerProfilePicture = givenPicture;
        playerScore = 0;       
        memeMessagePrefab = memefab;
        voteMessagePrefab = votefab;
        managerScriptReference = cm;
        chatModerators = mods;
        myHFTInput = GetComponent<HFTInput>();
        playerColor = GetComponent<HFTGamepad>().color;

        //playerName = givenName;
        //gameObject.name = givenName;

        playerName = GetComponent<HFTGamepad>().playerName;
        gameObject.name = GetComponent<HFTGamepad>().playerName;
    }

    /// <summary>
    /// ////////////////////////////// SPAM GAME MODE FUNCTIONS /////////////////////////////////
    /// </summary>

    void CheckIfIShouldBeBanned()
    {
        for (int i = 0; i < chatModerators.Length; i++)
        {
            if (chatModerators[i].activityStatus == "Online")
            {
                amIBanned = true;
                banTimeStart = Time.time;
            }
        }
    }

    void PostMeme()
    {
        if (amIBanned != true)
        {
            if (myHFTInput.GetButtonDown("Fire1"))
            {
                playerScore++;
                managerScriptReference.ScrollMessages();
                GameObject memeMessage = Instantiate(memeMessagePrefab) as GameObject;
                MemeMessageScript messageScript = memeMessage.GetComponent<MemeMessageScript>();
                messageScript.MemeConstructor(playerName, playerProfilePicture, managerScriptReference.memePictures[Random.Range(0, managerScriptReference.memePictures.Length)], this, playerColor);
                CheckIfIShouldBeBanned();
            }
        }
    }

    void UnBanCountdown()
    {
        if (amIBanned == true)
        {
            if (Time.time - banTimeStart > banDuration)
            {
                amIBanned = false;
                banDuration++;
            }
        }
    }

    /// <summary>
    /// /////////////////////////////// VOTE GAME MOD FUNCTIONS /////////////////////////////////
    /// </summary>

    void PostComment()
    {
        if (managerScriptReference.timeIsPaused != true)
        {
            if (myHFTInput.GetButtonDown("Fire1"))
            {
                votedYes = true;
                managerScriptReference.ScrollMessages();
                GameObject voteMessage = Instantiate(voteMessagePrefab) as GameObject;
                VoteMessageScript messageScript = voteMessage.GetComponent<VoteMessageScript>();
                messageScript.VoteConstructor(playerName, playerProfilePicture, managerScriptReference.voteStrings[Random.Range(0, managerScriptReference.voteStrings.Length)], this, playerColor);
            }
        }
    }

    /// <summary>
    /// ///////////////////////////////////// UPDATE ////////////////////////////////////////////
    /// </summary>

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "ChatRoom")
        {
            PostMeme();
            UnBanCountdown();
        }

        if(SceneManager.GetActiveScene().name == "AltChatRoom")
        {
            PostComment();
        }

        if(SceneManager.GetActiveScene().name == "Lobby")
        {
            playerScore = 0;
            banDuration = 5;
        }

        /*if (GetComponent<HFTGamepad>().playerName != playerName && GetComponent<HFTGamepad>().playerName != null)
        {
            playerName = GetComponent<HFTGamepad>().playerName;
            gameObject.name = GetComponent<HFTGamepad>().playerName;
        }*/
    }


}
