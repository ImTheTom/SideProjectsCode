#information https://medium.com/free-code-camp/simple-chess-ai-step-by-step-1d55a9266977
# https://python-chess.readthedocs.io/en/latest/core.html#
import chess
import helpers
board = chess.Board()

RANDOM = "1"
EVALUATED_MOVE = "2"
MINIMAX = "3"

OPTIONS = [RANDOM, EVALUATED_MOVE, MINIMAX]

while(True):
	gameType = input("Computer Intelligence\n\t1. Random moves\n\t2. Evaluated 1 move ahead\n\t3. Minimax algorithm 3 depth\n")
	if gameType in OPTIONS:
		while(not board.is_game_over()):
			print(board.unicode(invert_color=True, borders=True))
			move = input("Enter your move: ")
			try:
				board.parse_uci(move)
				move = chess.Move.from_uci(move)
				if (move != chess.Move.null()):
					board.push(move)
				else:
					print("Move was equal to null could not perform")
			except:
				print("Could not perform that move")
			if(board.turn == chess.BLACK):
				if gameType == RANDOM:
					bestMove = helpers.getRandomMove(board)
				elif gameType == EVALUATED_MOVE:
					bestMove = helpers.calculateBestMove(board)
				elif gameType == MINIMAX:
					bestMove = helpers.miniMaxRootAlpha(3, board, True)
				board.push(bestMove)
				print("Computer did move: " + bestMove.uci())

		if (board.turn):
			print("Black won")
		else:
			print("White won")
		move = input("Enter to play again...")
	else:
		print("Sorry please select a valid option of 1, 2 or 3")

