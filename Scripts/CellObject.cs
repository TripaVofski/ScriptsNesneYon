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
        return true; // Varsay�lan olarak ge�ilebilir.
    }

    public virtual void PlayerEntered()
    {
        // Bo� b�rak - Alt s�n�flar taraf�ndan ge�ersiz k�l�n�r.
    }
}

