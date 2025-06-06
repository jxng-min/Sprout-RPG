using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DamageIndicator : MonoBehaviour
{
    #region Variables
    [Header("데미지 라벨")]
    [SerializeField] private TMP_Text m_damage_label;

    private Animator m_animator;
    #endregion Variables

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        m_animator.SetTrigger("Up");
    }

    private void OnDisable()
    {
        m_animator.ResetTrigger("Up");
    }


    #region Helper Methods
    public void Initialize(float damage)
    {
        if (damage <= 0f)
        {
            m_damage_label.text = $"<color=#F05650>{NumberFormatter.FormatNumber(damage)}</color>";
        }
        else
        {
            m_damage_label.text = $"<color=#9AD756>{NumberFormatter.FormatNumber(damage)}</color>";
        }
    }

    public void Return()
    {
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.DAMAGE_INDICATOR);
    }
    #endregion Helper Methods
}
