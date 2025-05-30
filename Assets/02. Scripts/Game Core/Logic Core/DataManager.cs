using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    #region Variables
    private PlayerData m_player_data;
    #endregion Variables

    #region Properties
    public PlayerData Data
    {
        get => m_player_data;
        set => m_player_data = value;
    }
    #endregion Properties

    private new void Awake()
    {
        base.Awake();

        m_player_data = new PlayerData();
    }

    #region Helper Methods
    public void Save()
    {
        m_player_data.Position = GameManager.Instance.Player.transform.position;
        FindFirstObjectByType<Timer>().Save();
        m_player_data.Camera = Camera.main.transform.position;

        var inventory_slots = FindAnyObjectByType<Inventory>().Slots;
        for (int i = 0; i < inventory_slots.Length; i++)
        {
            m_player_data.Inventory[i] = inventory_slots[i].Item == null ? new SlotData(-1, 1) : new SlotData(inventory_slots[i].Item.ID, inventory_slots[i].Count);
        }

        var equipment_slots = FindAnyObjectByType<Equipment>().Slots;
        for (int i = 0; i < equipment_slots.Length; i++)
        {
            m_player_data.Equipment[i] = equipment_slots[i].Item == null ? new SlotData(-1, 1) : new SlotData(equipment_slots[i].Item.ID, equipment_slots[i].Count);
        }
    }

    public void Load(PlayerData data)
    {
        m_player_data = data;
    }
    #endregion Helper Methods
}