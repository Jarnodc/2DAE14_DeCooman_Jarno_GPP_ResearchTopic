# Chess AI BOT
A bot that plays [Chess](https://en.wikipedia.org/wiki/Chess) using AI [Minimax](https://en.wikipedia.org/wiki/Minimax).
![image](https://user-images.githubusercontent.com/70661716/150417511-cb5ed31b-2c05-4915-9e5c-329044622c54.png)

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
In the beginning, I tried to make my MiniMax function an iterative function. So I made a for loop till I reached my max depth. This was very difficult to do because there was a lot of variables I had to store. For performance, it was also a bad idea because you needed more memory to store these variables and he checked to much times a useless move. I also didn't use those beta and alpha variables. This was also stupid to do because I need to write more then needed because when you call yourself, you need to write it once.

The depth is now 3 deep and I first checked it with 5 or 4 because normally then you could get a really good move. But it took to long to calculate the right position because it the time will almost go quadratic. 3 is now a good value for performance and time. If I did it to 2 it was fast but some moves are not that good. So I could win very easy. Now I still could win but there is more losing games then wins in it. There are also still some stupid moves that it could be a tie. When I checked how many checks he did, there was, with a depth of 3, already an average of 12.000 checks. With a depth of 4 there is an average of 300.000 checks. 

<img src="https://user-images.githubusercontent.com/70661716/150187535-871b9052-1a3f-4f5f-a87d-63fb17131282.png" width="25%" img align="right">
Sometimes the AI can't calculate the right position and stays in an infinity loop or do the last move again an again so you couldn't win and he either. For example in the image below. I made a move with my pawn. And before I did a move with my knight and he moved his rook. Now he doesn't know what the right move is. Because he will check all the position and no matter which knight he will took, he is dead with his rook but those 2 knights always give each other a position. So the smartest thing to do is stay in this position and with the pawn is it the same. So he will check for the king and that is always a useless move so he wouldn't move that either.

I can't make the AI play to another AI because then the game ends instantly. The reason for this, is that it calculates his best move at the movement the other player just moved his piece. So it responds on a move of the other player. 

## Conclusion
When you think about performance, this AI is not the perfect algorithm to play chess. This is because you need to check every position no matter you optimize it. There are still a lot checks it needs to do. But for a beginning AI programmer this is not a mind blowing algorithm because there is still a logic formula you can see and understand and it works. To make a better and faster AI, I think you can try it with a deep learning algorithm where he will learn what the best move is in a specific location where he doesn't need to calculate a lot of moves.

Overall this project has some issues but in most cases it will do it job. It will do some very good and smart moves but when it plays to a decent player, these moves aren't enough to win. I could make it better with this algorithm but that will cost the AI a lot of time to calculate and that is not fun for the other player. 

## Sources
 
