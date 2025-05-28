using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDataManager : Singleton<ItemDataManager>
{
    #region Variables
    [Header("아이템 스크립터블 오브젝트들의 목록")]
    [SerializeField] private List<Item> m_item_list;
    private string m_item_data_path;

    private Dictionary<int, string> m_item_name_dict;
    private Dictionary<int, string> m_item_description_dict;
    private Dictionary<int, Item> m_item_dict;
    #endregion Variables

    private new void Awake()
    {
        base.Awake();

        m_item_name_dict = new();
        m_item_description_dict = new();
        m_item_dict = new();

        LoadJson();
        SetItemDictionary();
    }

    #region Helper Methods
    private void LoadJson()
    {
        m_item_data_path = Path.Combine(Application.streamingAssetsPath, "ItemData.json");

        if (!File.Exists(m_item_data_path))
        {
#if UNITY_EDITOR
            Debug.LogError($"{m_item_data_path}가 존재하지 않습니다.");
#endif
        }

        var json_data = File.ReadAllText(m_item_data_path);
        var item_data = JsonUtility.FromJson<ItemDatas>(json_data);

        foreach (var data in item_data.List)
        {
            m_item_name_dict.Add(data.ID, data.Name);
            m_item_description_dict.Add(data.ID, data.Description);
        }
    }

    private void SetItemDictionary()
    {
        for (int i = 0; i < m_item_list.Count; i++)
        {
            m_item_dict.Add(m_item_list[i].ID, m_item_list[i]);
        }
    }

    public string GetName(int item_id)
    {
        return m_item_name_dict.ContainsKey(item_id) ? m_item_name_dict[item_id] : null;
    }

    public string GetDescription(int item_id)
    {
        return m_item_description_dict.ContainsKey(item_id) ? m_item_description_dict[item_id] : null;
    }

    public Item GetItem(int item_id)
    {
        return m_item_dict.ContainsKey(item_id) ? m_item_dict[item_id] : null;
    }
    #endregion Helper Methods
}
