using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Piece  {
    private Player propietary;
    protected List<Move> possibleMoves;
    protected int pieceValue;

    protected int totalPieceValue;
    protected Square actualSquare;
    protected SquareTableValues values;
    protected PieceBehaviour behaviour;

    public Piece()
    {

    }
    public Piece (PieceBehaviour behaviour, int pieceValue, SquareTableValues values)
    {
        this.behaviour = behaviour;
        this.pieceValue = pieceValue;
        this.values = values;
        possibleMoves = new List<Move>();
    }

    public virtual Piece Copy(Square copySquare)
    {
        return null; 
    }

    public virtual void Evaluate(Square[,] board = null)
    {

    }

    public int EvaluateBoardScore()
    {
        totalPieceValue = pieceValue + values.squareValues[actualSquare.X, Mathf.Abs(actualSquare.Y - propietary.ModSquareTableValue)];
        return totalPieceValue; 
    }
    

    public virtual void Move(Square destiny)
    {
        actualSquare.SetNewPiece(null);
        destiny.SetNewPiece(this); 
    }

    public virtual void Move (SquareBehaviour destiny)
    {
        Move(destiny.Square);
        if (behaviour != null)
        {
            behaviour.Transform(new Vector3(destiny.transform.position.x, behaviour.transform.position.y, destiny.transform.position.z));
        }
    }
    
    public void Destroy()
    {
        if (propietary != null)
        {
            propietary.DestroyPiece(this);
            if (this.behaviour!= null)
                MonoBehaviour.Destroy(this.behaviour.gameObject); 
        }
    }

    public void SetNewSquare(Square square)
    {
        this.actualSquare = square; 
    }

    public void SetPropietary(Player player)
    {
        this.propietary = player;
        propietary.AddPiece(this); 
    }

    public int ActualValue()
    {
        return pieceValue + values.squareValues[actualSquare.X, actualSquare.Y];
    }

    public void RemoveMove(Square value)
    {
        for (int i = 0; i < possibleMoves.Count; ++i)
        {
            if (possibleMoves[i].Square.Equals(value))
            {
                possibleMoves.RemoveAt(i);
                break; 
           }
        } 
    }

    public void RemoveMove(int value)
    {
        possibleMoves.RemoveAt(value); 
    }

    public Player Propietary
    {
        get
        {
            return propietary;
        }
    }

    public List<Move> PossibleMoves
    {
        get
        {
            return possibleMoves;
        }
    }

    public Square ActualSquare
    {
        get
        {
            return actualSquare;
        }

    }

    protected int TotalPieceValue
    {
        get
        {
            return totalPieceValue;
        }

    }

    public SquareTableValues Values { get => values; }
}
