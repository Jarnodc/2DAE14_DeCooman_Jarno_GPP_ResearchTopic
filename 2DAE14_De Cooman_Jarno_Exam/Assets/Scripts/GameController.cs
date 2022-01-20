using System;
using System.IO; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameController : Singleton<GameController>
{
    [Header("Pieces prefabs")]
    [SerializeField] private GameObject kingPrefab;
    [SerializeField] private GameObject queenPrefab, rookPrefab, bishopPrefab, knightPrefab, pawnPrefab;
    [SerializeField] private int queenValue;

    [SerializeField] private CanvasGroup cg;
    [SerializeField] private Text textEnd;
    [SerializeField] private SquareTableValues queenSquareTableValues;
                     
    private Player playerOne, playerTwo;
    private Player actualPlayer, otherPlayer;

    private MiniMax miniMax;

    void Start () {
        cg.alpha = 0;

        playerOne = new Player(Player.PlayerType.HUMAN,7);
        playerTwo = new Player(Player.PlayerType.AI);
        miniMax = new MiniMax(); 

        InstantiatePlayerOnePieces();
        InstantiatePlayerTwoPieces();

        actualPlayer = playerOne;
        otherPlayer = playerTwo;

        EvaluatePlayers(); 
    }


    private void InstantiatePlayerOnePieces()
    {
        var board = Board.Instance.SquareBehaviourMatrix;

        for (int i = 0; i < 8; ++i)
        {
            InstantiatePiece(PieceType.PAWN, board[1, i], playerOne);
        }
        InstantiatePiece(PieceType.ROOK, board[0, 0], playerOne);
        InstantiatePiece(PieceType.ROOK, board[0, 7], playerOne);

        InstantiatePiece(PieceType.KNIGHT, board[0, 1], playerOne);
        InstantiatePiece(PieceType.KNIGHT, board[0, 6], playerOne);

        InstantiatePiece(PieceType.BISHOP, board[0, 2], playerOne);
        InstantiatePiece(PieceType.BISHOP, board[0, 5], playerOne);

        InstantiatePiece(PieceType.QUEEN, board[0, 3], playerOne);
        InstantiatePiece(PieceType.KING, board[0, 4], playerOne);
    }

    private void InstantiatePlayerTwoPieces()
    {
        var board = Board.Instance.SquareBehaviourMatrix;
        for (int i = 0; i < 8; ++i)
        {
            InstantiatePiece(PieceType.PAWN, board[6, i], playerTwo);
        }
        InstantiatePiece(PieceType.ROOK, board[7, 0], playerTwo);
        InstantiatePiece(PieceType.ROOK, board[7, 7], playerTwo);

        InstantiatePiece(PieceType.KNIGHT, board[7, 1], playerTwo);
        InstantiatePiece(PieceType.KNIGHT, board[7, 6], playerTwo);

        InstantiatePiece(PieceType.BISHOP, board[7, 2], playerTwo);
        InstantiatePiece(PieceType.BISHOP, board[7, 5], playerTwo);

        InstantiatePiece(PieceType.QUEEN, board[7, 3], playerTwo);
        InstantiatePiece(PieceType.KING, board[7, 4], playerTwo);
    }
    public void InstantiatePiece(PieceType type,SquareBehaviour squareBehaviour, Player player)
    {
        GameObject prefab = null;
        switch (type)
        {
            case PieceType.PAWN:
                prefab = pawnPrefab;
                break;
            case PieceType.QUEEN:
                prefab = queenPrefab;
                break;
            case PieceType.ROOK:
                prefab = rookPrefab;
                break;
            case PieceType.KNIGHT:
                prefab = knightPrefab;
                break;
            case PieceType.KING:
                prefab = kingPrefab;
                break;
            case PieceType.BISHOP:
                prefab = bishopPrefab;
                break;
        }


        var piece = ((GameObject)Instantiate(prefab)).GetComponent<PieceBehaviour>();
        piece.InitGraphics(player.ID); 
            piece.transform.position = new Vector3(squareBehaviour.transform.position.x, piece.transform.position.y, squareBehaviour.transform.position.z);
            squareBehaviour.Square.SetNewPiece(piece.Piece);
            piece.Piece.SetPropietary(player);
    }

    public void Reset()
    {
        SceneManager.LoadScene(0); 
    }


    public void InstantiateQueen(Square square, Player player)
    {
        var piece = new Queen(queenValue, queenSquareTableValues, square, player); 
    }

    public void ChangeTurns()
    {
        if (actualPlayer== playerOne)
        {
            actualPlayer = playerTwo;
            otherPlayer = playerOne;
        }
        else
        {
            actualPlayer = playerOne;
            otherPlayer = playerTwo; 
        }
    }

    public void EvaluatePlayers()
    {
        actualPlayer.Evaluate(null);
        otherPlayer.Evaluate(null);
        actualPlayer.EvaluateCastlings(null, otherPlayer);
        otherPlayer.EvaluateCastlings(null, actualPlayer); 

        actualPlayer.EvaluateCheckOffMoves(otherPlayer);
        otherPlayer.ResetPawnsState();

        switch(actualPlayer._GameState)
        {
            case Player.GameState.Playing:
                if (actualPlayer.Type == Player.PlayerType.AI)
                {
                    miniMax.RunMiniMax(actualPlayer, otherPlayer, Board.Instance.SquareMatrix);
                }
                break;
            case Player.GameState.Remise:
                cg.alpha = 1;
                textEnd.text = "It is a tie. No winners here. :D";
                break;
            case Player.GameState.Mate:
                cg.alpha = 1;
                textEnd.text = "Player: " + otherPlayer.ID + ": " + otherPlayer.Type.ToString() + " wins";
                break;
        }
    }

    public Player ActualPlayer
    {
        get
        {
            return actualPlayer;
        }

    }
}

public enum PieceType
{
    PAWN,
    KING,
    QUEEN, 
    KNIGHT,
    BISHOP, 
    ROOK
}
