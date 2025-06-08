using UnityEngine;

public class Brynlee : Craftman
{
    #region Helper Methods
    public override void Interaction()
    {
        Rotation();

        m_dialoguer.EndAction += Action;
        m_dialoguer.StartDialogue(1);
    }

    private void Action()
    {
        m_crafter.OpenUI(m_receipe_list);
    }
    #endregion Helper Methods
}
