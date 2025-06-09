using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Scriptable Object/Create new quest")]
public class Quest : ScriptableObject
{
    private QuestState m_quest_state = QuestState.NEVER_RECEIVED;
    public QuestState QuestState
    {
        get => m_quest_state;
        set => m_quest_state = value;
    }

    [Header("퀘스트의 고유한 ID")]
    [SerializeField] private int m_quest_id;
    public int ID { get => m_quest_id; }

    [Space(30f)]
    [Header("퀘스트 목표")]
    [SerializeField] private List<KillQuest> m_kill_quest_list;
    public List<KillQuest> KillQuests { get => m_kill_quest_list; }

    [SerializeField] private List<ItemQuest> m_item_quest_list;
    public List<ItemQuest> ItemQuests { get => m_item_quest_list; }

    [Space(30f)]
    [Header("퀘스트 보상")]
    [Header("보상 코인")]
    [SerializeField] private int m_coin;
    public int Coin { get => m_coin; }

    [Header("보상 경험치")]
    [SerializeField] private int m_exp;
    public int EXP { get => m_exp; }

    [Space(30f)]
    [Header("의뢰인 위치")]
    [SerializeField] private Vector3 m_source_position;
    public Vector3 Source { get => m_source_position; }

    [Header("목적지 위치")]
    [SerializeField] private Vector3 m_destination_position;
    public Vector3 Destination { get => m_destination_position; }

    private SubQuest[] m_all_subquests;
    public SubQuest[] AllSubquests
    {
        get => m_all_subquests;
        set => m_all_subquests = value;
    }
}
