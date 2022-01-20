using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMax
{
    private Piece pieceToMove;
    private Move bestMove;
    private int maxDepth = 3;

    public void RunMiniMax(Player maxPlayer, Player minPlayer, Square[,] board)
    {
        MiniMaxFunctionRoot(maxPlayer, minPlayer, board);
       
        pieceToMove.Move(bestMove.Square.RelatedBehaviour);
        bestMove.RunCallback();

        GameController.Instance.ChangeTurns();
        GameController.Instance.EvaluatePlayers();
    }

    private void MiniMaxFunctionRoot(Player maxPlayer, Player minPlayer, Square[,] board)
    {
        int bestValue = int.MinValue;
        for (int i = 0; i < maxPlayer.Pieces.Count; ++i)
        {
            for (int j = 0; j < maxPlayer.Pieces[i].PossibleMoves.Count; ++j)
            {

                //Generate a copy of the game state (players and board)
                Square[,] boardCopy = Board.Instance.GenerateBoardCopy(board);
                Player maxCopy = new Player(maxPlayer, boardCopy);
                Player minCopy = new Player(minPlayer, boardCopy);

                maxCopy.Evaluate(boardCopy);
                minCopy.Evaluate(boardCopy);

                maxCopy.EvaluateCastlings(boardCopy, minCopy);
                minCopy.EvaluateCastlings(boardCopy, maxCopy);

                maxCopy.EvaluateCheckOffMoves(minCopy, boardCopy);
                minCopy.ResetPawnsState();

                maxCopy.Pieces[i].Move(maxCopy.Pieces[i].PossibleMoves[j].Square);
                maxCopy.Pieces[i].PossibleMoves[j].RunCallback();

                int value = MiniMaxFunction(maxCopy, minCopy, boardCopy, false, maxDepth - 1, int.MinValue, int.MaxValue); 

                if (value >= bestValue)
                {
                    bestValue = value;
                    bestMove = maxPlayer.Pieces[i].PossibleMoves[j];
                    pieceToMove = maxPlayer.Pieces[i];

                }


            }
        }
    }
    //public int MiniMaxFunc(Player maxPlayer, Player minPlayer, Square[,] board)
    //{
    //    Square[,] copyBoard = Board.Instance.GenerateBoardCopy(board);
    //    Player actualPlayer = new Player(maxPlayer, copyBoard);
    //    Player otherPlayer = new Player(minPlayer, copyBoard);

    //    for (int i = 0; i < maxDepth; ++i)
    //    {
    //       Player temp = actualPlayer;
    //       actualPlayer = otherPlayer;
    //       otherPlayer = temp;

    //        actualPlayer.Evaluate(copyBoard);
    //        otherPlayer.Evaluate(copyBoard);
    //        actualPlayer.EvaluateCastlings(copyBoard, otherPlayer);
    //        otherPlayer.EvaluateCastlings(copyBoard, actualPlayer);
    //        actualPlayer.EvaluateCheckOffMoves(otherPlayer, copyBoard);
    //        otherPlayer.ResetPawnsState();

    //        int bestValue = i % 2 == 1 ? int.MinValue : int.MaxValue;

    //        foreach(Piece piece in actualPlayer.Pieces)
    //        {
    //            foreach(Move move in piece.PossibleMoves)
    //            {

    //            }
    //        }
           
    //    }

    //    return EvaluateBoard(maxPlayer, minPlayer);
    //}

    public int MiniMaxFunction(Player maxPlayer, Player minPlayer, Square[,] board, bool maxTurn, int depth, int alpha, int beta)
    {
        if (depth == 0)
        {
            return EvaluateBoard(maxPlayer, minPlayer) ;
        }

        //Generate all the moves
        Player actualPlayer = maxTurn ? maxPlayer : minPlayer;
        Player otherPlayer = maxTurn ? minPlayer : maxPlayer;

        actualPlayer.Evaluate(board);
        otherPlayer.Evaluate(board);
        actualPlayer.EvaluateCastlings(board, otherPlayer);
        otherPlayer.EvaluateCastlings(board, actualPlayer);
        actualPlayer.EvaluateCheckOffMoves(otherPlayer, board);
        otherPlayer.ResetPawnsState();

        int bestValue = maxTurn ? int.MinValue : int.MaxValue;
        for (int i = 0; i < actualPlayer.Pieces.Count; ++i)
        {
            var piece = actualPlayer.Pieces[i];
            var moves = actualPlayer.Pieces[i].PossibleMoves;
            for (int j = 0; j < actualPlayer.Pieces[i].PossibleMoves.Count; ++j)
            {
                var previousPiece = moves[j].Square.PieceContainer;
                var previousSquare = piece.ActualSquare;
                //Temporal move with the board copy. After this we will restore the board to its previous state.  
                piece.Move(moves[j].Square);
                piece.PossibleMoves[j].RunCallback();

                Square[,] copyBoard = Board.Instance.GenerateBoardCopy(board);
      
                if (maxTurn) //Max
                {

                    bestValue = Mathf.Max(bestValue, MiniMaxFunction(new Player(actualPlayer, copyBoard), new Player(otherPlayer, copyBoard), copyBoard, !maxTurn, depth - 1, alpha, beta));
                    alpha = Mathf.Max(bestValue, alpha);
                }
                else //Min
                {
                    bestValue = Mathf.Min(bestValue, MiniMaxFunction(new Player(otherPlayer, copyBoard), new Player(actualPlayer, copyBoard), copyBoard, !maxTurn, depth - 1, alpha, beta));
                    beta = Mathf.Min(beta, bestValue);
                }

                //Restore the previous state of the board to continue the loop
                piece.Move(previousSquare);
                otherPlayer.AddPiece(previousPiece);
                if (previousPiece != null)
                {
                    previousPiece.Move(board[moves[j].Square.Y, moves[j].Square.X]);
                }
                piece.PossibleMoves[j].RunCallbackReset();

                //Prune
                if (beta <= alpha)
                {
                    return bestValue;
                }
            }
        }
        return bestValue  ;
    }
    public int EvaluateBoard(Player max, Player min)
    {
        return max.EvaluateBoardValue() - min.EvaluateBoardValue();
    }


}
