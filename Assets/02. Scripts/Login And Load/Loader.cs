using System.IO;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Loader : MonoBehaviour
{
    #region Variables
    [Header("로더 슬롯의 부모 트랜스폼")]
    [SerializeField] private Transform m_slot_root;
    private LoaderSlot[] m_slots;

    private Animator m_animator;

    #endregion Variables

    private void Awake()
    {
        m_slots = m_slot_root.GetComponentsInChildren<LoaderSlot>();
        m_animator = GetComponent<Animator>();
    }

    #region Helper Methods
    private void Initialize(bool non_blocking)
    {
        ClearSlots(non_blocking);
        AddSlots();
    }

    public void Button_OpenUI(bool non_blocking)
    {
        Initialize(non_blocking);
        m_animator.SetBool("Open", true);
    }

    public void Button_CloseUI()
    {
        m_animator.SetBool("Open", false);
    }

    private void ClearSlots(bool non_blocking)
    {
        for (int i = 0; i < m_slots.Length; i++)
        {
            m_slots[i].Clear(non_blocking, i);
        }
    }

    public void AddSlots()
    {
        for (int i = 0; i < m_slots.Length; i++)
        {
            string player_data_path = Path.Combine(Application.persistentDataPath, "Save", $"SaveData{i}.json");

            if (File.Exists(player_data_path))
            {
                var json_data = File.ReadAllText(player_data_path);
                var player_data = JsonUtility.FromJson<PlayerData>(json_data);

                m_slots[i].Add(player_data);
            }
        }
    }
    #endregion Helper Methods
}