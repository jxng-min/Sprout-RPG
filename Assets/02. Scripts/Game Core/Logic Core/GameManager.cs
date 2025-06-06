using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Variables
    private GameEventType m_current;
    private PlayerCtrl m_player_ctrl;
    private CursorManager m_cursor;

    private bool m_can_init = true;
    #endregion Variables

    #region Properties
    public GameEventType Current { get => m_current; }
    public PlayerCtrl Player { get => m_player_ctrl; }
    public CursorManager Cursor { get => m_cursor; }
    #endregion Properties

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventType.LOGIN, Login);
        GameEventBus.Subscribe(GameEventType.LOADING, Loading);
    }

    private void Login()
    {
        m_current = GameEventType.LOGIN;

        m_can_init = true;

        Cursor.SetCursor(CursorMode.DEFAULT);
    }

    private void Loading()
    {
        m_current = GameEventType.LOADING;

        Cursor.SetCursor(CursorMode.WAITING);
        ObjectManager.Instance.ReturnObjectsAll();
    }

    public void Playing()
    {
        m_current = GameEventType.PLAYING;

        if (m_can_init)
        {
            m_can_init = false;

            Cursor.SetCursor(CursorMode.DEFAULT);

            m_player_ctrl = FindFirstObjectByType<PlayerCtrl>();
            Camera.main.transform.position = DataManager.Instance.PlayerData.Data.Camera;
        }
        else
        {

        }
    }

    public void Checking()
    {
        m_current = GameEventType.CHECKING;

        Cursor.SetCursor(CursorMode.DEFAULT);
    }

    public void Pausing()
    {
        m_current = GameEventType.PAUSING;

        Cursor.SetCursor(CursorMode.DEFAULT);
    }

    public void Dead()
    {
        m_current = GameEventType.DEAD;

        Cursor.SetCursor(CursorMode.DEFAULT);
    }

    public void Clear()
    {
        m_current = GameEventType.CLEAR;

        Cursor.SetCursor(CursorMode.DEFAULT);
    }
}
