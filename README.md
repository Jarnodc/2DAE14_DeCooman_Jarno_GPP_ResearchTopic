# Chess AI BOT
A bot that plays [Chess](https://en.wikipedia.org/wiki/Chess) using AI [Minimax](https://en.wikipedia.org/wiki/Minimax).

## Description
### How?
I am trying to make simple chess game in Unity where you can play against a bot that is controlled through a self-written AI. The algortihm I am going to use is MiniMax. MiniMax is a version of AI where the program creates a kind of search tree of all possible moves starting with your own moves and then the opponent's moves on top of yours and so on. Each move has a score and in this way the best move can be determined. The move must have the highest possible score for your own move and the smallest possible score for the opponent's move.

### MiniMax
Minimax is a kind of backtracking algorithm that is often used for decision making games, like Chess, Tic Tac Toe, etc. It is a way to find the optimal move for a player, and in most cases the AI-bot. In this algorithm you have 2 variables you have to hold. One of them is the maximizer, which holds the maximum possible score/value. And the other is the minimizer which tries to do the opposite and get the lowest score possible. The reason why you need to hold the minimum score too is because when the opposite player do a bad move with a low score, it is in most cases a good move for you. So when you calculate the move with the highest score combined with the lowest score for the opposite, you get your best possible move. If one player is winning, then the other player must be losing. So one's profit is related to another's loss.

Every possible board state has a value associated with it. When the value of a square is possitive, you can say the maximizer should be in the upper hand. When it is negative, the minimizer is in the upper hand. And since this is a backtracking algorithm, it first calculates every possible move and then backtracks it to make a decision.

## Implemented
### Chess
For AI I have writtten, I used a templated unity chess game where no logic is given to. Only generating the chess board with the pieces on the right place with the right possible moves to. It just missed the logic to move a piece itself. Because with the template, you could only move it yourself against yourself.
## Problems
## Evalution
## Sources
