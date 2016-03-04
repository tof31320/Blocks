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

    public Cell CreateAndCell(int i, int j)
    {
        Vector3 cellPosition = new Vector3(transform.position.x + i * CELL_SIZE,
                                           transform.position.y,
                                           transform.position.z + j * CELL_SIZE);

        GameObject g = Instantiate(cellPrefab, cellPosition, Quaternion.identity) as GameObject;
        g.transform.SetParent(transform);
        Cell cell = g.GetComponent<Cell>();

        return cell;
    }
}
