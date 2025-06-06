using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    #region Variables
    private PlayerDataManager m_player_data;

    private ItemDataManager m_item_data;
    #endregion Variables

    #region Properties
    public PlayerDataManager PlayerData { get => m_player_data; }
    public ItemDataManager ItemData { get => m_item_data; }
    #endregion Properties

    private new void Awake()
    {
        base.Awake();

        m_player_data = GetComponent<PlayerDataManager>();
        m_item_data = GetComponent<ItemDataManager>();
    }


}