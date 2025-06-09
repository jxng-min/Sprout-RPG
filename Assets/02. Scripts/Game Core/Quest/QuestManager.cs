using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    private Inventory m_inventory;

    [Header("퀘스트들의 목록")]
    [SerializeField] private List<Quest> m_quest_list;

    private Dictionary<int, Quest> m_quest_dict;
    private List<Quest> m_received_quest_list;

    public override void Awake()
    {
        base.Awake();

        m_quest_dict = new();
        m_received_quest_list = new();

        Initialize();

        LoadQuestList();
    }

    public void Initialize()
    {
        m_inventory = FindFirstObjectByType<Inventory>();
        m_received_quest_list.Clear();
    }

    private void LoadQuestList()
    {
        for (int i = 0; i < m_quest_list.Count; i++)
        {
            var new_quest = Instantiate(m_quest_list[i]);

            var all_quests = new List<SubQuest>();
            {
                foreach (var subquest in new_quest.KillQuests)
                {
                    all_quests.Add(subquest);
                }

                foreach (var subquest in new_quest.ItemQuests)
                {
                    all_quests.Add(subquest);
                }

                new_quest.AllSubquests = all_quests.ToArray();
            }

            m_quest_dict.Add(new_quest.ID, new_quest);
        }
    }

    public void ReceiveQuest(int quest_id)
    {
        if (m_quest_dict.TryGetValue(quest_id, out var new_quest))
        {
            m_received_quest_list.Add(new_quest);

            if (new_quest.QuestState == QuestState.CLEAR)
            {
                CompleteQuest(quest_id, false);
            }
            else
            {
                new_quest.QuestState = QuestState.ON_GOING;
            }
        }
    }

    public void CompleteQuest(int quest_id, bool award = true)
    {
        if (m_quest_dict.TryGetValue(quest_id, out var quest))
        {
            if (award)
            {
                DataManager.Instance.PlayerData.Data.EXP += quest.EXP;
            }

            quest.QuestState = QuestState.CLEAR;
            m_received_quest_list.Remove(quest);
        }
    }

    public QuestState CheckQuestState(int quest_id)
    {
        if (m_quest_dict.TryGetValue(quest_id, out var quest))
        {
            if (quest.QuestState == QuestState.CLEAR)
            {
                return QuestState.CLEAR;
            }

            for (int i = 0; i < m_received_quest_list.Count; i++)
            {
                if (m_received_quest_list[i].ID == quest.ID)
                {
                    foreach (var subquest in quest.AllSubquests)
                    {
                        if (!subquest.IsClear)
                        {
                            return QuestState.ON_GOING;
                        }
                    }

                    return QuestState.CAN_CLEAR;
                }
            }

            return QuestState.NEVER_RECEIVED;
        }

        throw new Exception($"딕셔너리에 저장된 퀘스트가 없습니다: {quest_id}");
    }

    public void UpdateKillCount(EnemyCode enemy_code)
    {
        for (int i = 0; i < m_received_quest_list.Count; i++)
        {
            for (int ii = 0; ii < m_received_quest_list[i].KillQuests.Count; ii++)
            {
                if (m_received_quest_list[i].KillQuests[ii].Target.m_enemy_code == enemy_code)
                {
                    ++m_received_quest_list[i].KillQuests[ii].Current;

                    m_received_quest_list[i].KillQuests[ii].IsClear
                        = m_received_quest_list[i].KillQuests[ii].Target.m_count <= m_received_quest_list[i].KillQuests[ii].Current;

                    return;
                }
            }
        }
    }

    public void UpdateItemCount()
    {
        for (int i = 0; i < m_received_quest_list.Count; i++)
        {
            for (int ii = 0; i < m_received_quest_list[i].ItemQuests.Count; ii++)
            {
                m_received_quest_list[i].ItemQuests[ii].Current = m_inventory.GetItemCount(m_received_quest_list[i].ItemQuests[ii].Target.m_item_code);

                m_received_quest_list[i].ItemQuests[ii].IsClear
                    = m_received_quest_list[i].ItemQuests[ii].Target.m_count <= m_inventory.GetItemCount(m_received_quest_list[i].ItemQuests[ii].Target.m_item_code);
            }
        }
    }
}
