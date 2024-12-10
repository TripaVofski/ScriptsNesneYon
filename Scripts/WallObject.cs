using UnityEngine;

public class WallObject : CellObject
{
    public int MaxHealth = 3; // Duvarýn baþlangýç caný
    private int currentHealth;

    public override void Init(Vector2Int cell)
    {
        base.Init(cell);
        currentHealth = MaxHealth; // Duvar canýný baþlat
    }

    public override bool PlayerWantsToEnter()
    {
        currentHealth--; // Duvarýn canýný 1 azalt
        GameManager.Instance.ChangeFood(-1); // Food'u 1 azalt

        if (currentHealth <= 0)
        {
            // Duvar yýkýlýrken oyuncu geçebilir hale gelir

            Destroy(gameObject); // Duvar nesnesini yok et
            return true; // Oyuncu bu hücreye geçebilir
        }

        Debug.Log($"Wall damaged! Remaining health: {currentHealth}");
        return false; // Oyuncu geçemez çünkü duvar hala saðlam
    }
}
