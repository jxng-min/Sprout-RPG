using Unity.VisualScripting;
using UnityEngine;

public class EnemyTraceState : MonoBehaviour, IState<EnemyCtrl>
{
    private EnemyCtrl m_enemy_ctrl;

    public void ExecuteEnter(EnemyCtrl sender)
    {
        if (!m_enemy_ctrl)
        {
            m_enemy_ctrl = sender;
        }

        Debug.Log("TRACE 진입");
        InvokeRepeating(nameof(UpdateTrace), 0f, 0.5f);
    }

    public void Execute()
    { 
        // TODO: 몬스터 데미지 로직
    }
    
    public void FixedExecute() { }

    public void ExecuteExit()
    {
        CancelInvoke(nameof(UpdateTrace));
        m_enemy_ctrl.Movement.Reset();
    }

    #region Helper Methods
    private void UpdateTrace()
    {
        var player = m_enemy_ctrl.Attacking.SearchTarget();
        if (player == null)
        {
            m_enemy_ctrl.ChangeState(EnemyState.IDLE);
        }
        else
        {
            m_enemy_ctrl.Animator.SetBool("IsMove", true);
            m_enemy_ctrl.Movement.Trace(player.transform.position);
        }
    }
    #endregion Helper Methods
}
