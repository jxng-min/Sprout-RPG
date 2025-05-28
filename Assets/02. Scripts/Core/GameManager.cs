using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Variables
    private GameEventType m_current;
    private PlayerCtrl m_player_ctrl;
    private bool m_can_init = true;
    #endregion Variables

    #region Properties
    public GameEventType Current { get => m_current; }
    public PlayerCtrl Player { get => m_player_ctrl; }
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
    }

    private void Loading()
    {
        m_current = GameEventType.LOADING;
    }

    public void Playing()
    {
        m_current = GameEventType.PLAYING;

        if (m_can_init)
        {
            m_can_init = false;
            
            m_player_ctrl = FindFirstObjectByType<PlayerCtrl>();
            Camera.main.transform.position = DataManager.Instance.Data.Camera;
        }
        else
        {

        }
    }

    public void Checking()
    {
        m_current = GameEventType.CHECKING;
    }

    public void Pausing()
    {
        m_current = GameEventType.PAUSING;
    }

    public void Dead()
    {
        m_current = GameEventType.DEAD;
    }

    public void Clear()
    {
        m_current = GameEventType.CLEAR;
    }
}
