using UnityEngine;

public class EnemyDamageState : MonoBehaviour, IState<EnemyCtrl>
{
    private EnemyCtrl m_enemy_ctrl;

    public void ExecuteEnter(EnemyCtrl sender)
    {
        if (!m_enemy_ctrl)
        {
            m_enemy_ctrl = sender;
        }

        m_enemy_ctrl.Movement.Reset();
        m_enemy_ctrl.Health.Hurt();
    }

    public void Execute()
    {
        if (!m_enemy_ctrl.Health.IsStaggered)
        {
            m_enemy_ctrl.ChangeState(EnemyState.IDLE);
        }
    }
    
    public void FixedExecute() {}

    public void ExecuteExit() {}
}
