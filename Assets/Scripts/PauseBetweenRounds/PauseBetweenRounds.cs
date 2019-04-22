using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseBetweenRounds : MonoBehaviour {

    float timer;

    public bool StartPause = false;       //проверка стартовала ли пауза
    bool ReadyActiveBuyPanel = false;    // переменная для проверки готова ли панель к активации
    bool kalkulatedCard = false;      //проверка были ли обнволены карты в магазине(панели)
    [SerializeField]
    float timerMAX = 10f;
    Game Game;

    public GameObject BuyPanel;       // на сцене висит объект BuyPanel юзается для покупки карт

    public List<GameObject> AllCardsForBuy;         // все карты доступные для покупки 1 звёздочные
    PlayerScript playerScript;               //передаём значение на стол

    // Use this for initialization
    void Start ()
    {
        Game = GameObject.Find("Assist").GetComponent<Game>();
        BuyPanel = GameObject.Find("BuyPanel");
        DeactivateBuyPanel();
        playerScript = GameObject.Find("NotDestoygGameObject").GetComponent<PlayerScript>();
        Invoke("Test", 2f);
	}
	


	void Update ()
    {
        TimerUp();
	}


    // действия с таймерами
    void TimerUp()
    {
        if (StartPause)    // если пауза началась
        { 
            if (ReadyActiveBuyPanel)          // если панель доступна для активирования
            {
                ActiveBuyPanel();

                if (!kalkulatedCard)               // если ещё не рассчитывали карты для покупки(забивание в массив)
                {
                    KalkulatateCardForBuy();
                }
            }

            if (timer < timerMAX)                       // таймер,длится по желаннию
            {
                timer += Time.deltaTime;
                Game.RefreshTimerText(timer);
                if(timer >= timerMAX)                        // конец таймера
                {
                    Game.EndRound();
                    EndTImer();
                }
            }
        }
    }
    // обнуление таймера и начинание игры
    void EndTImer()
    {
        StartPause = false;
        timer = 0;
        ClearKalkulatateCard();
        kalkulatedCard = false;
        DeactivateBuyPanel();       //насильное закрытие магазина
    }
    void Test()
    {
        StartPause = true;
        ReadyActiveBuyPanel = true;
    }

    //show timer
    void ShowTimer()
    {
         
    }
    //состояния панели для закупки карт
    void ActiveBuyPanel()
    {
        BuyPanel.SetActive(true);
    }

    //состояния панели для закупки карт 
    void DeactivateBuyPanel()
    {
        BuyPanel.SetActive(false);
    }

    //рассчитывание карт для покупки в начале каждого раунда     //в цикле исльпользуется число количества окошек на панели для покупки
    void KalkulatateCardForBuy()
    {
        kalkulatedCard = true;
        for (int i = 0; i < 6; i++)
        {
            int a = Random.Range(0, AllCardsForBuy.Count);
            GameObject game = Instantiate(AllCardsForBuy[a]);
            //  BuyPanel.transform.GetChild(i).SetParent(Instantiate(AllCardsForBuy[a].transform));
            game.transform.SetParent(BuyPanel.transform.GetChild(i));
            BuyPanel.transform.GetChild(i).GetComponent<Image>().sprite = Resources.Load<Sprite>(game.GetComponent<CardCharacter>().pathImage);
           
        }
    }

    //завершение,чистка массива из покупки
    void ClearKalkulatateCard()
    {
        for (int i = 0; i < 6; i++)
        {
            BuyPanel.transform.GetChild(i).GetComponent<Image>().sprite = null;

            if (BuyPanel.transform.GetChild(i).childCount > 0)
            {
                Destroy(BuyPanel.transform.GetChild(i).GetChild(0).gameObject);
            }

            BuyPanel.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    // переход в главное меню
    public void EndScene()
    {
        playerScript.ClearBoard();
        playerScript.firstStart = false;
        SceneManager.LoadScene(1);
    }

}
