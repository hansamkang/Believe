using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TutorialDirector : MonoBehaviour {
    Animator fadeAnimator;

    // Use this for initialization
    void Start () {
        FindObjectOfType<AudioManager>().BGM_Play("Tutorial_01");
        fadeAnimator = GameObject.Find("Fade").GetComponent<Animator>();
        fadeAnimator.Play("Fade_in");
    }
	
    public void endTutorial()
    {
        StartCoroutine(end());
    }

    IEnumerator end()
    {
        fadeAnimator.Play("Fade_out");
        yield return new WaitForSeconds(2f);
        Director.TutorialState = true;

        SaveLoadSystem.saveData();
        SceneManager.LoadScene("DhomaStage");
    }
}
