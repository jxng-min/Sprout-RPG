using UnityEngine;

public class PlayerIdleState : MonoBehaviour, IState<PlayerCtrl>
{
    private PlayerCtrl m_player_ctrl;

    public void ExecuteEnter(PlayerCtrl sender)
    {
        if (!m_player_ctrl)
        {
            m_player_ctrl = sender;
        }

        m_player_ctrl.Animator.SetBool("IsMove", false);
    }

    public void Execute()
    {
        if (m_player_ctrl.Movement.IsMoving())
        {
            m_player_ctrl.ChangeState(PlayerState.MOVE);
        }
    }

    public void FixedExecute() {}

    public void ExecuteExit() {}
}
