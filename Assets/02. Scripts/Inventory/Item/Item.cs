using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/Create new item")]
public class Item : ScriptableObject
{
    [Header("아이템의 고유한 ID")]
    [SerializeField] private int m_id;
    public int ID { get => m_id; }

    [Header("아이템의 이름")]
    [SerializeField] private string m_name;
    public string Name { get => m_name; }

    [Header("아이템의 타입")]
    [SerializeField] private ItemType m_type;
    public ItemType Type { get => m_type; }

    [Header("아이템 중첩 여부")]
    [SerializeField] private bool m_stackable;
    public bool Stackable { get => m_stackable; }

    [Header("아이템의 쿨타임")]
    [SerializeField] private float m_cooltime = -1f;
    public float Cooltime { get => m_cooltime; }

    [Header("아이템의 스프라이트")]
    [SerializeField] private Sprite m_sprite;
    public Sprite Sprite { get => m_sprite; }
}
