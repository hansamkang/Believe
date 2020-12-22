﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    public int startHP;
    public int currentHP;
    protected Transform playerTransform;
    protected Transform enemyTransform;
    protected Animator animator;
    protected Vector2 playerVectorX;  //플레이어의 x값의 저장된 Vector2 변수 초기화
    protected bool viewRight = false;
    protected float positionDuration; //보스와 플레이어간의 간격, 음수일때 보스 오른쪽, 양수일때 보스 왼쪽
    protected GameObject[] enemy;

    protected GameObject HpGauge;
    float waitTime = 0.1f;

    

    protected IEnumerator demage()
    {
        Color originalColor = new Color(1f, 1f, 1f, 1f);
        Color cColor = new Color(1f, 0f, 0f, 1f);
        changeColor(cColor);
        yield return new WaitForSeconds(waitTime);
        changeColor(originalColor);
        yield return new WaitForSeconds(waitTime);
    }

    void changeColor(Color color) //색 변경 함수
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            SpriteRenderer render = enemy[i].GetComponent<SpriteRenderer>();
            render.color = color;
        }
    }

    protected void decreaseUIHP()
    {
        HpGauge.GetComponent<Image>().fillAmount -= 1f / (float)startHP;
    }

    protected void setStartHP(int a)
    {
        startHP = a;
        currentHP = startHP;
    }
}
