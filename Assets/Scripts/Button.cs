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

    private void OnMouseUp()
    {
        LoadState(); // calls load state when clicked

    }

    //load state just checks the button type and loads it's corresponding scene using conditionals
    public void LoadState()
    {
        if (buttonType == "Chat")//if this button's type is game
        {
            SceneManager.LoadScene("ChatRoom", LoadSceneMode.Single);//it loads the game scene
        }
        else if (buttonType == "Menu")//if this button's type is menu
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);//it goes to the menu screen
        }


    }
    
}
