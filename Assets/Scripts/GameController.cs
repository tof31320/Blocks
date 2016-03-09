using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    private static GameController _instance = null;
    public static GameController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            }
            return _instance;
        }
    }

    public Grille grille;

    private int _currentPlayerIndex = 0;
    public int currentPlayerIndex
    {
        get { return _currentPlayerIndex; }
        set
        {           
            _currentPlayerIndex = value;
            if (currentPlayerIndex >= players.Count)
            {
                currentPlayerIndex = 0;
            }            
        }
    }
    public List<UIScore> players = new List<UIScore>();
    public UIScore currentPlayer
    {
        get { return players[currentPlayerIndex]; }
    }

    public void Start()
    {
        ResetGrille();

        players = UIManager.instance.scoresPanel.scores;
        currentPlayerIndex = 0;
    }

    private void ResetGrille()
    {
        grille.Build();
    }

    public GameObject piecePrefab;

    private Piece _currentPiece = null;
    public Piece currentPiece
    {
        get
        {
            return _currentPiece;
        }
        set
        {
            if (_currentPiece != null)
            {
                Destroy(_currentPiece.gameObject);
            }
            _currentPiece = value;
        }
    }

    private Cell currentCell = null;

    public void NextPlayer()
    {
        currentPlayer.selected = false;
        currentPlayerIndex++;
        currentPlayer.selected = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentPiece = Piece.Create(PieceType.UNIT);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentPiece = Piece.Create(PieceType.I_TYPE);
        }else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentPiece = Piece.Create(PieceType.L_TYPE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentPiece = Piece.Create(PieceType.P_TYPE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentPiece = Piece.Create(PieceType.N1_TYPE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentPiece = Piece.Create(PieceType.N2_TYPE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            currentPiece = Piece.Create(PieceType.O_TYPE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            currentPiece = Piece.Create(PieceType.T_TYPE);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            List<int> hLines = grille.CheckHorizontalsLines();
            List<int> vLines = grille.CheckVerticalsLines();

            Debug.Log("HLines: " + hLines.Count + " , VLines=" + vLines.Count);

            grille.CheckAndDestroyLines();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            currentPiece = null;
            UIManager.instance.panelPieces.currentButton = null;
        }
        

        if (currentPiece != null)
        {
            Vector3 position = FollowMousePosition();
            if (position.x != 9999f)
            {
                //currentPiece.transform.position = Vector3.Lerp(currentPiece.transform.position, position, 0.8f);
                currentPiece.transform.position = position;

                if (currentPiece.AboveFreeCells())
                {
                    currentPiece.color = Color.green;
                }
                else
                {
                    currentPiece.color = Color.red;
                }
            }            
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentPiece != null)
        {
            currentPiece.Rotate();
        }

        if (Input.GetMouseButtonDown(0) 
        && !EventSystem.current.IsPointerOverGameObject()
        && currentPiece != null
        && currentPiece.AboveFreeCells())
        {            
            //currentCell.piece = _currentPiece;            
            _currentPiece.Pose();
            ShakeCamera();

            int nbLignes = 0;// grille.CheckLines();
            //Debug.Log("Lignes : " + nbLignes);
            grille.CheckAndDestroyLines();
            AddScoreAtCurrentPlayer(_currentPiece.blocks.Count + nbLignes * _currentPiece.blocks.Count);
            _currentPiece = null;

            UIManager.instance.panelPieces.DropCurrentButton();

            NextPlayer();           
        }
    }

    public Vector3 FollowMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f, 1 << LayerMask.NameToLayer("Cells")))
        {
            currentCell = hit.collider.gameObject.GetComponent<Cell>();
            if (currentCell != null)
            {
                return currentCell.transform.position;
            }
        }
        return new Vector3(9999f, -1f, -1f);
    }

    public void AddScoreAtCurrentPlayer(int score)
    {
        currentPlayer.score += score;
    }

    public void ShakeCamera()
    {
        iTween.PunchPosition(Camera.main.gameObject, iTween.Hash(
            "amount", new Vector3(0f, 0f, 1f),
            "time", 0.2f
            ));
    }
}
