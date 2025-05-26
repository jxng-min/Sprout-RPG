using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    #region Variables
    [Header("대검 인터페이스의 애니메이터")]
    [SerializeField] private Animator m_animator;

    [Header("대검의 스크립터블 오브젝트")]
    [SerializeField] private Weapon m_scriptable_object;

    private int m_atk;
    private float m_current_cooltime;
    private float m_original_cooltime;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Cooling();
    }

    public void Initialize()
    {
        m_atk = m_scriptable_object.ATK; // TODO: 업그레이드 정보도 추가해야 함.

        m_original_cooltime = m_scriptable_object.Cooltime;
        m_current_cooltime = m_original_cooltime;
    }

    public void Use()
    {
        if (!IsReady())
        {
            return;
        }
        
        m_current_cooltime = 0f;

        m_animator.SetTrigger("Attack");
    }

    public bool IsReady()
    {
        return m_current_cooltime >= m_original_cooltime;
    }

    private void Cooling()
    {
        if (m_current_cooltime >= m_original_cooltime)
        {
            m_current_cooltime = m_original_cooltime;
        }
        else
        {
            m_current_cooltime += Time.deltaTime;
        }
    }
}
