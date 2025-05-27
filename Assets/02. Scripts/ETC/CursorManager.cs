using System.Collections.Generic;
using UnityEngine;

public enum CursorMode
{
    DEFAULT = 0,
    CAN_GRAB = 1,
    GRAB = 2,
    WAITING = 3,
    CAN_ATTACK = 4,
}

[System.Serializable]
public struct CursorData
{
    public CursorMode Mode;
    public Texture2D Cursor;
}

public class CursorManager : Singleton<CursorManager>
{
    #region Variables
    [Header("커서 데이터")]
    [SerializeField] private List<CursorData> m_cursor_datas;

    private Dictionary<CursorMode, Texture2D> m_cursor_dict;

    private CursorMode m_current_mode;
    #endregion Variables

    #region Properties
    public CursorMode Current { get => m_current_mode; }
    #endregion Properties

    private new void Awake()
    {
        base.Awake();

        Initialize();
    }

    private void Initialize()
    {
        m_cursor_dict = new();
        for (int i = 0; i < m_cursor_datas.Count; i++)
        {
            m_cursor_dict.Add(m_cursor_datas[i].Mode, m_cursor_datas[i].Cursor);
        }
    }

    public void SetCursor(CursorMode mode)
    {
        if (m_cursor_dict.TryGetValue(mode, out var cursor))
        {
            Cursor.SetCursor(cursor, Vector2.zero, UnityEngine.CursorMode.Auto);
        }
    } 
}
