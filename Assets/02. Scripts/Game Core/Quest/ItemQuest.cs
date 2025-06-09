using UnityEngine;

[System.Serializable]
public class ItemQuestData
{
    public ItemCode m_item_code;
    public int m_count;
}

[System.Serializable]
public class ItemQuest : SubQuest
{
    #region Variables
    [Header("데이터")]
    [SerializeField] private ItemQuestData m_item_data;

    private int m_current_count;
    #endregion Variables

    #region Properties
    public ItemQuestData Target { get => m_item_data; }
    public int Current
    {
        get => m_current_count;
        set => m_current_count = value;
    }

    public override string GetText()
    {
        return $"{Mathf.Clamp(Current, 0, Target.m_count) / Target.m_count}";
    }
    #endregion Properties
}
