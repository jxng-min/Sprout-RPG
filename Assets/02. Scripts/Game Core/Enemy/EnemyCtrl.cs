using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyCtrl : MonoBehaviour
{
    #region Variables
    private Animator m_animator;
    private EnemyMovement2D m_movement;
    private EnemyAttack2D m_attacking;
    private EnemyHealth2D m_health;
    private Pathfinder m_path_finder;

    private EnemyStateContext m_state_context;

    private IState<EnemyCtrl> m_idle_state;
    private IState<EnemyCtrl> m_move_state;
    private IState<EnemyCtrl> m_trace_state;
    private IState<EnemyCtrl> m_attack_state;
    private IState<EnemyCtrl> m_damage_state;
    private IState<EnemyCtrl> m_dead_state;

    [Header("몬스터 스크립터블 오브젝트")]
    [SerializeField] Enemy enemy;
    #endregion Variables

    #region Properties
    public Animator Animator { get => m_animator; }
    public EnemyMovement2D Movement { get => m_movement; }
    public EnemyAttack2D Attacking { get => m_attacking; }
    public EnemyHealth2D Health { get => m_health; }
    public Pathfinder Pathfinder { get => m_path_finder; }
    #endregion Properties

    private void Awake()
    {
        m_state_context = new EnemyStateContext(this);

        m_idle_state = gameObject.AddComponent<EnemyIdleState>();
        m_move_state = gameObject.AddComponent<EnemyMoveState>();
        m_trace_state = gameObject.AddComponent<EnemyTraceState>();
        // m_attack_state = gameObject.AddComponent<EnemyAttackState>();
        // m_damage_state = gameObject.AddComponent<EnemyDamageState>();
        // m_dead_state = gameObject.AddComponent<EnemyDeadState>();

        Initialize(enemy);
    }

    private void Update()
    {
        if (GameManager.Instance.Current != GameEventType.PLAYING)
        {
            return;
        }

        m_state_context.ExecuteUpdate();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.Current != GameEventType.PLAYING)
        {
            return;
        }

        m_state_context.FixedExecuteUpdate();
    }

    #region Helper Methods
    public void Initialize(Enemy enemy)
    {
        m_path_finder = GetComponent<Pathfinder>();

        m_movement = GetComponent<EnemyMovement2D>();
        m_movement.Initialize(enemy);

        m_attacking = GetComponent<EnemyAttack2D>();
        m_attacking.Initialize(enemy);

        m_health = GetComponent<EnemyHealth2D>();
        m_health.Initialize(enemy);

        m_animator = GetComponent<Animator>();
        m_animator.runtimeAnimatorController = enemy.Animator;

        ChangeState(EnemyState.IDLE);
    }

    public void ChangeState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.IDLE:
                m_state_context.Transition(m_idle_state);
                break;

            case EnemyState.MOVE:
                m_state_context.Transition(m_move_state);
                break;

            case EnemyState.TRACE:
                m_state_context.Transition(m_trace_state);
                break;

            case EnemyState.ATTACK:
                m_state_context.Transition(m_attack_state);
                break;

            case EnemyState.DAMAGE:
                m_state_context.Transition(m_damage_state);
                break;

            case EnemyState.DEAD:
                m_state_context.Transition(m_dead_state);
                break;
        }
    }
    #endregion Helper Methods
}
