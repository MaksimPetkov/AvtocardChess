using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    [SerializeField]
    public GameObject[] BorderCell;
    [SerializeField]
    public GameObject Border;


    void Start()
    {
        FindBoarderCells();
    }


    void Update()
    {

    }

    void FindBoarderCells()
    {
        for (int i = 0; i < BorderCell.Length; i++)
        {
            BorderCell[i] = Border.transform.GetChild(i).gameObject;
        }

    }
}

