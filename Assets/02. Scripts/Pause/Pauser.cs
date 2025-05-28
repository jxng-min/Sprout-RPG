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
        GameEventBus.Publish(GameEventType.PAUSING);

        m_animator.SetBool("Open", true);

        m_is_active = true;
    }

    private void CloseUI()
    {
        GameEventBus.Publish(GameEventType.PLAYING);

        m_animator.SetBool("Open", false);

        m_is_active = false;
    }

    public void Button_Title()
    {
        // TODO: 풀링한 게임 오브젝트들을 반환해야 할 필요가 있음
        LoadingManager.Instance.LoadScene("Title");
    }
    #endregion Helper Methods
}
