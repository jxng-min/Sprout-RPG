using Unity.VisualScripting;
using UnityEngine;

public class PlayerDeadState : MonoBehaviour, IState<PlayerCtrl>
{
    private PlayerCtrl m_player_ctrl;
    public void ExecuteEnter(PlayerCtrl sender)
    {
        if (!m_player_ctrl)
        {
            m_player_ctrl = sender;
        }

        m_player_ctrl.Health.Death();
    }

    public void Execute() {}

    public void FixedExecute() {}

    public void ExecuteExit() {}
}
