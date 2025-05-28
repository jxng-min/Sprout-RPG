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
}