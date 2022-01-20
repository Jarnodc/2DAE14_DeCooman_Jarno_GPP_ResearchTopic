using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square {
   
    private Piece pieceContainer; 
    private int x, y;

    public Square()
    {

    }

    public Square(Square square)
    {
        x = square.x;
        y = square.y;
        pieceContainer = null; 
    }
    	
    public void SetNewPiece(Piece piece)
    {
        if (piece != null )
        {
           
            if (pieceContainer != null && !pieceContainer.Propietary.Equals(piece.Propietary))
            {
                pieceContainer.Destroy(); 
            }
        }
        pieceContainer = piece;
        if (piece != null)
        {
            pieceContainer.SetNewSquare(this);
        }
    }
    public void InitPosition(int x, int y)
    {
        this.x = x;
        this.y = y; 
    }

    public bool HasPiece()
    {
        return pieceContainer != null; 
    }

    public override bool Equals(object obj)
    {
        Square otherSquare = obj as Square;
        return otherSquare != null && otherSquare.x == x && otherSquare.y == y; 
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public SquareBehaviour RelatedBehaviour
    {
        get
        {
            return Board.Instance.SquareBehaviourMatrix[Y, X]; 
        }
    }


    public int X
    {
        get
        {
            return x;
        }

    }

    public int Y
    {
        get
        {
            return y;
        }
    }

    public Piece PieceContainer
    {
        get
        {
            return pieceContainer;
        }
    }
}
