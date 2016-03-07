using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UIPanelPieces : MonoBehaviour {

    public GameObject pieceButtonPrefab;

    public UIPieceButton currentButton;

    public List<UIPieceButton> buttons = new List<UIPieceButton>();

	// Use this for initialization
	void Start () {

        /*AddPieceButton(PieceType.UNIT);
        AddPieceButton(PieceType.L_TYPE);
        AddPieceButton(PieceType.I_TYPE);
        AddPieceButton(PieceType.N1_TYPE);
        AddPieceButton(PieceType.N2_TYPE);
        AddPieceButton(PieceType.O_TYPE);
        AddPieceButton(PieceType.P_TYPE);
        AddPieceButton(PieceType.T_TYPE);*/

        AddRandomPieceButton();
        AddRandomPieceButton();
        AddRandomPieceButton();
        AddRandomPieceButton();
        AddRandomPieceButton();
        AddRandomPieceButton();
        AddRandomPieceButton();
    }
	
	public void AddPieceButton(PieceType type)
    {
        GameObject g = Instantiate(pieceButtonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        g.transform.SetParent(transform);

        UIPieceButton btn = g.GetComponent<UIPieceButton>();
        btn.pieceType = type;

        buttons.Add(btn);
    }

    public void AddRandomPieceButton()
    {
        Array types = Enum.GetValues(typeof(PieceType));

        int rand = UnityEngine.Random.Range(0, types.Length);
        PieceType type = (PieceType)rand;

        AddPieceButton(type);
    }

    public void DropCurrentButton()
    {
        if (currentButton == null)
        {
            return;
        }

        Destroy(currentButton.gameObject);
        AddRandomPieceButton();
    }
}
