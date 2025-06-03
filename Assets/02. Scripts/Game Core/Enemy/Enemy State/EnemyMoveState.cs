using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : MonoBehaviour, IState<EnemyCtrl>
{
    private EnemyCtrl m_enemy_ctrl;

    public void ExecuteEnter(EnemyCtrl sender)
    {
        if (!m_enemy_ctrl)
        {
            m_enemy_ctrl = sender;
        }

        Debug.Log("MOVE 진입");
    }

    public void Execute()
    {
        // TODO: 몬스터 데미지 로직

        m_enemy_ctrl.Attacking.SearchTarget();

        if (!m_enemy_ctrl.Movement.IsMoving)
        {
            m_enemy_ctrl.Animator.SetBool("IsMove", true);
            m_enemy_ctrl.Movement.Move();
        }
    }

    public void FixedExecute() {}

    public void ExecuteExit()
    {
        m_enemy_ctrl.Movement.Reset();
    }
}
