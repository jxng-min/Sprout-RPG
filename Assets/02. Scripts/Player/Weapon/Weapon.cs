using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scriptable Object/Create Weapon")]
public class Weapon : ScriptableObject
{
    [Header("무기의 고유한 ID")]
    [SerializeField] private int m_id;
    public int ID { get => m_id; }

    [Header("무기의 이름")]
    [SerializeField] private string m_name;
    public string Name { get => m_name; }

    [Header("무기의 공격력")]
    [SerializeField] private int m_atk;
    public int ATK { get => m_atk; }

    [Header("무기의 성장 공격력")]
    [SerializeField] private int m_growth_atk;
    public int GrowthATK { get => m_growth_atk; }

    [Header("무기의 재사용 대기시간")]
    [SerializeField] private float m_cooltime;
    public float Cooltime { get => m_cooltime; }
}
