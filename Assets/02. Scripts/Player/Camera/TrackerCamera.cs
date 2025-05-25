using UnityEngine;

public class TrackerCamera : MonoBehaviour
{
    #region Variables
    [Header("추적할 대상")]
    [SerializeField] private Transform m_target;
    #endregion Variables

    private void LateUpdate()
    {
        Tracking();
    }

    private void Tracking()
    {
        float delta_x = Mathf.Lerp(transform.position.x, m_target.position.x, Time.deltaTime * 5f);
        float delta_y = Mathf.Lerp(transform.position.y, m_target.position.y, Time.deltaTime * 10f);

        transform.position = new Vector3(delta_x, delta_y, transform.position.z);
    }
}
