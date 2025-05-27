using UnityEngine;

public class FieldMoney : MonoBehaviour
{
    #region Variables
    private int m_amount;
    #endregion Variables

    #region Properties
    public int Amount
    {
        get => m_amount;
        set => m_amount = value;
    }
    #endregion Properties

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            FindFirstObjectByType<Inventory>().AquireMoney(m_amount);
        }
    }
}
