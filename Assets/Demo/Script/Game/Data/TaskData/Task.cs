using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task  {
    public int Id;
    public int Num;
    public int MaxNum;
    public bool IsFinsh;

    public Task(int id,int num,int maxNum,bool isFinsh)
    {
        Id = id;
        Num = num;
        MaxNum = maxNum;
        IsFinsh = isFinsh;
    }
}
