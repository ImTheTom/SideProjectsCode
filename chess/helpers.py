#https://jsfiddle.net/q76uzxwe/1/
import chess
import numpy as np
import math
import random

def miniMaxRoot(depth, board, isMaximisingPlayer):
	legalMoves = board.legal_moves
	tmpMoves = list(legalMoves)
	random.shuffle(tmpMoves)

	bestMove = -9999
	bestMoveFound = None

	for move in tmpMoves:
		board.push(move)
		value = minimax(depth - 1, board, not isMaximisingPlayer)
		board.pop()
		if(value >= bestMove):
			bestMove = value
			bestMoveFound = move

	return bestMoveFound

def minimax(depth, board, isMaximisingPlayer):
	if depth == 0:
		return calculateBoard(board)

	legalMoves = board.legal_moves
	tmpMoves = list(legalMoves)
	random.shuffle(tmpMoves)

	if (isMaximisingPlayer):
		bestMove = -9999
		for move in tmpMoves:
			board.push(move)
			bestMove = max(bestMove, minimax(depth - 1, board, not isMaximisingPlayer))
			board.pop()
		return bestMove
	else:
		bestMove = 9999
		for move in tmpMoves:
			board.push(move)
			bestMove = min(bestMove, minimax(depth - 1, board, not isMaximisingPlayer))
			board.pop()
		return bestMove

def calculateBoard(board):
	tmp = toArray(board)
	value = 0
	for x in range(len(tmp)):
		for y in range(len(tmp[x])):
			value = value + tmp[x][y]
	return -value

def getRandomMove(board):
	legalMoves = board.legal_moves
	numberOfMoves = legalMoves.count()
	if (numberOfMoves > 0):
		return list(legalMoves)[random.randint(0, numberOfMoves)]
	return None

def calculateBestMove(board):
	legalMoves = board.legal_moves
	tmpMoves = list(legalMoves)
	random.shuffle(tmpMoves)
	bestMove = None
	bestValue = -999
	for move in tmpMoves:
		board.push(move)
		value = calculateBoard(board)
		board.pop()
		if (value > bestValue):
			bestValue = value
			bestMove = move
	return bestMove

def toArray(board):
	tmp = np.zeros((8,8))
	boardMap = board.piece_map()
	for loc, piece in boardMap.items():
		row = math.floor(loc / 8)
		column = (loc % 8)
		tmp[row, column] = toValue(piece)
	return tmp

def toValue(piece):
	if piece.piece_type == chess.PAWN:
		value = 10
	elif piece.piece_type == chess.BISHOP or piece.piece_type == chess.KNIGHT:
		value = 30
	elif piece.piece_type == chess.ROOK:
		value = 50
	elif piece.piece_type == chess.KING:
		value = 90
	elif piece.piece_type == chess.QUEEN:
		value = 900

	if piece.color == chess.BLACK:
		value = value * -1

	return value

def miniMaxRootAlpha(depth, board, isMaximisingPlayer):
	legalMoves = board.legal_moves
	tmpMoves = list(legalMoves)
	random.shuffle(tmpMoves)

	bestMove = -9999
	bestMoveFound = None

	for move in tmpMoves:
		board.push(move)
		value = minimaxAlpha(depth - 1, board, -10000, 10000, not isMaximisingPlayer)
		board.pop()
		if(value >= bestMove):
			bestMove = value
			bestMoveFound = move

	return bestMoveFound

def minimaxAlpha(depth, board, alpha, beta, isMaximisingPlayer):
	if depth == 0:
		return calculateBoardAlpha(board)

	legalMoves = board.legal_moves
	tmpMoves = list(legalMoves)
	random.shuffle(tmpMoves)

	if (isMaximisingPlayer):
		bestMove = -9999
		for move in tmpMoves:
			board.push(move)
			bestMove = max(bestMove, minimaxAlpha(depth-1, board, alpha, beta, isMaximisingPlayer))
			board.pop()
			alpha = max(alpha, bestMove)
			if (beta <= alpha):
				return bestMove
		return bestMove
	else:
		bestMove = 9999
		for move in tmpMoves:
			board.push(move)
			bestMove = min(bestMove, minimaxAlpha(depth-1, board, alpha, beta, isMaximisingPlayer))
			board.pop()
			beta = min(beta, bestMove)
			if (beta <= alpha):
				return bestMove
		return bestMove


def calculateBoardAlpha(board):
	tmp = toArrayAlpha(board)
	value = 0
	for x in range(len(tmp)):
		for y in range(len(tmp[x])):
			value = value + tmp[x][y]
	return -value


