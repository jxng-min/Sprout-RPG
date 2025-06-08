using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Dialoguer : MonoBehaviour
{
    #region Variables
    private static bool m_is_active = false;

    [Header("대화 컨텍스트 주체의 이름 라벨")]
    [SerializeField] private TMP_Text m_name_label;

    [Header("타이핑 이펙터")]
    [SerializeField] private TypingEffector m_typing_effector;

    private Animator m_animator;

    private DialogueData m_dialogue_data;
    private int m_context_index;

    private Action m_dialogue_end_action;
    #endregion Variables

    #region Properties
    public static bool IsActive { get => m_is_active; }
    public Action EndAction
    {
        get => m_dialogue_end_action;
        set => m_dialogue_end_action = value;
    }
    #endregion Properties

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!m_is_active)
        {
            return;
        }

        if (!m_typing_effector.IsEffecting && Input.GetKeyDown(KeyCode.Space))
        {
            if (m_context_index < m_dialogue_data.Contexts.Length - 1)
            {
                m_context_index++;
                SetUI();
            }
            else
            {
                ToggleUI(false);

                GameEventBus.Publish(GameEventType.PLAYING);

                EndAction?.Invoke();
                EndAction = null;
            }
        }
    }

    #region Helper Methods
    private void ToggleUI(bool is_open)
    {
        m_is_active = is_open;
        m_animator.SetBool("Open", m_is_active);
    }

    public void StartDialogue(int dialogue_id)
    {
        GameEventBus.Publish(GameEventType.CHECKING);

        m_dialogue_data = DataManager.Instance.DialogueData.GetDialogue(dialogue_id);
        m_context_index = 0;

        ToggleUI(true);
        SetUI();
    }

    private void SetUI()
    {
        m_name_label.text = DataManager.Instance.NPCData.GetName(m_dialogue_data.Contexts[m_context_index].NPC);
        m_typing_effector.SetContext(m_dialogue_data.Contexts[m_context_index].Context);
    }
    #endregion Helper Methods
}
