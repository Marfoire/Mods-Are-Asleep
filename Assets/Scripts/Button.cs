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

    // Use this for initialization
    void Awake () {
        
    }



    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            LoadState();
        }
    }


    //load state just checks the button type and loads it's corresponding scene using conditionals
    public void LoadState()
    {
        if (buttonType == "MemeChat")//if this button's type is game
        {
            print("b");
            SceneManager.LoadScene("ChatRoom", LoadSceneMode.Single);//it loads the game scene
        }
        else if (buttonType == "TextChat")//if this button's type is menu
        {
            SceneManager.LoadScene("AltChatRoom", LoadSceneMode.Single);//it goes to the menu screen
        }
    }
    
}
