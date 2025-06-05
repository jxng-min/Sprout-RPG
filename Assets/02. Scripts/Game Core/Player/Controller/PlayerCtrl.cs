using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCtrl : MonoBehaviour
{
    #region Variables
    [Header("플레이어의 애니메이터")]
    [SerializeField] private Animator m_animator;

    [Space(30)]
    [Header("플레이어 이동 컴포넌트")]
    [SerializeField] private Movement2D m_movement;

    [Header("플레이어 공격 컴포넌트")]
    [SerializeField] private Attack2D m_attacking;

    [Header("플레이어 체력/마나 컴포넌트")]
    [SerializeField] private Health2D m_health;

    [Space(30)]
    [Header("마우스 트래커")]
    [SerializeField] private MouseTracking m_mouse;

    [Header("장비 매니저")]
    [SerializeField] private Equipment m_equipment;

    private PlayerStateContext m_state_context;

    private IState<PlayerCtrl> m_idle_state;
    private IState<PlayerCtrl> m_move_state;
    private IState<PlayerCtrl> m_attack_state;
    private IState<PlayerCtrl> m_damage_state;
    private IState<PlayerCtrl> m_dead_state;

    private Action m_level_event;
    private Action m_hp_event;
    private Action m_mp_event;
    #endregion Variables

    #region Properties
    public Animator Animator { get => m_animator; }
    public Movement2D Movement { get => m_movement; }
    public Attack2D Attacking { get => m_attacking; }
    public Health2D Health { get => m_health; }
    public MouseTracking Mouse { get => m_mouse; }
    public Equipment Equipment { get => m_equipment; }

    public Action LVEvent
    {
        get => m_level_event;
        set => m_level_event = value;
    }

    public Action HPEvent
    {
        get => m_hp_event;
        set => m_hp_event = value;
    }

    public Action MPEvent
    {
        get => m_mp_event;
        set => m_mp_event = value;
    }
    #endregion Properties

    private void Awake()
    {
        m_state_context = new PlayerStateContext(this);

        m_idle_state = gameObject.AddComponent<PlayerIdleState>();
        m_move_state = gameObject.AddComponent<PlayerMoveState>();
        m_attack_state = gameObject.AddComponent<PlayerAttackState>();
        m_damage_state = gameObject.AddComponent<PlayerDamageState>();
        m_dead_state = gameObject.AddComponent<PlayerDeadState>();

        ChangeState(PlayerState.IDLE);
    }

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
        GameEventBus.Subscribe(GameEventType.CHECKING, GameManager.Instance.Checking);
        GameEventBus.Subscribe(GameEventType.PAUSING, GameManager.Instance.Pausing);
        GameEventBus.Subscribe(GameEventType.DEAD, GameManager.Instance.Dead);
        GameEventBus.Subscribe(GameEventType.CLEAR, GameManager.Instance.Clear);

        GameEventBus.Publish(GameEventType.PLAYING);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEventType.PLAYING, GameManager.Instance.Playing);
        GameEventBus.Unsubscribe(GameEventType.CHECKING, GameManager.Instance.Checking);
        GameEventBus.Unsubscribe(GameEventType.PAUSING, GameManager.Instance.Pausing);
        GameEventBus.Unsubscribe(GameEventType.DEAD, GameManager.Instance.Dead);
        GameEventBus.Unsubscribe(GameEventType.CLEAR, GameManager.Instance.Clear);
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
    public void ChangeState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IDLE:
                m_state_context.Transition(m_idle_state);
                break;

            case PlayerState.MOVE:
                m_state_context.Transition(m_move_state);
                break;

            case PlayerState.ATTACK:
                m_state_context.Transition(m_attack_state);
                break;

            case PlayerState.DAMAGE:
                m_state_context.Transition(m_damage_state);
                break;

            case PlayerState.DEAD:
                m_state_context.Transition(m_dead_state);
                break;
        }
    }
    #endregion Helper Methods
}
