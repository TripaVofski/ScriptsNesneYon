using UnityEngine;

public class ExitCellObject : CellObject
{
    public override void PlayerEntered()
    {
        Debug.Log("Reached the Exit!");
        GameManager.Instance.NewLevel(); // Yeni seviyeye geç
    }
}

