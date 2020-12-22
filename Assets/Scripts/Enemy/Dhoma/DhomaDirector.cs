using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DhomaDirector : MonoBehaviour {
    Animator fadeAnimator;

    private void Start()
    {
        fadeAnimator = GameObject.Find("Fade").GetComponent<Animator>();
        fadeAnimator.Play("Fade_in");
        FindObjectOfType<AudioManager>().BGM_Play("Darkest day");
    }
    public void backLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public static void dhomaDie()
    {
        Director.DhomaState = true;
        SaveLoadSystem.saveData();
    }
}
