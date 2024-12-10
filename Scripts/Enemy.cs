using UnityEngine;

public class Enemy : CellObject
{
    public int Damage = 10; // Oyuncunun food de�erini azaltacak hasar miktar�

    public override void PlayerEntered()
    {
        Debug.Log("Enemy encountered! Player takes damage.");
        GameManager.Instance.ChangeFood(-Damage); // Oyuncunun yiyecek miktar�n� azalt
    }
}