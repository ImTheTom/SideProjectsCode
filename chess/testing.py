import chess
import helpers
board = chess.Board()
bestMove = helpers.miniMaxRootAlpha(3, board, True)
print(bestMove)
helpers.toArray(board)
print(board)
