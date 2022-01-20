using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player  {

    public enum PlayerType
    {
        AI, 
        HUMAN
    }
    public enum GameState
    {
        Mate,
        Remise,
        Playing
    }

    private static int playerID = 0;
    private int iD; 

    private PlayerType type;
    private GameState gameState = GameState.Playing;
    private List<Piece> pieces;
    private King king;
    private int modSquareTableValue = 0;
    private int boardValue = 0; 

    public Player(PlayerType type = PlayerType.HUMAN, int modSquareValue = 0)
    {
        iD = Player.playerID;
        Player.playerID++;
        this.type = type;
        modSquareTableValue = modSquareValue; 
        pieces = new List<Piece>(); 
    }

    public Player (Player other, Square[,] newBoard)
    {
        iD = other.iD;
        modSquareTableValue = other.modSquareTableValue;
        type = other.type;
        pieces = new List<Piece>(); 
        for (int i = 0; i< other.pieces.Count; ++i)
        {
            var newPiece = other.pieces[i].Copy(newBoard[other.pieces[i].ActualSquare.Y, other.pieces[i].ActualSquare.X]);
            newPiece.SetPropietary(this); 
        }
    }

    public void Evaluate(Square[,] board = null)
    {
        foreach(var piece in pieces)
        {
            if (piece is King)
            {
               king = (piece as King);
            }
            piece.Evaluate(board);
        }

    }

    public int EvaluateBoardValue()
    {
        boardValue = 0;
        foreach (var piece in pieces)
        {
            boardValue += piece.EvaluateBoardScore(); 
        }
        return boardValue; 
    }

    public void EvaluateCastlings(Square[,] board = null, Player otherPlayer= null)
    {
        king.CastlingRound(board, otherPlayer);
    }

    public void AddPiece(Piece piece)
    {
        if (piece != null)
        {
            pieces.Add(piece);
        }
        
    }

    public void AddPieceInNumber(Piece piece ,int number)
    {
        pieces.Insert(number, piece); 
    }

    public int ReturnNumberPiece(Piece piece)
    {
        return pieces.FindIndex((Piece p) => { return p == piece; }); 
    }

    public void DestroyPiece(Piece piece)
    {
        pieces.Remove(piece);        
    }

    public override bool Equals(object other)
    {
        Player otherPlayer = other as Player; 
        return otherPlayer != null && otherPlayer.iD == this.iD;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public void EvaluateCheckOffMoves(Player otherPlayer, Square[,] b = null)
    {
        Square kingPosition = null;
        foreach (var piece in pieces)
        {
            if (piece.GetType() == typeof( King))
                kingPosition = piece.ActualSquare;
        }
        Square[,] actualBoard = null; 
        if (b == null)
        {
            actualBoard = Board.Instance.GenerateBoardCopy();
        }
        else
        {
            actualBoard = Board.Instance.GenerateBoardCopy(b); 
        }

        //Copies the player state and generates the possible moves.
        Player copyMe = new Player(this, actualBoard);
        Player copyOther = new Player(otherPlayer, actualBoard);
        copyMe.Evaluate(actualBoard);
        copyOther.Evaluate(actualBoard);
        copyMe.EvaluateCastlings(actualBoard, copyOther);
        copyOther.EvaluateCastlings(actualBoard, copyMe);
        List<Square> movesToErase = new List<Square>();

        for (int i = 0; i< pieces.Count; ++i)
        {
            var moves = pieces[i].PossibleMoves;
            var piece = copyMe.pieces[i]; 
            movesToErase.Clear(); 

           

            for (int j = 0; j < moves.Count; ++j)
            {
                //Makes the move and checks with the other player move to see if my move is valid. 
                var previousPiece = piece.PossibleMoves[j].Square.PieceContainer;
                var previousSquare = piece.ActualSquare; 
              
                piece.Move(actualBoard[moves[j].Square.Y, moves[j].Square.X]);
                piece.PossibleMoves[j].RunCallback();
                copyOther.Evaluate(actualBoard);


                //Checks if the move is possible. If my piece is the king check if some of the other player moves eat him. 
                if (piece is King) 
                {
                    if (copyOther.CheckIfSquareIsInMoves(piece.ActualSquare))
                    {
                        movesToErase.Add(moves[j].Square);
                    }
                }
                else
                {
                    if (copyOther.CheckIfSquareIsInMoves(kingPosition))
                    {
                        movesToErase.Add(moves[j].Square);
                    }
                }

                piece.Move(previousSquare);
                copyOther.AddPiece(previousPiece);
                if (previousPiece != null)
                    previousPiece.Move(actualBoard[moves[j].Square.Y, moves[j].Square.X]); 
                piece.PossibleMoves[j].RunCallbackReset(); 
            
            }

            for (int j= 0; j< movesToErase.Count; ++j)
            {
                this.pieces[i].RemoveMove(movesToErase[j]); 
            }

            
        }
        gameState = UpdateGameState(otherPlayer);
    }

    private GameState UpdateGameState(Player other)
    {
        GameState state = GameState.Playing;
        foreach (var piece in pieces)
        {
            if (piece.PossibleMoves.Count > 0)
                return GameState.Playing;
            else if (piece is King)
            {
                if (other.CheckIfSquareIsInMoves(piece.ActualSquare))
                {
                    state = GameState.Mate;
                }
                else
                {
                    state = GameState.Remise;
                }
            }
        }
        return state;
    }

    public void DestroyLastPiece()
    {
        pieces[pieces.Count - 1].Destroy(); 
    }

    public bool CheckIfSquareIsInMoves(Square square)
    {
        foreach (var piece in pieces)
        {
            for (int j = 0; j< piece.PossibleMoves.Count; ++j)
            {
                if (piece.PossibleMoves[j].IsHarmMove && piece.PossibleMoves[j].Square.Equals(square))
                {                  
                    return true; 
                }
            }
        }
        return false; 
    }
      
    public void ResetPawnsState()
    {
        for (int i = 0; i< pieces.Count; ++i)
        {
            Pawn pawn = pieces[i] as Pawn; 
            if (pawn != null)
            {
                pawn.ResetJustMovedTwo(); 
            }
        }
    }

    public PlayerType Type
    {
        get
        {
            return type;
        }
    }

    public int ID
    {
        get
        {
            return iD;
        }

    }

    public int ModSquareTableValue
    {
        get
        {
            return modSquareTableValue;
        }

      
    }
    public List<Piece> Pieces
    {
        get
        {
            return pieces;
        }

       
    }
    public GameState _GameState
    {
        get
        {
            return gameState;
        }
    }
}
