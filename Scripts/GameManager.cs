using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public BoardManager BoardManager;
    public PlayerController PlayerController;
    public TurnManager TurnManager;
    private VisualElement m_GameOverPanel;
    private Label m_GameOverMessage;

    private int m_CurrentLevel = 1;
    private int m_FoodAmount = 30;

    public UIDocument UIDoc;
    private Label m_FoodLabel;

    public void StartNewGame()
    {
        m_GameOverPanel.style.visibility = Visibility.Hidden;

        m_CurrentLevel = 1;
        m_FoodAmount = 20;
        m_FoodLabel.text = "Food : " + m_FoodAmount;

        
        BoardManager.Init();

        PlayerController.Init(BoardManager); // Init �a�r�s� burada
        PlayerController.Spawn(BoardManager, new Vector2Int(1, 1));
    }


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {

        TurnManager = new TurnManager();
        TurnManager.OnTick += OnTurnHappen;

        m_FoodLabel = UIDoc.rootVisualElement.Q<Label>("FoodLabel");
        m_GameOverPanel = UIDoc.rootVisualElement.Q<VisualElement>("GameOverPanel");
        m_GameOverMessage = m_GameOverPanel.Q<Label>("GameOverMessage");

        StartNewGame();

    }

    public void NewLevel()
    {
        
        BoardManager.Init();  // Yeni tahtay� olu�tur
        PlayerController.Spawn(BoardManager, new Vector2Int(1, 1)); // Oyuncuyu ba�lat

        m_CurrentLevel++;
    }

    void OnTurnHappen()
    {
        ChangeFood(-1);
    }

    public void ChangeFood(int amount)
    {
        m_FoodAmount += amount;
        m_FoodLabel.text = "Food : " + m_FoodAmount;

        // E�er yemek miktar� 0 veya alt�na d��erse
        if (m_FoodAmount <= 0)
        {
            m_FoodAmount = 0; // Yemek miktar�n� negatif olmamas� i�in s�f�rla
            m_FoodLabel.text = "Food : " + m_FoodAmount;
            PlayerController.Instance.GameOver(); // Oyunu bitir
            Debug.Log("Game Over! No food left.");
        }
    }

}

