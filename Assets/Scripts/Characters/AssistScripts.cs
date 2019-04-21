using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistScripts : MonoBehaviour {
    public GameObject[] allTargedets;
    public List<GameObject> readyforAttack;


    public bool StartsGame;
    public int[] busyCell;
    public List<int> cell;         //забитые ячейки
    public List<int> currentfigur;      //количество фигур
    // Use this for initialization
    void Start () {
        allTargedets = GameObject.FindGameObjectsWithTag("Target");
       // TargetsforAttack();
    }

    // Update is called once per frame
    void Update ()
    {
       
	}
    public void TargetsforAttack()
    {
        int a = allTargedets.Length;
        readyforAttack = new List<GameObject>(a);
        for(int i = 0; i < a; i++)
        {
            readyforAttack.Add(allTargedets[i]);
        }
    }

   

    public void TargetsUpdate()
    {
        int a = currentfigur.Count;

        for (int i = 0; i < a ; i++)
        {
            busyCell[i] = cell[i];
        }
    }
}
