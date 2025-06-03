using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyAttack2D : MonoBehaviour
{
    #region Variables
    [Header("몬스터 컨트롤러")]
    [SerializeField] private EnemyCtrl m_enemy_ctrl;

    [Header("플레이어 탐지 거리")]
    [Range(2f, 5f)][SerializeField] private float m_radius;

    [Header("플레이어의 레이어")]
    [SerializeField] LayerMask m_player_layer;

    private int m_atk;
    private float m_cooltime;
    #endregion Variables

    #region Properties
    public int ATK
    {
        get => m_atk;
        set => m_atk = value;
    }
    #endregion Properties

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }

    #region Helper Methods
    public void Initialize(Enemy enemy)
    {
        m_enemy_ctrl = GetComponent<EnemyCtrl>();

        m_atk = enemy.ATK;
        m_cooltime = enemy.Cooltime;
    }
    public Collider2D SearchTarget()
    {
        var hit = Physics2D.OverlapCircle(transform.position, m_radius, m_player_layer);
        if (hit)
        {
            m_enemy_ctrl.ChangeState(EnemyState.TRACE);
        }

        return hit;
    }
    #endregion Helper Methods
}
