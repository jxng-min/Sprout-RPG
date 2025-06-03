using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable Object/Create new enemy")]
public class Enemy : ScriptableObject
{
    [Header("몬스터의 고유한 ID")]
    [SerializeField] private int m_id;
    public int ID { get => m_id; }

    [Header("몬스터의 타입")]
    [SerializeField] private EnemyType m_type;
    public EnemyType Type { get => m_type; }

    [Header("몬스터의 체력")]
    [SerializeField] private float m_hp;
    public float HP { get => m_hp; }

    [Header("몬스터의 공격력")]
    [SerializeField] private int m_atk;
    public int ATK { get => m_atk; }

    [Header("몬스터의 이동 속도")]
    [SerializeField] private float m_spd;
    public float SPD { get => m_spd; }

    [Header("몬스터의 공격 대기시간")]
    [SerializeField] private float m_cooltime;
    public float Cooltime { get => m_cooltime; }

    [Header("몬스터의 애니메이트 컨트롤러")]
    [SerializeField] RuntimeAnimatorController m_animate_ctrl;
    public RuntimeAnimatorController Animator { get => m_animate_ctrl; }
}
