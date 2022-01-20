using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehaviour : MonoBehaviour {
    [SerializeField]
    private PieceType type;
    [SerializeField]
    private int pieceValue;
    [SerializeField]
    private SquareTableValues values;
    [SerializeField]
    private Sprite spriteWhite, spriteBlack;
    private SpriteRenderer spriteRend; 
    private Piece piece;

    public Piece Piece
    {
        get
        {
            return piece;
        }

    }
    void Awake () {
        values.Init(); 
        switch (type)
        {
            case PieceType.BISHOP:
                piece = new Bishop(this, pieceValue, values); 
                break;
            case PieceType.KING:
                piece = new King(this, pieceValue, values);
                break;

            case PieceType.KNIGHT:
                piece = new Knight(this, pieceValue, values); 
                break;

            case PieceType.PAWN:
                piece = new Pawn(this, pieceValue, values); 
                break;

            case PieceType.QUEEN:
                piece = new Queen(this, pieceValue, values); 
                break;

            case PieceType.ROOK:
                piece = new Rook(this, pieceValue, values); 
                break; 
        }
	}

    public void InitGraphics(int id) {
        spriteRend = this.GetComponentInChildren<SpriteRenderer>();
        spriteRend.sprite = id == 0 ? spriteWhite : spriteBlack; 
    }

    public void ChangeMaterial(Material material)
    {
        var rends = this.GetComponentsInChildren<MeshRenderer>(); 
        for (int i= 0; i< rends.Length; ++i)
        {
            rends[i].material = material; 
        }
    }

    public void Transform(Vector3 pos)
    {
        transform.position = pos;
    }
}
