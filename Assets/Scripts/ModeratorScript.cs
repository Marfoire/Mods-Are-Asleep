using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeratorScript : MonoBehaviour {

    private float offlineTimeStart;
    private float loggingTimeStart;
    private float onlineTimeStart;

    private float offlineTimeInterval;
    private float loggingTimeInterval;
    private float onlineTimeInterval;

    public string activityStatus;

    private SpriteRenderer onlineStatusBlip;
    private SpriteRenderer modProfilePicture;
    private TextMesh onlineStatusText;
    private TextMesh modProfileName;



	// Use this for initialization
	void Awake() {

        onlineStatusBlip = transform.GetChild(3).GetComponent<SpriteRenderer>();
        modProfilePicture = transform.GetChild(1).GetComponent<SpriteRenderer>();

        onlineStatusText = transform.GetChild(2).GetComponent<TextMesh>();
        modProfileName = transform.GetChild(0).GetComponent<TextMesh>();

        activityStatus = "Offline";
        offlineTimeInterval = Random.Range(5, 21);
    }

    void Start()
    {
        offlineTimeStart = Time.time;
        onlineStatusBlip.color = Color.red;
        onlineStatusText.color = Color.red;
    }

    public void ModConstructor(string name, Sprite picture)
    {
        modProfileName.text = name;
        modProfilePicture.sprite = picture;
    }



    void GoLogging()
    {
        if (activityStatus == "Offline")
        {           
            if (Time.time - offlineTimeStart > offlineTimeInterval)//if the delay is up
            {
                activityStatus = "Logging In...";
                loggingTimeStart = Time.time;
                loggingTimeInterval = Random.Range(2, 11);
                onlineStatusBlip.color = Color.yellow;
                onlineStatusText.color = Color.yellow;
            }
        }
    }

    void GoOnline()
    {
        if (activityStatus == "Logging In...")
        {
            if (Time.time - loggingTimeStart > loggingTimeInterval)//if the delay is up
            {
                if (Random.Range(0, 10) >= 3)
                {
                    activityStatus = "Online";
                    onlineTimeStart = Time.time;
                    onlineTimeInterval = Random.Range(2, 6);
                    onlineStatusBlip.color = Color.green;
                    onlineStatusText.color = Color.green;
                }
                else
                {
                    activityStatus = "Offline";
                    offlineTimeStart = Time.time;
                    offlineTimeInterval = Random.Range(5, 21);
                    onlineStatusBlip.color = Color.red;
                    onlineStatusText.color = Color.red;
                }
            }
        }
    }

    void GoOffline()
    {
        if(activityStatus == "Online")
        {
            if (Time.time - onlineTimeStart > onlineTimeInterval)//if the delay is up
            {
                activityStatus = "Offline";
                offlineTimeStart = Time.time;
                offlineTimeInterval = Random.Range(5, 21);
                onlineStatusBlip.color = Color.red;
                onlineStatusText.color = Color.red;
            }
        }

    }

    void CycleOnlineActivities()
    {
        GoLogging();
        GoOnline();
        GoOffline();
        onlineStatusText.text = activityStatus;
    }

	
	// Update is called once per frame
	void Update () {
        CycleOnlineActivities();
	}
}
