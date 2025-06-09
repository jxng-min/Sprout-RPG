using UnityEngine;

[System.Serializable]
public class KillQuestData
{
    public EnemyCode m_enemy_code;
    public int m_count;
}

[System.Serializable]
public class KillQuest : SubQuest
{
    #region Variables
    [Header("데이터")]
    [SerializeField] private KillQuestData m_kill_data;

    private int m_current_count;
    #endregion Variables

    #region Properties
    public KillQuestData Target { get => m_kill_data; }
    public int Current
    {
        get => m_current_count;
        set => m_current_count = value;
    }
    #endregion Properties

    #region Helper Methods
    public override string GetText()
    {
        return $"{Mathf.Clamp(Current, 0, Target.m_count) / Target.m_count}";
    }
    #endregion Helper Methods
}