using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoaderSlot : MonoBehaviour
{
    #region Variables
    [Header("슬롯의 버튼")]
    [SerializeField] private Button m_slot_button;

    [Header("슬롯의 상태를 나타내는 오브젝트")]
    [SerializeField] private GameObject m_info_object;

    [Header("플레이어 레벨 라벨")]
    [SerializeField] private TMP_Text m_level_label;

    [Header("플레이어 플레이 타임 라벨")]
    [SerializeField] private TMP_Text m_play_time_label;

    [Header("비활성화 라벨")]
    [SerializeField] private GameObject m_deactive_label;

    private string m_player_data_path;
    private PlayerData m_player_data;
    #endregion Variables

    #region Helper Methods
    public void Add(PlayerData player_data)
    {
        m_player_data = player_data;
        
        m_info_object.SetActive(true);
        m_level_label.text = $"레벨: {m_player_data.LV}";

        var times = SeperateTime(m_player_data.PlayTime);
        m_play_time_label.text = $"플레이 타임: {times.Item1.ToString("00")}:{times.Item2.ToString("00")}:{times.Item3.ToString("00")}";

        m_deactive_label.SetActive(false);

        m_slot_button.interactable = true;
    }

    private (int, int, int) SeperateTime(float time)
    {
        int hour = (int)time / 3600;
        time %= 3600;

        int min = (int)time / 60;
        int sec = (int)time % 60;

        return (hour, min, sec);
    }

    public void Clear(bool non_blocking, int index = -1)
    {
        if (index != -1)
        {
            m_player_data_path = Path.Combine(Application.persistentDataPath, "Save", $"SaveData{index}.json");
        }

        m_level_label.text = "";
        m_play_time_label.text = "";
        m_info_object.SetActive(false);

        m_deactive_label.SetActive(true);

        m_slot_button.interactable = non_blocking;
    }

    public void Button_Save()
    {
        DataManager.Instance.Save();
        m_player_data = DataManager.Instance.Data;

        var json_data = JsonUtility.ToJson(m_player_data, true);
        File.WriteAllText(m_player_data_path, json_data);

        Add(m_player_data);
    }

    public void Button_Load()
    {
        var json_data = File.ReadAllText(m_player_data_path);

        m_player_data = JsonUtility.FromJson<PlayerData>(json_data);
        DataManager.Instance.Load(m_player_data);

        LoadingManager.Instance.LoadScene("Game");
    }
    #endregion Helper Methods
}