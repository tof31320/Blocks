using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    private static UIManager _instance = null;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
            }
            return _instance;
        }
    }

    public UIPanelPieces panelPieces;

    public UIScoresPanel scoresPanel;
}
