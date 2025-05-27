using System.Collections.Generic;
using UnityEngine;

public class ItemCoolManager : Singleton<ItemCoolManager>
{
    #region Variables
    private Dictionary<int, float> m_cooltime_dict;
    private List<int> m_item_list;
    private float m_temp_cooltime;
    #endregion Variables

    private new void Awake()
    {
        base.Awake();

        m_cooltime_dict = new();
        m_item_list = new();
    }

    private void Update()
    {
        for (int i = m_item_list.Count - 1; i >= 0; i--)
        {
            m_temp_cooltime = m_cooltime_dict[m_item_list[i]] -= Time.deltaTime;

            if (m_temp_cooltime < 0f)
            {
                m_item_list.RemoveAt(i);
            }
        }
    }

    #region Helper Methods
    public void Enqueue(int item_id, float origin_cooltime)
    {
        m_cooltime_dict.TryAdd(item_id, origin_cooltime);
        m_cooltime_dict[item_id] = origin_cooltime;

        m_item_list.Add(item_id);
    }

    public float GetTime(int item_id)
    {
        if (m_cooltime_dict.TryGetValue(item_id, out float cooltime))
        {
            return cooltime;
        }
        else
        {
            return 0f;
        }
    }
    #endregion Helper Methods
}
