using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteMessageScript : MonoBehaviour {


    public void VoteConstructor(string profileName, Sprite profilePicture, string message, PlayerScript ps, Color playerColor)
    {
        GetComponentInChildren<TextMesh>().text = "<color=#" + ColorUtility.ToHtmlStringRGBA(playerColor) + ">" + profileName + "</color>";
        GetComponent<SpriteRenderer>().sprite = profilePicture;
        transform.GetChild(1).GetComponent<TextMesh>().text = message;
        transform.GetChild(1).transform.GetChild(0).GetComponent<TextMesh>().text = message;
    }


    private void DeleteMessage()
    {
        if (transform.position.y > 22)
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        DeleteMessage();
    }
}
