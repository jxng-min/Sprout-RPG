using System.Collections;
using UnityEngine;

public class EnemyHealth2D : MonoBehaviour
{
    #region Variables
    [Header("몬스터 컨트롤러")]
    [SerializeField] private EnemyCtrl m_enemy_ctrl;

    private SpriteRenderer m_renderer;

    private float m_hp;

    private bool m_is_stagger;
    private Coroutine m_stagger_coroutine;
    private bool m_is_dead;
    #endregion Variables

    #region Properties
    public float HP
    {
        get => m_hp;
        set => m_hp = value;
    }

    public bool IsStaggered { get => m_is_stagger; }
    #endregion Properties

    #region Helper Methods
    public void Initialize(Enemy enemy)
    {
        m_enemy_ctrl = GetComponent<EnemyCtrl>();

        m_renderer = GetComponent<SpriteRenderer>();
        m_renderer.sortingOrder = 8;

        m_hp = enemy.HP;
        m_is_dead = false;
    }
    public void UpdateHP(float amount)
    {
        if (m_is_dead)
        {
            return;
        }

        m_hp += amount;

        if (amount < 0f)
        {
            m_enemy_ctrl.Animator.SetTrigger("Hurt");
            m_enemy_ctrl.ChangeState(EnemyState.DAMAGE);
        }

        if (m_hp <= 0f)
        {
            m_enemy_ctrl.ChangeState(EnemyState.DEAD);
        }
    }

    public void Hurt()
    {
        m_is_stagger = true;

        if (m_stagger_coroutine != null)
        {
            StopCoroutine(m_stagger_coroutine);
            m_stagger_coroutine = null;
        }

        m_stagger_coroutine = StartCoroutine(Co_Hurt());
    }

    private IEnumerator Co_Hurt()
    {
        float elapsed_time = 0f;
        float target_time = 0.1f;

        while (elapsed_time < target_time)
        {
            yield return new WaitUntil(() => GameManager.Instance.Current == GameEventType.PLAYING);

            elapsed_time += Time.deltaTime;
            yield return null;
        }

        m_is_stagger = false;
        m_stagger_coroutine = null;
    }

    public void Death()
    {
        if (m_is_dead)
        {
            return;
        }

        m_is_dead = true;

        m_enemy_ctrl.Movement.Rigidbody.linearVelocity = Vector2.zero;
        m_enemy_ctrl.Movement.Rigidbody.simulated = false;

        m_enemy_ctrl.Animator.SetTrigger("Death");
        m_renderer.sortingOrder = 7;

        m_enemy_ctrl.Attacking.Collider.enabled = false;

        Invoke(nameof(Return), 1f);
    }

    private void Return()
    {
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.ENEMY);
    }
    #endregion Helper Methods
}
