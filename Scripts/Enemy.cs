using UnityEngine;

public class Enemy : CellObject
{
    public int Damage = 10; // Oyuncunun food deðerini azaltacak hasar miktarý

    public override void PlayerEntered()
    {
        Debug.Log("Enemy encountered! Player takes damage.");
        GameManager.Instance.ChangeFood(-Damage); // Oyuncunun yiyecek miktarýný azalt
    }
}