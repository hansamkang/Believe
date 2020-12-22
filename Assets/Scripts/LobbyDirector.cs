using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyDirector : MonoBehaviour {
    public GameObject image;
    public GameObject cleardMessage;
    Animator fadeAnimator;
    Animator menuAnimator;

    // Use this for initialization
    void Start () {
        menuAnimator = image.GetComponent<Animator>();

        fadeAnimator = GameObject.Find("Fade").GetComponent<Animator>();

        FindObjectOfType<AudioManager>().BGM_Play("Lobby_01");

        fadeAnimator.Play("Fade_in");

        if (Director.GameOnState == true)
        {
            menuAnimator.Play("Menu_idle");

            GameData data = SaveLoadSystem.loadData();
            if(data != null)
            {
                Director.TutorialState = data.tutorialCleared;
                Director.DhomaState = data.DhomaCleared;
            }
           
        }
        else
        {
            menuAnimator.Play("Menu_closed");
        }
        
        if(Director.DhomaState)
        {
            cleardMessage.GetComponent<SpriteRenderer>().enabled = true;
        }
        Director.GameOnState = false;
    }
	
    public void CloseMenu()
    {
        FindObjectOfType<AudioManager>().Play("Switch_01");
        menuAnimator.Play("Menu_closing");
    }
}
