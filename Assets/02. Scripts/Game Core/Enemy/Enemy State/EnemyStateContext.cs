using UnityEngine;

public class EnemyStateContext
{
    #region Variables
    private readonly EnemyCtrl m_enemy_ctrl;
    private IState<EnemyCtrl> m_current_state;
    #endregion Variables

    #region Properties
    public IState<EnemyCtrl> Current { get => m_current_state; }
    #endregion Properties

    #region Helper Methods
    public EnemyStateContext(EnemyCtrl enemy_ctrl)
    {
        m_enemy_ctrl = enemy_ctrl;
    }

    public void Transition(IState<EnemyCtrl> enemy_state)
    {
        if (m_current_state == enemy_state)
        {
            return;
        }

        m_current_state?.ExecuteExit();
        m_current_state = enemy_state;
        m_current_state?.ExecuteEnter(m_enemy_ctrl);
    }

    public void ExecuteUpdate()
    {
        if (!m_enemy_ctrl)
        {
            return;
        }

        m_current_state?.Execute();
    }

    public void FixedExecuteUpdate()
    {
        if (!m_enemy_ctrl)
        {
            return;
        }

        m_current_state?.FixedExecute();
    }
    #endregion Helper Methods
}