def toArrayAlpha(board):
	tmp = np.zeros((8,8))
	boardMap = board.piece_map()
	for loc, piece in boardMap.items():
		row = math.floor(loc / 8)
		column = (loc % 8)
		tmp[row, column] = toValueAlpha(piece, row, column)
	return tmp


def toValueAlpha(piece, row, column):
	if piece.piece_type == chess.PAWN:
		value = 10 + pawnWhiteValues()[row][column] if isWhite(piece) else pawnBlackValues()[row][column]
	elif piece.piece_type == chess.BISHOP:
		value = 30 + bishopWhiteValues()[row][column] if isWhite(piece) else bishopBlackValues()[row][column]
	elif piece.piece_type == chess.KNIGHT:
		value = 30 + knightValues()[row][column]
	elif piece.piece_type == chess.ROOK:
		value = 50 + rookWhiteValues()[row][column] if isWhite(piece) else rookBlackValues()[row][column]
	elif piece.piece_type == chess.KING:
		value = 90 + kingWhiteValues()[row][column] if isWhite(piece) else kingBlackValues()[row][column]
	elif piece.piece_type == chess.QUEEN:
		value = 900 + queenValues()[row][column]

	if piece.color == chess.BLACK:
		value = value * -1

	return value

def reverseNumpy(array):
	return np.flip(array, 0)

def isWhite(piece):
	return piece.color

def pawnWhiteValues():
	return np.array(
    	[
    	    [0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0],
    	    [5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0, 5.0],
    	    [1.0, 1.0, 2.0, 3.0, 3.0, 2.0, 1.0, 1.0],
    	    [0.5, 0.5, 1.0, 2.5, 2.5, 1.0, 0.5, 0.5],
    	    [0.0, 0.0, 0.0, 2.0, 2.0, 0.0, 0.0, 0.0],
    	    [0.5, -0.5, -1.0, 0.0, 0.0, -1.0, -0.5, 0.5],
    	    [0.5, 1.0, 1.0, -2.0, -2.0, 1.0, 1.0, 0.5],
    	    [0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0]
    	]
	)

def pawnBlackValues():
	return reverseNumpy(pawnWhiteValues())

def knightValues():
	return np.array(
            [
                [-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0],
                [-4.0, -2.0, 0.0, 0.0, 0.0, 0.0, -2.0, -4.0],
                [-3.0, 0.0, 1.0, 1.5, 1.5, 1.0, 0.0, -3.0],
                [-3.0, 0.5, 1.5, 2.0, 2.0, 1.5, 0.5, -3.0],
                [-3.0, 0.0, 1.5, 2.0, 2.0, 1.5, 0.0, -3.0],
                [-3.0, 0.5, 1.0, 1.5, 1.5, 1.0, 0.5, -3.0],
                [-4.0, -2.0, 0.0, 0.5, 0.5, 0.0, -2.0, -4.0],
                [-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0]
            ]
	)

def bishopWhiteValues():
	return np.array(
            [
                [-2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0],
                [-1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0],
                [-1.0, 0.0, 0.5, 1.0, 1.0, 0.5, 0.0, -1.0],
                [-1.0, 0.5, 0.5, 1.0, 1.0, 0.5, 0.5, -1.0],
                [-1.0, 0.0, 1.0, 1.0, 1.0, 1.0, 0.0, -1.0],
                [-1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, -1.0],
                [-1.0, 0.5, 0.0, 0.0, 0.0, 0.0, 0.5, -1.0],
                [-2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0]
            ]
	)


def bishopBlackValues():
	return reverseNumpy(bishopWhiteValues())

def rookWhiteValues():
	return np.array(
            [
                [0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0],
                [0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 0.5],
                [-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5],
                [-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5],
                [-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5],
                [-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5],
                [-0.5, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -0.5],
                [0.0, 0.0, 0.0, 0.5, 0.5, 0.0, 0.0, 0.0]
            ]
	)


def rookBlackValues():
	return reverseNumpy(rookWhiteValues())

def queenValues():
	return np.array(
            [
                [-2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0],
                [-1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0],
                [-1.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0],
                [-0.5, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5],
                [0.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5],
                [-1.0, 0.5, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0],
                [-1.0, 0.0, 0.5, 0.0, 0.0, 0.0, 0.0, -1.0],
                [-2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0]
            ]
	)

def kingWhiteValues():
	return np.array(
            [
                [-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0],
                [-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0],
                [-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0],
                [-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0],
                [-2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0],
                [-1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0],
                [2.0, 2.0, 0.0, 0.0, 0.0, 0.0, 2.0, 2.0],
                [2.0, 3.0, 1.0, 0.0, 0.0, 1.0, 3.0, 2.0]
            ]
	)


def kingBlackValues():
	return reverseNumpy(kingWhiteValues())
