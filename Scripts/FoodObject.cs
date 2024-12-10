using UnityEngine;

public class FoodObject : CellObject
{
    public int AmountGranted = 10; // Yiyecek miktarý

    public override void PlayerEntered()
    {
        Debug.Log("Food Collected!");
        GameManager.Instance.ChangeFood(AmountGranted); // Yemek miktarýný artýr
        Destroy(gameObject); // Yiyecek yok edilir
    }
}