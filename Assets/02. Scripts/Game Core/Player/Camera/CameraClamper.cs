using System.Collections;
using UnityEngine;

public class CameraClamper : MonoBehaviour
{
    #region Variables
    [Header("고정 타겟 카메라")]
    [SerializeField] private Transform m_camera;

    [Header("카메라가 고정될 위치")]
    [SerializeField] private Transform m_clamped_transform;

    [Header("다음 스폰 위치")]
    [SerializeField] private Transform m_spawn_transform;
    #endregion Variables

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Warp();
        }
    }

    private void Warp()
    {
        GameObject.Find("Player").transform.position = m_spawn_transform.position;

        StopCoroutine("TranslateCamera");
        StartCoroutine("TranslateCamera");
    }

    private IEnumerator TranslateCamera()
    {
        float elapsed_time = 0f;
        float target_time = 1f;

        while (elapsed_time <= target_time)
        {
            elapsed_time += Time.deltaTime;

            float delta = elapsed_time / target_time;

            float delta_x = Mathf.Lerp(m_camera.position.x, m_clamped_transform.position.x, delta);
            float delta_y = Mathf.Lerp(m_camera.position.y, m_clamped_transform.position.y, delta);

            m_camera.transform.position = new Vector3(delta_x, delta_y, m_camera.transform.position.z);
            
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Portal.png", true);

        Gizmos.DrawIcon(m_clamped_transform.position, "Camera.png", true);

        Gizmos.DrawIcon(m_spawn_transform.position, "Portal.png", true);
    }
}
