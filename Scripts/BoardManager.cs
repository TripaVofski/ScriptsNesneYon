using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    public int Width = 10, Height = 10;
    public Tile[] GroundTiles, WallTiles;
    public GameObject FoodPrefab; // Yemek prefab�
    public WallObject WallPrefab; // Duvar prefab�
    public ExitCellObject ExitCellPrefab; // ��k�� h�cresi prefab�
    public WallObject Enemy;
    private Tilemap m_Tilemap;
    private CellData[,] m_BoardData;
    private List<Vector2Int> m_EmptyCellsList;

    public GameObject EnemyPrefab; // Inspector'dan atayaca��n�z Enemy prefab�

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
        public bool Passable; // H�cre ge�ilebilir mi?
        public CellObject ContainedObject; // H�credeki nesne
    }

    public void Init()
    {
        m_Tilemap = GetComponentInChildren<Tilemap>();
        Debug.Log(m_Tilemap != null ? "Tilemap bulundu" : "Tilemap bulunamad�!");

        if (m_Tilemap == null)
        {
            Debug.LogError("Tilemap referans� bulunamad�! BoardManager'a ba�l� bir Tilemap var m� kontrol edin.");
            return;
        }

        m_BoardData = new CellData[Width, Height];
        m_EmptyCellsList = new List<Vector2Int>();

        // Haritay� olu�tur
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

        Debug.Log("BoardManager Init tamamland�.");

        m_BoardData = new CellData[Width, Height];
        m_EmptyCellsList = new List<Vector2Int>();

        // Harita olu�turma d�ng�s�
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

        // Oyuncunun ba�lang�� konumunu bo� h�cre listesinden ��kar
        m_EmptyCellsList.Remove(new Vector2Int(1, 1));

        // ��k�� h�cresini olu�tur
        AddExitCell();

        // Yiyecek ve duvarlar� yerle�tir
        GenerateWall();
        GenerateFood();

        Debug.Log("BoardManager Init() ba�ar�yla tamamland�.");
    }

    public CellData GetCellData(Vector2Int coord)
    {
        if (coord.x >= 0 && coord.x < Width && coord.y >= 0 && coord.y < Height)
        {
            return m_BoardData[coord.x, coord.y];
        }
        return null; // Ge�ersiz koordinatlar i�in null d�ner
    }

    private void AddExitCell()
    {
        Vector2Int endCoord = new Vector2Int(Width - 2, Height - 2);

        ExitCellObject exitCell = Instantiate(ExitCellPrefab);
        exitCell.Init(endCoord);
        exitCell.transform.position = CellToWorld(endCoord);

        m_BoardData[endCoord.x, endCoord.y].ContainedObject = exitCell;
        m_EmptyCellsList.Remove(endCoord);

        Debug.Log($"ExitCell {endCoord} koordinat�na eklendi.");
    }

    public void GenerateWall()
    {
        int wallCount = Random.Range(6, 10);
        Debug.Log($"Duvar say�s�: {wallCount}");

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
        Debug.Log($"Yemek say�s�: {foodCount}");

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







