using System.Collections.Generic;
using UnityEngine;

public enum CursorMode
{
    DEFAULT = 0,
    CAN_GRAB = 1,
    GRAB = 2,
    WAITING = 3,
    CAN_TALK = 4,
    CAN_ATTACK = 5,
}

[System.Serializable]
public struct CursorData
{
    public CursorMode Mode;
    public Texture2D Cursor;
    public Vector2 Hotspot;
}

public class CursorManager : MonoBehaviour
{
    #region Variables
    [Header("커서 데이터")]
    [SerializeField] private List<CursorData> m_cursor_datas;

    private Dictionary<CursorMode, Texture2D> m_cursor_dict;
    private Dictionary<CursorMode, Vector2> m_hotspot_dict;

    private CursorMode m_current_mode;
    #endregion Variables

    #region Properties
    public CursorMode Current { get => m_current_mode; }
    #endregion Properties

    private void Awake()
    {
        Initialize();
    }

    #region Helper Methods
    private void Initialize()
    {
        m_cursor_dict = new();
        m_hotspot_dict = new();
        for (int i = 0; i < m_cursor_datas.Count; i++)
        {
            m_cursor_dict.Add(m_cursor_datas[i].Mode, m_cursor_datas[i].Cursor);
            m_hotspot_dict.Add(m_cursor_datas[i].Mode, m_cursor_datas[i].Hotspot);
        }
    }

    public void SetCursor(CursorMode mode)
    {
        if (m_cursor_dict.TryGetValue(mode, out var cursor))
        {
            if (m_hotspot_dict.TryGetValue(mode, out var hotspot))
            {
                Vector2 pivot = new Vector2(cursor.width * hotspot.x, cursor.height * hotspot.y);
                Cursor.SetCursor(cursor, pivot, UnityEngine.CursorMode.Auto);
            }
        }
    } 
    #endregion Helper Methods
}
