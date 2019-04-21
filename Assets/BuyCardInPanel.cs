using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BuyCardInPanel : MonoBehaviour {

    public PlayerScript ps;
    public Game gamescript;

	// Use this for initialization
	void Start ()
    {
        ps = GameObject.Find("NotDestoygGameObject").GetComponent<PlayerScript>();
        gamescript = GameObject.Find("Assist").GetComponent<Game>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}



     public void BuyCard()
    {
        if (transform.childCount > 0)
     {
        GameObject card = transform.GetChild(0).gameObject;
        int i;

            int a;
            a = gamescript.moneyCount - transform.GetChild(0).GetComponent<CardCharacter>().costmoney; 

            if (a >= 0) {

                ps.findBoardPlayer();

                if (ps.PlayerCard.Count < 8)
                {
                    ps.PlayerCard.Add(card);
                    gamescript.UpdateMoney(transform.GetChild(0).GetComponent<CardCharacter>().costmoney);

                    i = 0;

                    for (i = 0; i < ps.PlayerCard.Count; i++)
                    {

                    }

                    card.transform.SetParent(ps.BoardWithCard.transform.GetChild(i));
                    card.transform.position = new Vector3(ps.BoardWithCard.transform.GetChild(i).transform.position.x, ps.BoardWithCard.transform.GetChild(i).transform.position.y, ps.BoardWithCard.transform.GetChild(i).transform.position.z);

                    GetComponent<Button>().interactable = false;
                }
            }

            if(a < 0)
            {
                gamescript.NotEnough();
            }

        }
    }
}
