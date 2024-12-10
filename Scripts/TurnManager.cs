using UnityEngine;
using UnityEngine.Tilemaps;
public class TurnManager
{
    private int m_TurnCount; // Tur sayac�

    // Tur ger�ekle�ti�inde tetiklenecek olay
    public event System.Action OnTick;

    public TurnManager()
    {

        m_TurnCount = 1;
    }

    // Bir tur ilerlet
    public void Tick()
    {
        m_TurnCount += 1;
        Debug.Log("Current turn count: " + m_TurnCount);

        // OnTick olay�n� tetikle (null de�ilse)
        OnTick?.Invoke();
    }
}
