using UnityEngine;

[System.Serializable]
public abstract class SubQuest
{
    #region Variables
    [Header("서브 퀘스트의 ID")]
    [SerializeField] private int m_subquest_id;

    [Header("퀘스트 클리어 여부")]
    [SerializeField] private bool m_is_clear;
    #endregion Variables

    #region Properties
    public bool IsClear
    {
        get => m_is_clear;
        set => m_is_clear = value;
    }
    #endregion Properties

    #region Helper Methods
    public abstract string GetText();
    #endregion Helper Methods
}