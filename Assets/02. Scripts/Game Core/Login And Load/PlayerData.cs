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
public struct PlayerStatus
{
    public float MaxHP;
    public float MaxMP;
    public float HP;
    public float MP;
    public int ATK;
    public float SPD;

    public PlayerStatus(float max_hp, float max_mp, int atk, float spd)
    {
        MaxHP = max_hp;
        MaxMP = max_mp;
        HP = MaxHP;
        MP = MaxMP;
        ATK = atk;
        SPD = spd;
    }
}

[System.Serializable]
public class PlayerData
{
    public Vector3 Position;
    public Vector3 Camera;
    public int LV;
    public int EXP;
    public PlayerStatus Status;
    public int Money;
    public SlotData[] Inventory;
    public SlotData[] Equipment;
    public float PlayTime;

    public PlayerData()
    {
        Position = new Vector3(14f, -15f, 0f);
        Camera = new Vector3(13.5f, 0f, -10f);
        LV = 1;
        EXP = 0;
        Status = new PlayerStatus(100f, 70f, 0, 15f);
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