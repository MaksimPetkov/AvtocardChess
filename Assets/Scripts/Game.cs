using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    [SerializeField]
    Text roundText, levelText, moneyText, hpText;                   // ссылки на текст
    [SerializeField]
    int needpointforup, currentExp = 0;      // переменны для функции NeedLevelPoint,колличество необходимого опыта, и количесство текущего опыта
    [SerializeField]
    public int roundCount = 0, moneyCount = 0, levelCount = 1, hpCount = 100;         //ссылки на числа
    [SerializeField]
    PauseBetweenRounds betweenRounds;  // получение переменной из скрипта паузы(пауза)
    public bool roundsgoing = false;            // переменная для вызова корутины


    void Start() {
        roundText = GameObject.FindGameObjectWithTag("PanelUI").transform.GetChild(0).GetChild(0).GetComponent<Text>();            //...
        moneyText = GameObject.FindGameObjectWithTag("PanelUI").transform.GetChild(1).GetChild(0).GetComponent<Text>();            // ....
        levelText = GameObject.FindGameObjectWithTag("PanelUI").transform.GetChild(2).GetChild(0).GetComponent<Text>();            //....
        hpText = GameObject.FindGameObjectWithTag("PanelUI").transform.GetChild(3).GetChild(0).GetComponent<Text>();               //Инициализация объектов есть префаб Canvas
        betweenRounds = GameObject.Find("PauseAssist").GetComponent<PauseBetweenRounds>();
        Invoke("Test", 2f);
    }

    void Update()
    {
        if (betweenRounds.StartPause)
        {
            if (!roundsgoing)
            {
                roundsgoing = true;
                StartCoroutine("UpdateTexts");
            }
        }
    }
    
    void Test()
    {
        StartCoroutine(UpdateTexts());
    }
    //конец раунда
    public void EndRound()
    {
        Invoke("NextRound", 5); 
        //
    }

    //обновление UI панели 
    void NextRound()
    {
        roundCount++;
        roundText.text = " " + roundCount;
    }

    void KalculateHP()
    {

        if (hpCount <= 0)
        {
            EndGame();
            hpCount = 0;
        }
        hpText.text = hpCount + " / 100";

    }
   
    //метод для обновления опыта,в переменную вбивается количество приходящего опыта
    void KalculateLevel(int a)
    {
        NeedLevelPoint(a);
        levelText.text = levelCount + " ";
    }

    //метод для проверки лвла и приход колличества опыта в еденицах
    void NeedLevelPoint(int q)
    {
        int residue = 0;
        int a = currentExp + q; ; // переменная содержащая в себе значение для добавления
        switch (levelCount)
        {
            case 1:
                needpointforup = 1;
                if(a >= needpointforup)        // если потенциальное повышение количество опыта больше чем нужно для апа
                {
                    levelCount++;
                    residue = (int)Mathf.Abs(needpointforup - a);         // модуль остатка

                    //если остаток после лвлапа больше чем нужно,то остаток переносим в след уровень
                    if(residue > 0)
                    {
                        currentExp = 0;
                        a = currentExp + residue;
                        goto case 2;
                    }
                }
                break;

            case 2:
                needpointforup = 2;
                currentExp = a;

                if (a >= needpointforup)
                {
                    levelCount++;

                    if(residue > 0)
                    {
                        if (needpointforup > a)   // если остатка нехватает для лвлапа;
                        {
                            currentExp = residue;      // количество опыта  = остатку;
                            residue = 0;
                        }
                       else                         // если остаток больше  чем нужно для лвлапа
                        {
                            residue = (int)Mathf.Abs(needpointforup - a);
                            currentExp = 0;
                            a = currentExp + residue;
                            goto case 3;
                        }
                    }
                }
                break;

            case 3:
                needpointforup = 4;
                currentExp = a;

                if (a >= needpointforup)
                {
                    levelCount++;
                    if (residue > 0)
                    {
                        if (needpointforup > a)   // если остатка нехватает для лвлапа;
                        {
                            currentExp = residue;      // количество опыта  = остатку;
                            residue = 0;
                        }
                        else                         // если остаток больше  чем нужно для лвлапа
                        {
                            residue = (int)Mathf.Abs(needpointforup - a);
                            currentExp = 0;
                            a = currentExp + residue;
                            goto case 4;
                        }
                    }
                }
                break;

            case 4:
                needpointforup = 8;
                currentExp = a;

                if (a >= needpointforup)
                {
                    levelCount++;

                    if (residue > 0)
                    {
                        if (needpointforup > a)   // если остатка нехватает для лвлапа;
                        {
                            currentExp = residue;      // количество опыта  = остатку;
                            residue = 0;
                        }
                        else                         // если остаток больше  чем нужно для лвлапа
                        {
                            residue = (int)Mathf.Abs(needpointforup - a);
                            currentExp = 0;
                            a = currentExp + residue;
                            goto case 5;
                        }
                    }
                }
                break;

            case 5:
                needpointforup = 16;
                currentExp = a;

                if (a >= needpointforup)
                {
                    levelCount++;

                    if (residue > 0)
                    {
                        if (needpointforup > residue)   // если остатка нехватает для лвлапа;
                        {
                            currentExp = residue;      // количество опыта  = остатку;
                            residue = 0;
                        }
                        else                         // если остаток больше  чем нужно для лвлапа
                        {
                            residue = (int)Mathf.Abs(needpointforup - a);
                            currentExp = 0;
                            a = currentExp + residue;
                            goto case 6;
                        }
                    }
                }
                break;

            case 6:
                needpointforup = 24;
                currentExp = a;

                if (a >= needpointforup)
                {
                    levelCount++;

                    if (residue > 0)
                    {
                        if (needpointforup > residue)   // если остатка нехватает для лвлапа;
                        {
                            currentExp = residue;      // количество опыта  = остатку;
                            residue = 0;
                        }
                        else                         // если остаток больше  чем нужно для лвлапа
                        {
                            residue = (int)Mathf.Abs(needpointforup - a);
                            currentExp = 0;
                            a = currentExp + residue;
                            goto case 7;
                        }
                    }
                }
                break;

            case 7:
                needpointforup = 38;
                currentExp = a;

                if (a >= needpointforup)
                {
                    levelCount++;

                    if (residue > 0)
                    {
                        if (needpointforup > residue)   // если остатка нехватает для лвлапа;
                        {
                            currentExp = residue;      // количество опыта  = остатку;
                            residue = 0;
                        }
                        else                         // если остаток больше  чем нужно для лвлапа
                        {
                            residue = (int)Mathf.Abs(needpointforup - a);
                            currentExp = 0;
                            a = currentExp + residue;
                            goto case 8;
                        }
                    }
                }
                break;

            case 8:
                needpointforup = 52;
                currentExp = a;

                if (a >= needpointforup)
                {
                    levelCount++;

                    if (residue > 0)
                    {
                        if (needpointforup > residue)   // если остатка нехватает для лвлапа;
                        {
                            currentExp = residue;      // количество опыта  = остатку;
                            residue = 0;
                        }
                        else                         // если остаток больше  чем нужно для лвлапа
                        {
                            residue = (int)Mathf.Abs(needpointforup - a);
                            currentExp = 0;
                            a = currentExp + residue;
                            goto case 9;
                        }
                    }
                }
                break;

            case 9:
                needpointforup = 64;
                currentExp = a;

                if (a >= needpointforup)
                {
                    levelCount++;

                    if (residue > 0)
                    {
                        if (needpointforup > residue)   // если остатка нехватает для лвлапа;
                        {
                            currentExp = residue;      // количество опыта  = остатку;
                            residue = 0;
                        }
                        else                         // если остаток больше  чем нужно для лвлапа
                        {
                            residue = (int)Mathf.Abs(needpointforup - a);
                            currentExp = 0;
                            a = currentExp + residue;
                            goto case 10;
                        }
                    }
                }
                break;

            case 10:
                needpointforup = 80;
                currentExp = a;

                if (currentExp >= needpointforup)
                {
                    currentExp = needpointforup;  //возврат максимума;
                }
                break;
        }
    }
    //рассчёт в конце раунда
    void KalculateMoney()
    {
        moneyCount = +5;
        if (moneyCount > 50)
        {
            moneyCount = +2;
        }


        moneyText.text = moneyCount + " ";
    }

    //рассчитывание монет после действия какого либо(покупка)
    public void UpdateMoney(int a) // обновление денег(после покупки например) и отображение
    {
        moneyCount -= a;
        moneyText.text = moneyCount + " ";
    }

    //детостаточно денег(отображение) 
    public void NotEnough()
    {
        Handheld.Vibrate();
        // c.lf gbcfnm 
    }

    void EndGame()
    {

    }
    


    // корутина для обновления текста(в данный момент во время Start)
    IEnumerator UpdateTexts()
    {
        roundsgoing = true;
        UpdateMoney(0);
        KalculateLevel(0);
        KalculateHP();
        yield return new WaitForSeconds(5f);             //таймер в pauseBetweenRoounds
        roundsgoing = false;
    }

}

