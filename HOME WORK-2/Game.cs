using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private int Generation = 0;
    public Text GenerationText;
    private static int GridWidth = 40;
    private static int GridHeight = 40;

    public float SimulationSpeed = 0.1f;
    private float Timer = 0;
    public bool IsSimulationActive = false;
    private Cell[,] CellGrid = new Cell[GridWidth, GridHeight];

    void Start()
    {
        PlaceCells();
    }

    void Update()
    {
        if (IsSimulationActive && Timer >= SimulationSpeed)
        {
            Timer = 0f;
            CountNeighbours();
            PopulationControl();
            Generation++;
        }
        else
        {
            Timer += Time.deltaTime;
        }

        UpdateGenerationText();
    }

    void PlaceCells()
    {
        for (int y = 0; y < GridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                CellGrid[x, y] = InstantiateCell(x, y);
            }
        }

        PatternManager.Pattern3(CellGrid, 0, 0);
         PatternManager.Pattern1(CellGrid,0,0);
        PatternManager.Pattern2(CellGrid,0,0);
    }

    Cell InstantiateCell(int x, int y)
    {
        Cell cell = Instantiate(Resources.Load<Cell>("Prefabs/Cell"), new Vector2(x, y), Quaternion.identity) as Cell;
        cell.SetState(false);
        return cell;
    }

    void CountNeighbours()
    {
        for (int y = 0; y < GridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                CellGrid[x, y].numNeighbours = GetNeighbourCount(x, y);
            }
        }
    }

    int GetNeighbourCount(int x, int y)
    {
        int numNeighbours = 0;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;

                int nx = x + dx;
                int ny = y + dy;

                if (IsValidCellPosition(nx, ny) && CellGrid[nx, ny].IsAlive)
                {
                    numNeighbours++;
                }
            }
        }

        return numNeighbours;
    }

    void PopulationControl()
    {
        for (int y = 0; y < GridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                bool isAlive = CellGrid[x, y].IsAlive;
                int numNeighbours = CellGrid[x, y].numNeighbours;

                CellGrid[x, y].SetState(isAlive ? (numNeighbours == 2 || numNeighbours == 3) : numNeighbours == 3);
            }
        }
    }

    void UpdateGenerationText()
    {
        Debug.Log(" Generation:  " + Generation);

        if (GenerationText != null)
        {
            GenerationText.text = "Generation: " + Generation;
        }
    }

    bool IsValidCellPosition(int x, int y)
    {
        return x >= 0 && x < GridWidth && y >= 0 && y < GridHeight;
    }
}
