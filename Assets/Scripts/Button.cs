/*
 * The button script is attached to buttons
 * It is responsible for changing scenes when it is clicked
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {


    public string buttonType = "button"; // the string provided in inspector view determines where this button goes


    //load state just checks the button type and loads it's corresponding scene using conditionals
    public void LoadState()
    {
        if (buttonType == "MemeChat")//if this button's type is game
        {
            SceneManager.LoadScene("ChatRoom", LoadSceneMode.Single);//it loads the game scene
        }
        else if (buttonType == "TextChat")//if this button's type is menu
        {
            SceneManager.LoadScene("AltChatRoom", LoadSceneMode.Single);//it goes to the menu screen
        }
        else if (buttonType == "Lobby")//if this button's type is menu
        {
            SceneManager.LoadScene("Lobby", LoadSceneMode.Single);//it goes to the menu screen
        }
    }
    
}
