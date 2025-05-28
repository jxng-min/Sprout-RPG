using UnityEngine;

[System.Serializable]
public struct SlotData
{
    public int ID;
    public int Count;

    public SlotData(int id = -1, int count = 1)
    {
        ID = id;
        Count = count;
    }
}

[System.Serializable]
public class PlayerData
{
    public Vector3 Position;
    public int LV;
    public int EXP;
    public int Money;
    public SlotData[] Inventory;
    public SlotData[] Equipment;
    public float PlayTime;

    public PlayerData()
    {
        Position = new Vector3(14f, -15f, 0f);
        LV = 1;
        EXP = 0;
        Money = 0;

        Inventory = new SlotData[24];
        for (int i = 0; i < 24; i++)
        {
            Inventory[i].ID = -1;
            Inventory[i].Count = 0;
        }

        Equipment = new SlotData[4];
        for (int i = 0; i < 4; i++)
        {
            Equipment[i].ID = -1;
            Equipment[i].Count = 0;

            if (i == 2)
            {
                Equipment[i].ID = 20;
                Equipment[i].Count = 1;
            }
        };

        PlayTime = 0f;
    }
}