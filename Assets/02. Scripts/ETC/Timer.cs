using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region Variables
    [Header("타이머 라벨")]
    [SerializeField] private TMP_Text m_timer_label;

    private float m_timer;
    #endregion Variables

    private void Awake()
    {
        Load();
    }

    private void Update()
    {
        if (GameManager.Instance.Current != GameEventType.PLAYING)
        {
            return;
        }

        m_timer += Time.deltaTime;
        SetUI();
    }

    #region Helper Methods
    private void Load()
    {
        m_timer = DataManager.Instance.PlayerData.Data.PlayTime;
    }

    public void Save()
    {
        DataManager.Instance.PlayerData.Data.PlayTime = m_timer;
    }

    private (int, int, int) Calculate()
    {
        int hr = (int)m_timer / 3600;
        m_timer %= 3600;

        int min = (int)m_timer / 60;
        int sec = (int)m_timer % 60;

        return (hr, min, sec);
    }

    private void SetUI()
    {
        var sperate_time = Calculate();
        m_timer_label.text = $"{sperate_time.Item1.ToString("00")}:{sperate_time.Item2.ToString("00")}:{sperate_time.Item3.ToString("00")}";
    }
    #endregion Helper Methods
}
