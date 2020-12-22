using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData{
    public bool tutorialCleared;
    public bool DhomaCleared;

    public GameData()
    {
        this.tutorialCleared = Director.TutorialState;
        this.DhomaCleared = Director.DhomaState;
    }
    public GameData(GameData data)
    {
        this.tutorialCleared = data.tutorialCleared;
        this.DhomaCleared = data.DhomaCleared;
    }
}
