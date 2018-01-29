using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item{
    public int Id;
    public int Num;

    public Item(int id, int num)
    {
        Id = id;
        Num = num;
    }
}
