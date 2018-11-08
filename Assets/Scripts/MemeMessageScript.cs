using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemeMessageScript : MonoBehaviour {


    // Use this for initialization
    void Start()
    {

    }

    public void MemeConstructor(string profileName, Sprite profilePicture, Sprite memePicture, PlayerScript ps, Color playerColor)
    {
        GetComponentInChildren<TextMesh>().text = "<color=#" + ColorUtility.ToHtmlStringRGBA(playerColor) + ">" + profileName + "</color>";
        GetComponent<SpriteRenderer>().sprite = profilePicture;
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = memePicture;
        
    }


    private void DeleteMessage()
    {
        if (transform.position.y > 22)
        {
            Destroy(this.gameObject);
        }
    }

	
	// Update is called once per frame
	void Update () {
        DeleteMessage();
	}
}
