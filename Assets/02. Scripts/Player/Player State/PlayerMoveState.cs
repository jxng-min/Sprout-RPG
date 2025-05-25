using UnityEngine;

public class PlayerMoveState : MonoBehaviour, IState<PlayerCtrl>
{
    private PlayerCtrl m_player_ctrl;

    public void ExecuteEnter(PlayerCtrl sender)
    {
        if (!m_player_ctrl)
        {
            m_player_ctrl = sender;
        }

        m_player_ctrl.Animator.SetBool("IsMove", true);
    }

    public void Execute()
    {
        if (!m_player_ctrl.Movement.IsMoving())
        {
            m_player_ctrl.ChangeState(PlayerState.IDLE);
        }
    }

    public void FixedExecute()
    {
        m_player_ctrl.Movement.Move(m_player_ctrl.Movement.SPD);
    }

    public void ExecuteExit() {}
}
