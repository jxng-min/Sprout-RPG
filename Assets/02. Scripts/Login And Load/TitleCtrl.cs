using UnityEngine;

public class TitleCtrl : MonoBehaviour
{
    #region Variables
    [Header("데이터 로더의 애니메이터")]
    [SerializeField] private Animator m_loader_animator;
    #endregion Variables

    #region Helper Methods
    public void Button_Start()
    {
        LoadingManager.Instance.LoadScene("Game");
    }

    public void Button_Load()
    {
        m_loader_animator.SetBool("Open", true);
    }

    public void Button_Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion Helper Methods
}
