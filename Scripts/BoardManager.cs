using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    public int Width = 10, Height = 10;
    public Tile[] GroundTiles, WallTiles;
    public GameObject FoodPrefab; // Yemek prefabý
    public WallObject WallPrefab; // Duvar prefabý
    public ExitCellObject ExitCellPrefab; // Çýkýþ hücresi prefabý
    public WallObject Enemy;
    private Tilemap m_Tilemap;
    private CellData[,] m_BoardData;
    private List<Vector2Int> m_EmptyCellsList;

    public GameObject EnemyPrefab; // Inspector'dan atayacaðýnýz Enemy prefabý

    public void GenerateEnemies(int enemyCount = 3)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int randomIndex = Random.Range(0, m_EmptyCellsList.Count);
            Vector2Int coord = m_EmptyCellsList[randomIndex];
            m_EmptyCellsList.RemoveAt(randomIndex);

            GameObject newEnemy = Instantiate(EnemyPrefab);
            newEnemy.transform.position = CellToWorld(coord);
            m_BoardData[coord.x, coord.y].ContainedObject = newEnemy.GetComponent<Enemy>();
        }
    }

    public class CellData
    {
        public bool Passable; // Hücre geçilebilir mi?
        public CellObject ContainedObject; // Hücredeki nesne
    }

    public void Init()
    {
        m_Tilemap = GetComponentInChildren<Tilemap>();
        Debug.Log(m_Tilemap != null ? "Tilemap bulundu" : "Tilemap bulunamadý!");

        if (m_Tilemap == null)
        {
            Debug.LogError("Tilemap referansý bulunamadý! BoardManager'a baðlý bir Tilemap var mý kontrol edin.");
            return;
        }

        m_BoardData = new CellData[Width, Height];
        m_EmptyCellsList = new List<Vector2Int>();

        // Haritayý oluþtur
        for (int y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x)
            {
                Tile tile;
                m_BoardData[x, y] = new CellData();

                if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                {
                    tile = WallTiles[Random.Range(0, WallTiles.Length)];
                    m_BoardData[x, y].Passable = false;
                }
                else
                {
                    tile = GroundTiles[Random.Range(0, GroundTiles.Length)];
                    m_BoardData[x, y].Passable = true;
                    m_EmptyCellsList.Add(new Vector2Int(x, y));
                }

                m_Tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }

        Debug.Log("BoardManager Init tamamlandý.");

        m_BoardData = new CellData[Width, Height];
        m_EmptyCellsList = new List<Vector2Int>();

        // Harita oluþturma döngüsü
        for (int y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x)
            {
                Tile tile;
                m_BoardData[x, y] = new CellData();

                if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                {
                    tile = WallTiles[Random.Range(0, WallTiles.Length)];
                    m_BoardData[x, y].Passable = false;
                }
                else
                {
                    tile = GroundTiles[Random.Range(0, GroundTiles.Length)];
                    m_BoardData[x, y].Passable = true;
                    m_EmptyCellsList.Add(new Vector2Int(x, y));
                }

                m_Tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }

        // Oyuncunun baþlangýç konumunu boþ hücre listesinden çýkar
        m_EmptyCellsList.Remove(new Vector2Int(1, 1));

        // Çýkýþ hücresini oluþtur
        AddExitCell();

        // Yiyecek ve duvarlarý yerleþtir
        GenerateWall();
        GenerateFood();

        Debug.Log("BoardManager Init() baþarýyla tamamlandý.");
    }

    public CellData GetCellData(Vector2Int coord)
    {
        if (coord.x >= 0 && coord.x < Width && coord.y >= 0 && coord.y < Height)
        {
            return m_BoardData[coord.x, coord.y];
        }
        return null; // Geçersiz koordinatlar için null döner
    }

    private void AddExitCell()
    {
        Vector2Int endCoord = new Vector2Int(Width - 2, Height - 2);

        ExitCellObject exitCell = Instantiate(ExitCellPrefab);
        exitCell.Init(endCoord);
        exitCell.transform.position = CellToWorld(endCoord);

        m_BoardData[endCoord.x, endCoord.y].ContainedObject = exitCell;
        m_EmptyCellsList.Remove(endCoord);

        Debug.Log($"ExitCell {endCoord} koordinatýna eklendi.");
    }

    public void GenerateWall()
    {
        int wallCount = Random.Range(6, 10);
        Debug.Log($"Duvar sayýsý: {wallCount}");

        for (int i = 0; i < wallCount; i++)
        {
            if (m_EmptyCellsList.Count == 0) return;

            int randomIndex = Random.Range(0, m_EmptyCellsList.Count);
            Vector2Int coord = m_EmptyCellsList[randomIndex];
            m_EmptyCellsList.RemoveAt(randomIndex);

            WallObject newWall = Instantiate(WallPrefab);
            newWall.Init(coord);
            newWall.transform.position = CellToWorld(coord);

            m_BoardData[coord.x, coord.y].ContainedObject = newWall;
        }
    }

    public void GenerateFood()
    {
        int foodCount = 5;
        Debug.Log($"Yemek sayýsý: {foodCount}");

        for (int i = 0; i < foodCount; i++)
        {
            if (m_EmptyCellsList.Count == 0) return;

            int randomIndex = Random.Range(0, m_EmptyCellsList.Count);
            Vector2Int coord = m_EmptyCellsList[randomIndex];
            m_EmptyCellsList.RemoveAt(randomIndex);

            GameObject newFood = Instantiate(FoodPrefab);
            newFood.transform.position = CellToWorld(coord);
            m_BoardData[coord.x, coord.y].ContainedObject = newFood.GetComponent<CellObject>();
        }
    }

    public Vector3 CellToWorld(Vector2Int cell)
    {
        return m_Tilemap.GetCellCenterWorld((Vector3Int)cell);
    }

    
}







