using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CardCharacter : MonoBehaviour {
    AssistScripts assist;
    public CardType cardType;
    public starsCharacter starCharacter;
    public SideCard sideCard;
    Board board;
    public string pathImage;

    [SerializeField]
    int hp, attack;
    [SerializeField]
    float atackspeed;

    Animation animation;
    [SerializeField]
    GameObject Target;

    [SerializeField]
    AnimationClip[] clips;                   // 0-idle,1-attack,2-jump
    int pose;
    public int costmoney;

    bool startgame = false;
    [SerializeField]
    List<int> freeposition;

    // Use this for initialization
    void Start () {
        board = GameObject.Find("Board").GetComponent<Board>();
        assist = GameObject.Find("Assist").GetComponent<AssistScripts>();
        animation = GetComponent<Animation>();
        assist.currentfigur.Add(1);

        Invoke("FindTarget", 2f);
	}
	
	// Update is called once per frame
	void Update ()
    {
       
	}


    void FindTarget()
    {

        if (sideCard == SideCard.Playercard)
        {
            int a = UnityEngine.Random.Range(0, assist.readyforAttack.Count);
            Target = assist.readyforAttack[a];
            assist.readyforAttack.RemoveAt(a);
            int b = UnityEngine.Random.Range(0, 20);
            KalculatePosition();
            Target.GetComponent<CardCharacter>().Duel(b,this.gameObject,freeposition,pose);
        }
    }

    void JumpTarget(List<int> q,int Targetpose)
    {
      //  int a = UnityEngine.Random.Range(0,q.Count);
     //   int b = q[a];

        RaycastHit hit;

        for (int i = 0; i < q.Count; i++)
        {
           int  b = q[i];
            if (Physics.Raycast(board.transform.GetChild(b).transform.position, board.transform.GetChild(Targetpose).transform.position, out hit))
            {
                if (board.transform.GetChild(b).childCount == 0)
                {
                    if (hit.transform.name == Convert.ToString(Targetpose))
                    {
                        Debug.Log(board.transform.GetChild(b).name);
                        transform.position = new Vector3(board.transform.GetChild(b).transform.position.x, board.transform.GetChild(b).transform.position.y, board.transform.GetChild(b).transform.position.z);
                        break;
                    }
                }
            }
        }
    }

    void CommonTarget(List<int> avs, int Targetpose)
    {
        int rndA = UnityEngine.Random.Range(0, avs.Count);
        int b = avs[rndA];

        RaycastHit hit;


        for (int i = 0; i < avs.Count; i++)
        {
            b = avs[i];
            if(Physics.Raycast(board.transform.GetChild(b).transform.position,board.transform.GetChild(Targetpose).transform.position,out hit))
            {
                if (board.transform.GetChild(b).childCount == 0)
                {
                    if (hit.transform.name == Convert.ToString(Targetpose))
                    {
                        Debug.Log(board.transform.GetChild(b).name);
                        transform.position = new Vector3(board.transform.GetChild(b).transform.position.x, board.transform.GetChild(b).transform.position.y, board.transform.GetChild(b).transform.position.z);
                        break;
                    }
                }
            }
        }
    }



    public void UpPosition()
    {
        transform.SetParent(board.transform.GetChild(pose));
        transform.position = board.transform.GetChild(pose).transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "cell")
        {
            pose = Convert.ToInt32(collision.transform.name);
            assist.cell.Add(pose);
            UpPosition();
        }
    }

    public void Swap(int a,GameObject b)
    {
        board.Border.transform.GetChild(a).SetParent(b.transform);
        b.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void KalculatePosition()
    {
        int z;
        z = pose + 1;
        {
            if (z >= 0 && z <= 63)
            {
                if (board.Border.transform.GetChild(z).childCount == 0)
                {
                    freeposition.Add(z);
                    Debug.Log(z);
                }
            }
        }
        z = pose - 1;
        {
            if (z >= 0 && z <= 63)
            {
                if (board.Border.transform.GetChild(z).childCount == 0)
                {
                    freeposition.Add(z);
                    Debug.Log(z);
                }
            }
        }
        z = pose - 8;
        {
            if (z >= 0 && z <= 63)
            {
                if (board.Border.transform.GetChild(z).childCount == 0)
                {
                    freeposition.Add(z);
                    Debug.Log(z);
                }
            }
        }
        z = pose + 8;
        {
            if (z >= 0 && z <= 63)
            {
                if (board.Border.transform.GetChild(z).childCount == 0)
                {
                    freeposition.Add(z);
                    Debug.Log(z);
                }
            }
        }
    }

    public void Duel(int a, GameObject lokaltarget,List<int> vs,int positionTarget)                     //a приходит число в дуэль
    {
        Target = lokaltarget;
        freeposition.Clear();
        int q = pose;
        int b = UnityEngine.Random.Range(0, 20);
        if (a > b)
        {
            CommonTarget(vs,positionTarget);
        }

        if (a <= b)
        {
            int z = q + 1;
            {  
                    if (z >= 0 && z <= 63)
                    {
                        if (board.Border.transform.GetChild(z).childCount == 0)
                        {
                            freeposition.Add(z);
                         Debug.Log(z);
                        }
                    }
            }
            z = q - 1;
            {
                if (z >= 0 && z <= 63)
                {
                    if (board.Border.transform.GetChild(z).childCount == 0)
                    {
                        freeposition.Add(z);
                        Debug.Log(z);
                    }
                }
            }
            z = q - 8;
            {
                if (z >= 0 && z <= 63)
                {
                    if (board.Border.transform.GetChild(z).childCount == 0)
                    {
                        freeposition.Add(z);
                        Debug.Log(z);
                    }
                }
            }
            z = q + 8;
            {
                if (z >= 0 && z <= 63)
                {
                    if (board.Border.transform.GetChild(z).childCount == 0)
                    {
                        freeposition.Add(z);
                        Debug.Log(z);
                    }
                }
            }
            Target.GetComponent<CardCharacter>().JumpTarget(freeposition,pose);
        }
    }
   public enum CardType
    {
        undead,
        human,
        ciclops,
        dwarfs,
        goblins
    }

    public enum starsCharacter
    {
        Star1,
        Star2
    }

    public enum SideCard
    {
        EnemyCard,
        Playercard
    }   
}
