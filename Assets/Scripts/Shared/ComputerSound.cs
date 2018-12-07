using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ComputerSound : MonoBehaviour {

    public AudioClip startSound;
    public AudioClip endingSound;

    // Use this for initialization
    void Start () {

        if (SceneManager.GetActiveScene().name == "ChatRoom" || SceneManager.GetActiveScene().name == "AltChatRoom")
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(startSound, 1);
        }
        if (SceneManager.GetActiveScene().name == "ResultScreen")
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(endingSound, 1);
        }

    }
	
}
