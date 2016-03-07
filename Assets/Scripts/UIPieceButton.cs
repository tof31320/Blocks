using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPieceButton : MonoBehaviour {

    [SerializeField]
    private PieceType _pieceType;
    public PieceType pieceType
    {
        get
        {
            return _pieceType;
        }
        set
        {
            _pieceType = value;
            Sprite spr = null;
            switch (_pieceType)
            {
                case PieceType.UNIT: spr = sprites[0]; break;
                case PieceType.I_TYPE: spr = sprites[1]; break;
                case PieceType.L_TYPE: spr = sprites[2]; break;
                case PieceType.N1_TYPE: spr = sprites[3]; break;
                case PieceType.N2_TYPE: spr = sprites[4]; break;
                case PieceType.O_TYPE: spr = sprites[5]; break;
                case PieceType.P_TYPE: spr = sprites[6]; break;
                case PieceType.T_TYPE: spr = sprites[7]; break;
            }

            if (spr != null)
            {
                image.sprite = spr;
            }
        }
    }

    public Sprite[] sprites;

    public Image image;

	// Use this for initialization
	void Awake () {
        image = GetComponent<Image>();

        pieceType = PieceType.UNIT;
	}	

    public void OnSelect()
    {
        GameController.instance.currentPiece = Piece.Create(pieceType);
        UIManager.instance.panelPieces.currentButton = this;
    }
}
