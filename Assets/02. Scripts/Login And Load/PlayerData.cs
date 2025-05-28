using System.Collections.Generic;
using UnityEngine;

public struct SlotData
{
    public int ID;
    public int Count;
}

[System.Serializable]
public class PlayerData
{
    public Vector3 Position;
    public int LV;
    public int EXP;
    public List<SlotData> Inventory;
    public List<SlotData> Equipment;
    public float PlayTime;
}