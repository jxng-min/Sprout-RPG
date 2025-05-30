public class PlayerStateContext
{
    #region Variables
    private readonly PlayerCtrl m_player_ctrl;
    private IState<PlayerCtrl> m_current_state;
    #endregion Variables

    #region Properties
    public IState<PlayerCtrl> Current { get => m_current_state; }
    #endregion Properties

    #region Helper Methods
    public PlayerStateContext(PlayerCtrl player_ctrl)
    {
        m_player_ctrl = player_ctrl;
    }

    public void Transition(IState<PlayerCtrl> player_state)
    {
        if (m_current_state == player_state)
        {
            return;
        }

        m_current_state?.ExecuteExit();
        m_current_state = player_state;
        m_current_state?.ExecuteEnter(m_player_ctrl);
    }

    public void ExecuteUpdate()
    {
        if (!m_player_ctrl)
        {
            return;
        }

        m_current_state.Execute();
    }

    public void FixedExecuteUpdate()
    {
        if (!m_player_ctrl)
        {
            return;
        }

        m_current_state.FixedExecute();
    }
    #endregion Helper Methods
}