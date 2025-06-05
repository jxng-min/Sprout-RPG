using UnityEngine;

public class PlayerDamageState : MonoBehaviour, IState<PlayerCtrl>
{
    private PlayerCtrl m_player_ctrl;
    public void ExecuteEnter(PlayerCtrl sender)
    {
        if (!m_player_ctrl)
        {
            m_player_ctrl = sender;
        }

        m_player_ctrl.Health.Hurt();
    }

    public void Execute()
    {
        if (!m_player_ctrl.Health.IsStagger)
        {
            m_player_ctrl.ChangeState(PlayerState.IDLE);
        }
    }

    public void FixedExecute() {}

    public void ExecuteExit() {}
}
