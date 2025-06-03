using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable Object/Create new enemy")]
public class Enemy : ScriptableObject
{
    [Header("몬스터의 고유한 ID")]
    [SerializeField] private EnemyCode m_id;
    public EnemyCode ID { get => m_id; }

    [Header("몬스터의 오브젝트 타입")]
    [SerializeField] private ObjectType m_object_type;
    public ObjectType Type { get => m_object_type; }

    [Header("몬스터의 체력")]
    [SerializeField] private float m_hp;
    public float HP { get => m_hp; }

    [Header("몬스터의 공격력")]
    [SerializeField] private int m_atk;
    public int ATK { get => m_atk; }

    [Header("몬스터의 이동 속도")]
    [SerializeField] private float m_spd;
    public float SPD { get => m_spd; }

    [Header("몬스터 처치 시 획득 경험치")]
    [SerializeField] private float m_exp;
    public float EXP { get => m_exp; }
}
