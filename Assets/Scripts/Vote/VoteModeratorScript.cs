using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteModeratorScript : MonoBehaviour {

    private SpriteRenderer modProfilePicture;
    private TextMesh onlineStatusText;
    private TextMesh modProfileName;


    // Use this for initialization
    void Awake () {
        modProfilePicture = transform.GetChild(1).GetComponent<SpriteRenderer>();
        modProfileName = transform.GetChild(0).GetComponent<TextMesh>();
    }

    public void VoteModConstructor(string name, Sprite picture)
    {
        modProfileName.text = name;
        modProfilePicture.sprite = picture;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
