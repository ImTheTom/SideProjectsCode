#information https://medium.com/free-code-camp/simple-chess-ai-step-by-step-1d55a9266977
# https://python-chess.readthedocs.io/en/latest/core.html#
# https://medium.com/applied-data-science/how-to-build-your-own-muzero-in-python-f77d5718061a
import chess
import helpers
board = chess.Board()

while(not board.is_game_over()):
	print(board)
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

if (board.turn):
	print("Black won")
else:
	print("White won")
