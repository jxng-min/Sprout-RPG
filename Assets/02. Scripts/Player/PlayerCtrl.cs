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

    [Space(30)]
    [Header("마우스 트래커")]
    [SerializeField] private MouseTracking m_mouse;

    private PlayerStateContext m_state_context;

    private IState<PlayerCtrl> m_idle_state;
    private IState<PlayerCtrl> m_move_state;
    #endregion Variables

    #region Properties
    public Animator Animator { get => m_animator; }
    public Movement2D Movement { get => m_movement; }
    #endregion Properties

    private void Awake()
    {
        m_state_context = new PlayerStateContext(this);

        m_idle_state = gameObject.AddComponent<PlayerIdleState>();
        m_move_state = gameObject.AddComponent<PlayerMoveState>();

        ChangeState(PlayerState.IDLE);
    }

    private void Update()
    {
        m_state_context.ExecuteUpdate();
        
        SetAnimation();
    }

    private void FixedUpdate()
    {
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
        }
    }

    private void SetAnimation()
    {
        Vector2 normarlized_direction = (m_mouse.Position - (Vector2)transform.position).normalized;

        Animator.SetFloat("MoveX", normarlized_direction.x);
        Animator.SetFloat("MoveY", normarlized_direction.y);
    }
    #endregion Helper Methods
}
