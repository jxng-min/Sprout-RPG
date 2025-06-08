using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NPC : MonoBehaviour
{
    #region Variables
    private Animator m_animator;

    [Header("NPC가 기본적으로 바라보는 방향")]
    [SerializeField] private Vector2 m_origin_direction;

    [Header("대화 UI 컴포넌트")]
    [SerializeField] protected Dialoguer m_dialoguer;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        SetDirection(m_origin_direction);
    }

    protected void SetDirection(Vector2 direction)
    {
        m_animator.SetFloat("DirX", direction.x);
        m_animator.SetFloat("DirY", direction.y);
    }

    public virtual void Interaction()
    {
        Rotation();
    }

    protected void Rotation()
    {
        Vector2 target_direction = ((Vector2)GameManager.Instance.Player.transform.position - (Vector2)transform.position).normalized;

        SetDirection(target_direction);
    }
}
