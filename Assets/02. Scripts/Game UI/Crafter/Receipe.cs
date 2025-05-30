using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReceipeItem
{
    public Item Item;
    public int Count;
}

[CreateAssetMenu(fileName = "New Receipe", menuName = "Scriptable Object/Create new receipe")]
public class Receipe : ScriptableObject
{
    [Header("재료 아이템의 목록")]
    [SerializeField] private List<ReceipeItem> m_ingredient_item;
    public List<ReceipeItem> Ingredient { get => m_ingredient_item; }

    [Header("제작 아이템")]
    [SerializeField] private ReceipeItem m_target_item;
    public ReceipeItem Target { get => m_target_item; }

    [Header("제작에 필요한 시간")]
    [SerializeField] private float m_time;
    public float Time { get => m_time; }
}
