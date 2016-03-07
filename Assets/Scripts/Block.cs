using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public MeshRenderer meshRenderer;

    public Light l;

    private Cell _cell = null;
    public Cell cell
    {
        get { return _cell; }
        set
        {
            _cell = value;
            if (_cell != null)
            {
                _cell.block = this;
            }
        }
    }

    public Vector2 pieceCoords = Vector2.zero;
   
    public bool onCollision = false;

    private Color _color = Color.blue;
    public Color color
    {
        get { return _color; }
        set
        {
            _color = value;
            meshRenderer.material.color = _color;
            l.color = _color;
        }
    }
	
    public void OnTriggerEnter(Collider collision)
    {
        Cell cell = collision.gameObject.GetComponent<Cell>();
        if (cell != null)
        {
            this.cell = cell;
        }

        Block b = collision.gameObject.GetComponent<Block>();
        if (b != null)
        {
            onCollision = true;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        Cell cell = collision.gameObject.GetComponent<Cell>();
        if (cell != null && cell.block == this && this.cell != null)
        {
            this.cell.block = null;
            this.cell = null;
        }

        Block b = collision.gameObject.GetComponent<Block>();
        if (b != null)
        {
            onCollision = false;
        }
    }
}
