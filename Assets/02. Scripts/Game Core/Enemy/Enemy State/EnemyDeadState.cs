using UnityEngine;

public class EnemyDeadState : MonoBehaviour, IState<EnemyCtrl>
{
    private EnemyCtrl m_enemy_ctrl;

    public void ExecuteEnter(EnemyCtrl sender)
    {
        if (!m_enemy_ctrl)
        {
            m_enemy_ctrl = sender;
        }

        m_enemy_ctrl.Health.Death();
    }

    public void Execute() {}

    public void FixedExecute() {}

    public void ExecuteExit() {}
}
