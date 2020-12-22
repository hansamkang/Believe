﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class HeartSystem : MonoBehaviour {

    private int maxHeartAmount = 5;
    public int startHearts = 3;
    public int curHealth;
    //private int maxHealth;
    private int healthPerHeart=2;

    public Image[] healthImages;
    public Sprite[] healthSprites;

    void Start(){
        curHealth = startHearts * healthPerHeart;
        //maxHealth = maxHeartAmount * healthPerHeart;
        checkHealthAmount();
    }

    void checkHealthAmount()
    {
        for (int i = 0; i < maxHeartAmount; i++)
        {
            if (startHearts <= i)
            {
                healthImages[i].enabled = false;
            }
            else
            {
                healthImages[i].enabled = true;
            }
        }
        updateHearts();
    }

    void updateHearts () {
        bool empty = false;
        int i = 0;  

        foreach(Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprites[0];
            }
            else
            {
                i++;
                if(curHealth >= i* healthPerHeart)
                {
                    image.sprite = healthSprites[healthSprites.Length - 1];
                }
                else
                {
                    int currentHeartHealth = (int)(healthPerHeart - (healthPerHeart * i - curHealth));
                    int healthPerImage = healthPerHeart / (healthSprites.Length -1);
                    int imageIndex = currentHeartHealth / healthPerImage;
                    image.sprite = healthSprites[imageIndex];
                    empty = true;
                }
            }
        }
	}

    public void takeDamage(int amount)
    {
        curHealth -= amount;
        curHealth = Mathf.Clamp(curHealth, 0, startHearts * healthPerHeart);
        updateHearts();
    }

    public void addHeartContainer()
    {
        startHearts++;
        startHearts = Mathf.Clamp(startHearts, 0, maxHeartAmount);

        //curHealth = startHearts * healthPerHeart;
        //maxHealth = maxHeartAmount * healthPerHeart;

        checkHealthAmount();
    }

}
