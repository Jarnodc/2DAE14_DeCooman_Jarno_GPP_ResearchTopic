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
For AI I have writtten, I used a templated unity chess game where no AI logic is given to. There was only the logic of every piece and the board. It has made some datamaps to store that info in. It had some functionality in every piece where I could check if the position was possible. It had also a player class where logic was given so 2 players could play to each other. I changed it a bit so 1 player could be the AI. I wrote the MiniMax script that is controlled by the player AI and added some functionality.

### AI
So to calculate the position, I wrote a MiniMax script. A lot of sources on the internet uses an recursion function, so did I. This means that in the function, you call your function again. In the calculation. I make for every piece move a fake move so I could calculate the board value for every possible move the AI could do. Then it is very easy to get the best move because it is just the heighest boardvalue. To calculate if it is better or worser. I use a Beta and Alpha pruning. These variables stored by the AI to compare with each other to get the move with a higher score. That is also variables to cut the rest of the search, basically to optimize on its depth searching time. There is also a basic implementation of a weighted heuristic. This involves the position of each individual piece. A simple example would be having a knight near the middle of the board as it would open up more available moves for it, than being in a corner or side of the chess board.

## Problems
In the beginning, I tried to make my MiniMax function an iterative function. So I made a for loop till I reached my max depth. This was very difficult to do because there was a lot of variables I had to store. For performance, it was also a bad idea because you needed mor memory to stores these variables and he checked to much times a useless move. I also didn't use those beta and alpha variables. This was also stupid to do because I need to write more then needed because when you call yourself, you need to write it once.

The depth is now 3 deep and I first checked it with 5 or 4 because normally then you could get a really good move. But it took to long to calculate the right position because it the time will almost go quadratic. 3 is now a good value for performance and time. If I did it to 2 it was fast but some moves are not that good. So I could win very easy. Now I still could win but there is more losing games then wins in it. There are also still some stupid moves that it could be a tie.

## Conclusion
## Sources
