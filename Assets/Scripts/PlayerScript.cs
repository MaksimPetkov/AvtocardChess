using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    public bool firstStart = true;
    public List<GameObject> PlayerCard;          // карты с фигурами
    public GameObject BoardWithCard;        //поле с неактивными фигурами    8 мест на борде


                                     // все готовые префабы готовых карт из папки

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(1);
    }

    void Start ()
    {

	}
	

	void Update () {
		
	}


    public void findBoardPlayer()
    {
        BoardWithCard = GameObject.Find("NotBoard(Player)");
    }

    // чистка стека покупных карт
    public void ClearBoard()
    {
        for(int i = 1;i < (BoardWithCard.transform.childCount); i++)
        {
            //удаляет объекты с поля
            if (BoardWithCard.transform.GetChild(i).transform.childCount > 0)
            {
                Destroy(BoardWithCard.transform.GetChild(i).GetChild(0).transform.gameObject);

            }

            //бля,хз почему но массив отрабатывается именно такое количество раз,а также чистит массив который лежит в неудаляемом объекте гуляещему по сценам
            if (i == BoardWithCard.transform.childCount - 1)
            {
                PlayerCard.Clear();
            }
        }

    }
}
