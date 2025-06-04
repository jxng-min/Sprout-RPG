using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyAttack2D : MonoBehaviour
{
    #region Variables
    private EnemyCtrl m_enemy_ctrl;

    [Header("플레이어 탐지 거리")]
    [Range(4f, 8f)][SerializeField] private float m_radius;

    [Header("플레이어의 레이어")]
    [SerializeField] LayerMask m_player_layer;
    private CircleCollider2D m_collider;
    private int m_atk;
    #endregion Variables

    #region Properties
    public CircleCollider2D Collider { get => m_collider; }

    public int ATK
    {
        get => m_atk;
        set => m_atk = value;
    }
    #endregion Properties

    private void Awake()
    {
        m_enemy_ctrl = GetComponent<EnemyCtrl>();
        m_collider = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        m_collider.enabled = true;
    }

    private void Start()
    {
        m_atk = m_enemy_ctrl.ScriptableObject.ATK;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_radius);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            // TODO: 처리 예정
        }   
    }

    #region Helper Methods
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
