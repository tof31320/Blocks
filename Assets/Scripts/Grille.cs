using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grille : MonoBehaviour {

    private static Grille _instance = null;
    public static Grille instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>();
            }
            return _instance;
        }
    }

    public Vector2 dimensions = Vector2.zero;

    public GameObject cellPrefab;

    public float CELL_SIZE = 0.5f;

    public List<Cell> cells = new List<Cell>();

    public void Build()
    {
        cells = new List<Cell>();

        for (int i = 0; i < dimensions.x; i++)
        {
            for(int j = 0; j < dimensions.y; j++)
            {
                Cell cell = CreateAndCell(i, j);
                cells.Add(cell);
            }
        }
    }    

    public Vector3 GetCellPosition(int i, int j)
    {
        Vector3 cellPosition = new Vector3(transform.position.x + i * CELL_SIZE,
                                           transform.position.y,
                                           transform.position.z + j * CELL_SIZE);
        return cellPosition;
    }

    public Cell CreateAndCell(int i, int j)
    {
        Vector3 cellPosition = GetCellPosition(i, j);

        GameObject g = Instantiate(cellPrefab, cellPosition, Quaternion.identity) as GameObject;
        g.transform.SetParent(transform);

        Cell cell = g.GetComponent<Cell>();
        cell.id = i * (int) dimensions.x + j;
        cell.x = i;
        cell.y = j;

        return cell;
    }

    public Cell GetCellAt(int i, int j)
    {
        if (i >= dimensions.x || j >= dimensions.y)
        {
            return null;
        }
        //return cells[i * (int) dimensions.x + j];
        for(int it = 0; it < cells.Count; it++)
        {
            Cell cell = cells[it];            
            if (cell.x == i && cell.y == j)
            {
                return cell;
            }
        }
        return null;
    }    

    public void RotateLeft()
    {
        //iTween.PunchRotation(gameObject, new Vector3(1f, 1f, 1f), 1f);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(90f, Vector3.up), 1f);
    }

    public void RotateRight()
    {

    }    

    public void ReindexCells()
    {
        int id = 0;
        int x = 0;
        int y = 0;

        foreach(Cell cell in cells)
        {            
            if (x >= dimensions.x)
            {
                x = 0;
                y++;
            }
            else
            {
                x++;
            }

            cell.id = id;
            cell.x = x;
            cell.y = y;

            id++;
        }
    }

    public List<int> CheckHorizontalsLines()
    {
        List<int> lines = new List<int>();
        
        for (int j = 0; j < dimensions.y; j++)
        {
            bool cellLibreTrouvee = false;
            for (int i = 0; i < dimensions.x; i++)
            {
                Cell cell = GetCellAt(i, j);

                if (cell.block == null)
                {
                    cellLibreTrouvee = true;
                    break;
                }
            }

            if (!cellLibreTrouvee)
            {
                lines.Add(j);
            }
        }

        return lines;
    }

    public List<int> CheckVerticalsLines() {

        List<int> lines = new List<int>();

        for(int i = 0; i < dimensions.x; i++)
        {
            bool cellLibreTrouvee = false;
            for (int j = 0; j < dimensions.y; j++)
            {
                Cell cell = GetCellAt(i, j);

                if (cell.block == null)
                {
                    cellLibreTrouvee = true;
                    break;
                }
            }

            if (!cellLibreTrouvee)
            {
                lines.Add(i);
            }
        }

        return lines;
    }

    public void CheckAndDestroyLines()
    {
        List<int> horinzontalLines = CheckHorizontalsLines();
        List<int> verticalLines = CheckVerticalsLines();

        foreach(int line in horinzontalLines)
        {
            DestroyLines(line);
            Debug.Log("Ligne H: x=" + line);
        }
        foreach (int line in verticalLines)
        {
            DestroyLines(line, "V");
            Debug.Log("Ligne V: y=" + line);
        }

        //ReindexCells();
    }

    public void DestroyLines(int n, string sens = "H")
    {
        List<Cell> cellsToDestroy = new List<Cell>();
        if (sens.Equals("H"))
        {
            for(int x = 0; x < dimensions.x; x++)
            {
                Cell cell = GetCellAt(x, n);                

                cellsToDestroy.Add(cell);
            }

            

            // Déplace toutes les lignes au dessus vers le bas
            for(int y = n+1; y < dimensions.y; y++)
            {
                for(int x = 0; x < dimensions.x; x++)
                {
                    Cell cell = GetCellAt(x, y);
                    cell.y -= 1;
                }
            }

            dimensions.y--;
        }
        else
        {
            for(int y = 0; y < dimensions.y; y++)
            {
                Cell cell = GetCellAt(n, y);              

                cellsToDestroy.Add(cell);
            }

            

            // Déplace les lignes de droites vers la gauche
            for (int x = n; x < dimensions.x; x++)
            {
                for(int y = 0; y <dimensions.y; y++)
                {
                    Cell cell = GetCellAt(x, y);
                    cell.x -= 1;
                }
            }

            dimensions.x--;
        }

        foreach(Cell cell in cellsToDestroy)
        {
            DestroyCell(cell);
        }
    }

    public void DestroyCellAt(int i, int j)
    {

    }

    public void DestroyCell(Cell cell)
    {
        if (cell == null)
        {
            return;
        }
        cells.Remove(cell);

        /*if (cell.block != null)
        {
            Destroy(cell.block.gameObject);
        }*/
        Destroy(cell.gameObject);
    }
}
