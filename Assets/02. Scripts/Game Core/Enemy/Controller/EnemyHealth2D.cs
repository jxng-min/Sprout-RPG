using System.Collections;
using UnityEngine;

public class EnemyHealth2D : MonoBehaviour
{
    #region Variables
    private EnemyCtrl m_enemy_ctrl;
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

    private void Awake()
    {
        m_enemy_ctrl = GetComponent<EnemyCtrl>();
        m_renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        m_enemy_ctrl.Animator.ResetTrigger("Hurt");
        m_enemy_ctrl.Animator.ResetTrigger("Death");

        m_renderer.color = new Color(m_renderer.color.r, m_renderer.color.g, m_renderer.color.b, 1f);
        m_renderer.sortingOrder = 8;
        m_hp = m_enemy_ctrl.ScriptableObject.HP;
        m_is_dead = false;
    }

    #region Helper Methods
    public void UpdateHP(float amount)
    {
        if (m_is_dead)
        {
            return;
        }

        m_hp += amount;

        if (amount < 0f)
        {
            if (m_hp <= 0f)
            {
                m_enemy_ctrl.ChangeState(EnemyState.DEAD);
            }
            else
            {
                m_enemy_ctrl.ChangeState(EnemyState.DAMAGE);
            }
        }
    }

    public void Hurt()
    {
        m_is_stagger = true;

        m_enemy_ctrl.Animator.SetTrigger("Hurt");

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
        float target_time = 1f;

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

        m_enemy_ctrl.Drop.Drop();

        GameManager.Instance.Player.Attacking.GetExp(Random.Range(m_enemy_ctrl.ScriptableObject.EXP - m_enemy_ctrl.ScriptableObject.EXP_DEV,
                                                      m_enemy_ctrl.ScriptableObject.EXP + m_enemy_ctrl.ScriptableObject.EXP_DEV));

        Invoke(nameof(Return), 0.5f);
    }

    private void Return()
    {
        FindFirstObjectByType<SpawnerManager>().Updates(m_enemy_ctrl.SpawnerID, -1);
        ObjectManager.Instance.ReturnObject(gameObject, m_enemy_ctrl.ScriptableObject.Type);
    }
    #endregion Helper Methods
}
