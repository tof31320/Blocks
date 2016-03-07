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

    private Block _block = null;
    [SerializeField]
	public Block block
    {
        get { return _block; }
        set
        {
            if (_block == null || value == null)
            {
                _block = value;
            }                   
        }
    }
}
