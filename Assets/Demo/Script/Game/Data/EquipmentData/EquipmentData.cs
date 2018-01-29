using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData : BaseData {
    public int helment;
    public int clothes;
    public int pants;
    public int shoes;
    public int necklace;
    public int ring;
    public int weapon;
}

public enum enumEquipment
{
    weapon=1,
    helment,
    clothes,
    pants,
    shoes,
    necklace,
    ring
}