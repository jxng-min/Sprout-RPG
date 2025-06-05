using System.Collections;
using UnityEngine;

public class Health2D : MonoBehaviour
{
    #region Variables
    private PlayerCtrl m_player_ctrl;

    private float m_max_hp;
    private float m_max_mp;
    private float m_current_hp;
    private float m_current_mp;


    private Coroutine m_stagger_coroutine;
    private bool m_is_stagger;
    private bool m_is_dead;
    #endregion Variables

    #region Properties
    public float MaxHP { get => m_max_hp; }
    public float MaxMP { get => m_max_mp; }
    public float HP { get => m_current_hp; }
    public float MP { get => m_current_mp; }
    public bool IsStagger { get => m_is_stagger; }
    #endregion Properties

    private void Awake()
    {
        m_player_ctrl = GetComponent<PlayerCtrl>();
    }

    private void Start()
    {
        Initialize();
    }

    #region Helper Methods
    private void Initialize()
    {
        UpdateMaxHPMP();
        m_current_hp = DataManager.Instance.Data.Status.HP;
        m_current_mp = DataManager.Instance.Data.Status.MP;

        m_player_ctrl.HPEvent();
        m_player_ctrl.MPEvent();
    }

    public void UpdateMaxHPMP()
    {
        m_max_hp = DataManager.Instance.Data.Status.MaxHP + m_player_ctrl.Equipment.Effect.HP;
        m_max_mp = DataManager.Instance.Data.Status.MaxMP + m_player_ctrl.Equipment.Effect.MP;

        m_player_ctrl.HPEvent();
        m_player_ctrl.MPEvent();
    }

    public void UpdateHP(float amount)
    {
        if (m_is_dead)
        {
            return;
        }

        m_current_hp += amount;
        m_player_ctrl.HPEvent();

        if (amount < 0f)
        {
            if (m_current_hp <= 0f)
            {
                m_player_ctrl.ChangeState(PlayerState.DEAD);
            }
            else
            {
                m_player_ctrl.ChangeState(PlayerState.DAMAGE);
            }
        }
    }

    public void UpdateMP(float amount)
    {
        m_current_mp += amount;
        m_player_ctrl.MPEvent();
    }

    public void Hurt()
    {
        m_is_stagger = true;

        m_player_ctrl.Animator.SetTrigger("Hurt");

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

        m_player_ctrl.Animator.SetTrigger("Death");

        GameEventBus.Publish(GameEventType.DEAD);
    }
    #endregion Helper Methods
}
