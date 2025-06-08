using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    public NPCCode ID;
    public string Name;
}

[System.Serializable]
public class NPCDataList
{
    public NPCData[] Data;
}

public class NPCDataManager : MonoBehaviour
{
    #region Variables
    private string m_npc_data_path;

    private Dictionary<NPCCode, string> m_npc_data_dict;
    #endregion Variables

    private void Awake()
    {
        m_npc_data_dict = new();
        Load();
    }

    #region Helper Methods
    private void Load()
    {
        m_npc_data_path = Path.Combine(Application.streamingAssetsPath, "NPCData.json");

        if (File.Exists(m_npc_data_path))
        {
            var json_data = File.ReadAllText(m_npc_data_path);

            var data_list = JsonUtility.FromJson<NPCDataList>(json_data);
            if (data_list != null && data_list.Data != null)
            {
                for (int i = 0; i < data_list.Data.Length; i++)
                {
                    if (!m_npc_data_dict.TryAdd(data_list.Data[i].ID, data_list.Data[i].Name))
                    {
#if UNITY_EDITOR
                        Debug.LogWarning($"딕셔너리에 중복된 데이터가 존재합니다.    중복된 데이터의 아이디: {data_list.Data[i].ID}");
#endif
                    }
                }
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log($"{m_npc_data_path}가 존재하지 않습니다.");
#endif
        }
    }

    public string GetName(NPCCode npc_id)
    {
        return m_npc_data_dict.TryGetValue(npc_id, out var name) ? name : null;
    }
    #endregion Helper Methods
}
