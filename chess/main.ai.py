#information https://medium.com/free-code-camp/simple-chess-ai-step-by-step-1d55a9266977
# https://python-chess.readthedocs.io/en/latest/core.html#
import chess
import helpers
board = chess.Board()

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
		bestMove = helpers.miniMaxRootAlpha(3, board, True)
		print("Computer did move: " + bestMove.uci())
		board.push(bestMove)

if (board.turn):
	print("Black won")
else:
	print("White won")
