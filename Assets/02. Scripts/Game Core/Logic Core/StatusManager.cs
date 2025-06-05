using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    #region Variables
    [Header("플레이어 컨트롤러")]
    [SerializeField] private PlayerCtrl m_player_ctrl;

    [Space(30)]
    [Header("플레이어 레벨 라벨")]
    [SerializeField] private TMP_Text m_level_label;

    [Header("플레이어 경험치 슬라이더")]
    [SerializeField] private Slider m_exp_slider;

    [Header("플레이어 체력 슬라이더")]
    [SerializeField] private Slider m_hp_slider;

    [Header("플레이어 마나 슬라이더")]
    [SerializeField] private Slider m_mp_slider;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        Subscribe();
        UpdateLV();
    }

    private void Subscribe()
    {
        m_player_ctrl.HPEvent += UpdateHP;
        m_player_ctrl.MPEvent += UpdateMP;
        m_player_ctrl.LVEvent += UpdateLV;
    }

    private void UpdateLV()
    {
        m_level_label.text = $"LV.{DataManager.Instance.Data.LV}";

        m_exp_slider.value = 0.3f;
    }

    private void UpdateHP()
    {
        m_hp_slider.value = m_player_ctrl.Health.HP / m_player_ctrl.Health.MaxHP;
    }

    private void UpdateMP()
    {
        m_mp_slider.value = m_player_ctrl.Health.MP / m_player_ctrl.Health.MaxMP;
    }
    #endregion Variables
}
