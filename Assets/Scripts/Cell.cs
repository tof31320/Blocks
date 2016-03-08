using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

    public int id = 0;

    [SerializeField]
    private int _x = 0;
    [SerializeField]
    private int _y = 0;
    public int x
    {
        get { return _x; }
        set
        {
            _x = value;

            //transform.position = Grille.instance.GetCellPosition(_x, _y);
            iTween.MoveTo(gameObject, iTween.Hash(
                "position", Grille.instance.GetCellPosition(_x, _y),
                "time", 1f,
               "easetype", iTween.EaseType.easeOutBounce
                ));
        }
    }
    public int y
    {
        get { return _y; }
        set
        {
            _y = value;
            //transform.position = Grille.instance.GetCellPosition(_x, _y);
            iTween.MoveTo(gameObject, iTween.Hash(
               "position", Grille.instance.GetCellPosition(_x, _y),
               "time", 1f,
               "easetype", iTween.EaseType.easeOutBounce
               ));

        }
    }
    
	public Block block
    {
        get {
            Block b = null;
            if (transform.childCount > 0)
            {
                b = transform.GetChild(0).GetComponent<Block>();
            }
            return b;
        }
        set
        {
            if (transform.childCount > 0)
            {
                transform.DetachChildren();
            }

            Block b = value;
            if (b != null)
            {
                b.transform.SetParent(transform);
            }                   
        }
    }   
}
