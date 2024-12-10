using UnityEngine;

public class CellObject : MonoBehaviour
{
    protected Vector2Int m_Cell;

    public virtual void Init(Vector2Int cell)
    {
        m_Cell = cell;
    }

    public virtual bool PlayerWantsToEnter()
    {
        return true; // Varsayýlan olarak geçilebilir.
    }

    public virtual void PlayerEntered()
    {
        // Boþ býrak - Alt sýnýflar tarafýndan geçersiz kýlýnýr.
    }
}

