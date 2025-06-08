using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ContextData
{
    public NPCCode NPC;
    public string Context;
}

[System.Serializable]
public class DialogueData
{
    public int ID;
    public ContextData[] Contexts;
}

[System.Serializable]
public class DialogueDataList
{
    public DialogueData[] Data;
}

public class DialogueDataManager : MonoBehaviour
{
    #region Variables
    private string m_dialogue_data_path;

    private Dictionary<int, DialogueData> m_dialogue_data_dict;
    #endregion Variables

    private void Awake()
    {
        m_dialogue_data_dict = new();
        Load();
    }

    #region Helper Methods
    private void Load()
    {
        m_dialogue_data_path = Path.Combine(Application.streamingAssetsPath, "DialogueData.json");

        if (File.Exists(m_dialogue_data_path))
        {
            var json_data = File.ReadAllText(m_dialogue_data_path);

            var data_list = JsonUtility.FromJson<DialogueDataList>(json_data);
            if (data_list != null && data_list.Data != null)
            {
                for (int i = 0; i < data_list.Data.Length; i++)
                {
                    if (!m_dialogue_data_dict.TryAdd(data_list.Data[i].ID, data_list.Data[i]))
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
            Debug.Log($"{m_dialogue_data_path}가 존재하지 않습니다.");
#endif
        }
    }

    public DialogueData GetDialogue(int dialogue_id)
    {
        return m_dialogue_data_dict.TryGetValue(dialogue_id, out var dialogue_data) ? dialogue_data : null;
    }
    #endregion Helper Methods
}
