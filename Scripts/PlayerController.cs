using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; } // Singleton Instance

    private BoardManager m_Board;
    private Vector2Int m_CurrentPosition;
    private bool m_IsGameOver;
   
    private void Awake()
    {
        // Singleton kontrolü
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Init(BoardManager board)
    {
        m_Board = board;
        m_IsGameOver = false;
    }

    // Oyuncuyu belirtilen pozisyona yerleþtir
    public void Spawn(BoardManager board, Vector2Int startPos)
    {
        m_Board = board;
        m_CurrentPosition = startPos;
        transform.position = m_Board.CellToWorld(startPos);
        m_IsGameOver = false;
    }

    private void Update()
    {
        if (m_IsGameOver)
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                GameManager.Instance.StartNewGame();
            }
            return;
        }

        Vector2Int input = Vector2Int.zero;
        if (Keyboard.current.wKey.wasPressedThisFrame) input = Vector2Int.up;
        else if (Keyboard.current.sKey.wasPressedThisFrame) input = Vector2Int.down;
        else if (Keyboard.current.aKey.wasPressedThisFrame) input = Vector2Int.left;
        else if (Keyboard.current.dKey.wasPressedThisFrame) input = Vector2Int.right;

        if (input != Vector2Int.zero) TryMove(m_CurrentPosition + input);
    }

    private void TryMove(Vector2Int newCellTarget)
    {
        BoardManager.CellData cellData = m_Board.GetCellData(newCellTarget);

        if (cellData == null) return;

        if (cellData.Passable)
        {
            if (cellData.ContainedObject == null)
            {
                MoveTo(newCellTarget);
                GameManager.Instance.ChangeFood(-1); // Food'u 1 azalt
            }
            else if (cellData.ContainedObject.PlayerWantsToEnter())
            {
                MoveTo(newCellTarget);
                cellData.ContainedObject.PlayerEntered();
                GameManager.Instance.ChangeFood(-1); // Food'u 1 azalt
            }
        }
    }

    private void MoveTo(Vector2Int newPosition)
    {
        m_CurrentPosition = newPosition;
        transform.position = m_Board.CellToWorld(newPosition);
    }

    public void GameOver()
    {
        m_IsGameOver = true;
        Debug.Log("Game Over!");
    }
}






