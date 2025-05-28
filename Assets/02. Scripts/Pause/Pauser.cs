using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Pauser : MonoBehaviour
{
    #region Variables
    private static bool m_is_active = false;

    private Animator m_animator;
    #endregion Variables

    #region Properties
    public static bool IsActive { get => m_is_active; }
    #endregion Properties

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ToggleUI();
    }

    #region Helper Methods
    private void ToggleUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_is_active)
            {
                CloseUI();
            }
            else
            {
                OpenUI();
            }
        }
    }

    private void OpenUI()
    {
        m_animator.SetBool("Open", true);

        m_is_active = true;
    }

    private void CloseUI()
    {
        m_animator.SetBool("Open", false);

        m_is_active = false;
    } 
    #endregion Helper Methods
}
