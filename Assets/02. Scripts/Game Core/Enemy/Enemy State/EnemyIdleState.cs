using UnityEngine;

public class EnemyIdleState : MonoBehaviour, IState<EnemyCtrl>
{
    private EnemyCtrl m_enemy_ctrl;
    private float m_wait_time;

    public void ExecuteEnter(EnemyCtrl sender)
    {
        if (!m_enemy_ctrl)
        {
            m_enemy_ctrl = sender;
        }

        m_enemy_ctrl.Animator.SetBool("IsMove", false);
        m_wait_time = Random.Range(1.0f, 3.0f);

        Debug.Log("IDLE 진입");
    }

    public void Execute()
    {
        // TODO: 몬스터 데미지 로직

        m_enemy_ctrl.Attacking.SearchTarget();

        m_wait_time -= Time.deltaTime;
        if (m_wait_time <= 0f)
        {
            m_enemy_ctrl.ChangeState(EnemyState.MOVE);
        }
    }

    public void FixedExecute() {}
    public void ExecuteExit() {}
}