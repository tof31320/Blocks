using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PieceType
{
    UNIT = 0,
    L_TYPE = 1,
    P_TYPE = 2,
    O_TYPE = 3,
    N1_TYPE = 4,
    N2_TYPE = 5,
    I_TYPE = 6,
    T_TYPE = 7
}

public class Piece : MonoBehaviour {

    public GameObject blockPrefab;

    private PieceType _type = PieceType.UNIT;
    public PieceType type
    {
        get { return _type; }
        set
        {
            RemoveBlocks();
            _type = value;
            BuildBlocks();
        }
    }

    private Color _color = Color.green;
    public Color color
    {
        get { return _color; }
        set
        {
            _color = value;
            foreach(Block b in blocks)
            {
                b.color = _color;
            }
        }
    }

    public List<Block> blocks = new List<Block>();

    private bool _locked = false;
    public bool locked
    {
        get { return _locked; }
        set
        {
            _locked = value;            
        }
    }

    public float currentRotation = 0f;

    public static Piece Create(PieceType type)
    {
        GameObject p = Instantiate(GameController.instance.piecePrefab, new Vector3(5f,0f, 5f), Quaternion.identity) as GameObject;

        Piece piece = p.GetComponent<Piece>();
        piece.type = type;
        piece.color = Color.green;

        return piece;
    }

    public void BuildBlocks()
    {
        switch (type)
        {
            case PieceType.UNIT:
                Block b = CreateAddBlockAtRelativePosition(0, 0);
                break;
            case PieceType.I_TYPE:
                CreateAddBlockAtRelativePosition(0, 0);
                CreateAddBlockAtRelativePosition(1, 0);
                CreateAddBlockAtRelativePosition(2, 0);
                CreateAddBlockAtRelativePosition(3, 0);
                break;
            case PieceType.L_TYPE:
                CreateAddBlockAtRelativePosition(0, 0);
                CreateAddBlockAtRelativePosition(0, 1); CreateAddBlockAtRelativePosition(1, 1); CreateAddBlockAtRelativePosition(2, 1);                
                break;
            case PieceType.P_TYPE:
                                                                                                CreateAddBlockAtRelativePosition(2, 0);
                CreateAddBlockAtRelativePosition(0, 1); CreateAddBlockAtRelativePosition(1, 1); CreateAddBlockAtRelativePosition(2, 1);
                break;
            case PieceType.N1_TYPE:
                                                        CreateAddBlockAtRelativePosition(1, 0); CreateAddBlockAtRelativePosition(2, 0);
                CreateAddBlockAtRelativePosition(0, 1); CreateAddBlockAtRelativePosition(1, 1);                
                break;
            case PieceType.N2_TYPE:
                CreateAddBlockAtRelativePosition(0, 0); CreateAddBlockAtRelativePosition(1, 0);
                                                        CreateAddBlockAtRelativePosition(1, 1); CreateAddBlockAtRelativePosition(2, 1);                
                break;
            case PieceType.O_TYPE:
                CreateAddBlockAtRelativePosition(0, 0);
                CreateAddBlockAtRelativePosition(0, 1);
                CreateAddBlockAtRelativePosition(1, 0);
                CreateAddBlockAtRelativePosition(1, 1);
                break;
            case PieceType.T_TYPE:
                CreateAddBlockAtRelativePosition(0, 0);
                CreateAddBlockAtRelativePosition(1, 0);
                CreateAddBlockAtRelativePosition(2, 0);
                CreateAddBlockAtRelativePosition(1, 1);
                break;
        }
    }

    public void RemoveBlocks()
    {
        foreach(Block b in blocks)
        {
            Destroy(b.gameObject);
        }
        blocks.Clear();
    }

    private Block CreateAddBlockAtRelativePosition(int x, int y)
    {
        Vector3 position = new Vector3(x * Grille.instance.CELL_SIZE, 0.56f, y * Grille.instance.CELL_SIZE);

        GameObject b = Instantiate(blockPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        b.transform.SetParent(transform);
        b.transform.localPosition = position;

        Block block = b.GetComponent<Block>();
        block.pieceCoords = new Vector2(x, y);
        blocks.Add(block);

        return block;
    }

    public void Rotate()
    {
        //currentRotation = currentRotation + 90f;
        transform.Rotate(new Vector3(0f, 90f, 0f));
        //Debug.Log("Rotation: " + currentRotation+ " Piece:" + gameObject.GetInstanceID());
        //transform.rotation = Quaternion.AngleAxis(180f, transform.up);
    }

    public bool AboveFreeCells()
    {
        foreach(Block b in blocks)
        {
            if (b.onCollision || b.cellCollision == null)
            {
                return false;
            }
        }
        return true;
    }    

    public void Pose()
    {
        color = Color.blue;
        foreach(Block b in blocks)
        {
            b.transform.localScale = Vector3.Lerp(b.transform.localScale, new Vector3(1f, 1f, 1f), 0.9f);
            b.transform.SetParent(b.cellCollision.transform);
            b.cell = b.cellCollision;
        }
    }
}
