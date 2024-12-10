using UnityEngine;

public class WallObject : CellObject
{
    public int MaxHealth = 3; // Duvar�n ba�lang�� can�
    private int currentHealth;

    public override void Init(Vector2Int cell)
    {
        base.Init(cell);
        currentHealth = MaxHealth; // Duvar can�n� ba�lat
    }

    public override bool PlayerWantsToEnter()
    {
        currentHealth--; // Duvar�n can�n� 1 azalt
        GameManager.Instance.ChangeFood(-1); // Food'u 1 azalt

        if (currentHealth <= 0)
        {
            // Duvar y�k�l�rken oyuncu ge�ebilir hale gelir

            Destroy(gameObject); // Duvar nesnesini yok et
            return true; // Oyuncu bu h�creye ge�ebilir
        }

        Debug.Log($"Wall damaged! Remaining health: {currentHealth}");
        return false; // Oyuncu ge�emez ��nk� duvar hala sa�lam
    }
}
