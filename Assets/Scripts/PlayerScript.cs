using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public string playerName;
    public Sprite playerProfilePicture;
    public int playerScore;
    public ChatManagerScript managerScriptReference;
    public GameObject memeMessagePrefab;
    public ModeratorScript[] chatModerators;
    public bool amIBanned;
    private float banDuration;
    private float banTimeStart;
    private HFTInput myHFTInput;
    public Color playerColor;

    public void PlayerConstructor(string givenName, Sprite givenPicture, GameObject memefab, ModeratorScript[] mods, ChatManagerScript cm)
    {
        banDuration = 2;
        amIBanned = false;
        playerName = givenName;
        playerProfilePicture = givenPicture;
        playerScore = 0;
        gameObject.name = givenName;
        memeMessagePrefab = memefab;
        managerScriptReference = cm;
        chatModerators = mods;
        myHFTInput = GetComponent<HFTInput>();
        playerColor = GetComponent<HFTGamepad>().color;
    }

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



    private void Update()
    {
        PostMeme();
        UnBanCountdown();
    }


}
