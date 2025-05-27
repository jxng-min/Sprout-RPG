using UnityEngine;

public class PlayerAttackState : MonoBehaviour, IState<PlayerCtrl>
{
    private PlayerCtrl m_player_ctrl;

    public void ExecuteEnter(PlayerCtrl sender)
    {
        if (!m_player_ctrl)
        {
            m_player_ctrl = sender;
        }

        m_player_ctrl.Attacking.Weapon.Use();
    }

    public void Execute()
    {
        // m_player_ctrl.Attacking.Attack();

        if (m_player_ctrl.Movement.IsMoving())
        {
            m_player_ctrl.ChangeState(PlayerState.MOVE);
        }
        else
        {
            m_player_ctrl.ChangeState(PlayerState.IDLE);
        }
    }

    public void FixedExecute() {}

    public void ExecuteExit() {}
}
