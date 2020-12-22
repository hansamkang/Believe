using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director{
    private static bool gameOnState = true; // true == Game turn On, false == Game not turn On 
    private static bool tutorialState = false;  // true == Cleared, false == Not Cleared
    private static bool dhomaState = false; // true == Cleared, false == Not Cleared

   public static bool GameOnState
   {
        get { return gameOnState; }
        set { gameOnState = value; }
   }

    public static bool TutorialState
    {
        get { return tutorialState; }
        set { tutorialState = value; }
    }
    
    public static bool DhomaState
    {
        get { return dhomaState; }
        set { dhomaState = false; }
    }
}
