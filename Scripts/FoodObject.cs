using UnityEngine;

public class FoodObject : CellObject
{
    public int AmountGranted = 10; // Yiyecek miktar�

    public override void PlayerEntered()
    {
        Debug.Log("Food Collected!");
        GameManager.Instance.ChangeFood(AmountGranted); // Yemek miktar�n� art�r
        Destroy(gameObject); // Yiyecek yok edilir
    }
}