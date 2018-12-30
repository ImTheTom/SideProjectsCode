var across = 12;

var up = 25;

var gameMatrix;

var colourMatrix;

var flag = 0;

var score = 0;

var seconds = 0;

var gameCanvas;

var gameOver = false;

var socket;

var enteredName;

var nextSelection;

var next;

var Cube = function () {
	var self = {
		x: 5,
		y: -2,
		colour: '#ff0000', //Red
		states: [
			[
				[1, 1],
				[1, 1]
			]
		]
	};
	return self;
};

var ZBlock = function () {
	var self = {
		x: 5,
		y: -2,
		colour: '#0000ff', //Blue
		states: [
			[
				[1, 0],
				[1, 1],
				[0, 1]
			],
			[
				[0, 1, 1],
				[1, 1, 0]
			],
			[
				[0, 1],
				[1, 1],
				[1, 0]
			],
			[
				[1, 1, 0],
				[0, 1, 1]
			]
		]
	};
	return self;
};

var ZBlock2 = function () {
	var self = {
		x: 5,
		y: -2,
		colour: '#00ff00', //Green
		states: [
			[
				[0, 1],
				[1, 1],
				[1, 0]
			],
			[
				[1, 1, 0],
				[0, 1, 1]
			],
			[
				[1, 0],
				[1, 1],
				[0, 1]
			],
			[
				[0, 1, 1],
				[1, 1, 0]
			]
		]
	};
	return self;
};

var TBlock = function () {
	var self = {
		x: 5,
		y: -2,
		colour: '#f44179', //Violet red or dark pink
		states: [
			[
				[0, 1],
				[1, 1],
				[0, 1]
			],
			[
				[1, 1, 1],
				[0, 1, 0]
			],
			[
				[1, 0],
				[1, 1],
				[1, 0]
			],
			[
				[0, 1, 0],
				[1, 1, 1]
			]
		]
	};
	return self;
};

var LineBlock = function () {
	var self = {
		x: 5,
		y: -4,
		colour: '#00ffff', //Cyan
		states: [
			[
				[1, 1, 1, 1]
			],
			[
				[1],
				[1],
				[1],
				[1]
			]
		]
	};
	return self;
};

var LBlock = function () {
	var self = {
		x: 5,
		y: -2,
		colour: '#ac41f4', // purple
		states: [
			[
				[1, 1],
				[0, 1],
				[0, 1]
			],
			[
				[0, 0, 1],
				[1, 1, 1]
			],
			[
				[1, 0],
				[1, 0],
				[1, 1]
			],
			[
				[1, 1, 1],
				[1, 0, 0]
			]
		]
	};
	return self;
};

var LBlock2 = function () {
	var self = {
		x: 5,
		y: -2,
		colour: '#ff6e00', // Orange
		states: [
			[
				[0, 1],
				[0, 1],
				[1, 1]
			],
			[
				[1, 0, 0],
				[1, 1, 1]
			],
			[
				[1, 1],
				[1, 0],
				[1, 0]
			],
			[
				[1, 1, 1],
				[0, 0, 1]
			]
		]
	};
	return self;
};

var blocks = [Cube, ZBlock, ZBlock2, TBlock, LineBlock, LBlock, LBlock2];

var Block = {
	init: function (shape, size) {
		this.x = shape.x;
		this.y = shape.y;
		this.stateIndex = 0;
		this.state = shape.states[0];
		this.states = shape.states;
		this.colour = shape.colour;
		this.blocksize = size;
	},

	DrawPreview: function (context) {
		context.beginPath();
		var shapeState = this.state;
		for (var i = 0; i < shapeState.length; i++) {
			for (var j = 0; j < shapeState[i].length; j++) {
				if (shapeState[i][j] === 1) {
					var x = this.blocksize * (i + this.x / 4);
					var y = this.blocksize * (j + 2 + (this.y / 2));
					DrawBox(x, y, this.colour, context, this.blocksize);
				}
			}
		}
		context.closePath();
	}
};

//socket.emit('keyPress',1);

document.onkeydown = function (event) {
	if (event.keyCode === 68) //d
		flag = 1;
	else if (event.keyCode === 83) //s
		flag = 2;
	else if (event.keyCode === 65) //a
		flag = 3;
	else if (event.keyCode === 69) //e
		flag = 4;
};

var CreateMatrixes = function () {
	gameMatrix = [];
	colourMatrix = [];
	for (var i = 0; i < across; i++) {
		gameMatrix[i] = [];
		colourMatrix[i] = [];
		for (var j = 0; j < up; j++) {
			gameMatrix[i][j] = 0;
			colourMatrix[i][j] = 'white';
		}
	}
};

var ClearMatrixes = function () {
	for (var i = 0; i < across; i++) {
		for (var j = 0; j < up; j++) {
			gameMatrix[i][j] = 0;
			colourMatrix[i][j] = 'white';
		}
	}
};

function DrawBox(x, y, colour, context, size) {
	if (y < 0) {
		return;
	}
	context.beginPath();
	context.rect(x, y, size, size);
	context.fillStyle = colour;
	context.fill();
	context.lineWidth = 1;
	context.stroke();
	context.closePath();
}

var SetUp = function () {
	var tetrisCanvas = document.getElementById('tetrisCanvas');

	var previewCanvas = document.getElementById('previewCanvas');

	CreateMatrixes();

	gameCanvas = TetrisCanavas;

	gameCanvas.init(tetrisCanvas, previewCanvas);

	gameCanvas.ResetGame();

	gameCanvas.DrawGame();
};

var ConnectToServer = function () {
	if (socket === undefined) {
		socket = io();

		socket.on('loginStatus', function (data) {
			if (data.status) {
				document.getElementById("connectionStatus").innerHTML = "Connected";
			} else {
				document.getElementById("connectionStatus").innerHTML = "Not Connected";
			}
		});

		socket.on('newMatrix', function (data) {
			gameMatrix = data.gameMatrix;
			colourMatrix = data.colourMatrix;
			gameCanvas.DrawGame();
		});

		socket.on('newScore', function (data) {
			score = data.score;
			document.getElementById("scoreText").innerHTML = "Score " + score;
		});

		socket.on('newSeconds', function (data) {
			seconds = data.seconds;
			document.getElementById("timeText").innerHTML = "Time " + seconds;
		});

		socket.on('newStatus', function (data) {
			gameOver = data.gameOver;
			if (gameOver) {
				document.getElementById("gameoverText").innerHTML = "Game is over";

				document.getElementById("status").innerHTML = "Default";
			} else {
				document.getElementById("gameoverText").innerHTML = "Game is not over";
			}
		});

		socket.on('newNext', function (data) {
			nextSelection = data.nextSelection;

			var nextBlockSelection = blocks[nextSelection];

			next = Block;

			next.init(nextBlockSelection(), gameCanvas.blocksize);

			gameCanvas.ClearNext();

			next.DrawPreview(gameCanvas.nextContext);
		});

		socket.on('newMixer', function (data) {
			var stat = data.status;
			document.getElementById("status").innerHTML = stat;
		});

	}
};

var Restart = function () {
	socket.emit('restart');
};

var UpdateInfo = function () {

	var nextBlockSelection = blocks[nextSelection];

	next = Block;

	next.init(nextBlockSelection(), gameCanvas.blocksize);

	gameCanvas.ClearNext();

	next.DrawPreview(gameCanvas.nextContext);
};


var TetrisCanavas = {
	init: function (game, next) {
		this.game = game;
		this.next = next;
		this.gameContext = game.getContext('2d');
		this.nextContext = next.getContext('2d');
		this.blocksize = game.width / across;
	},

	ClearGame: function () {
		this.gameContext.clearRect(0, 0, this.game.width, this.game.height);
	},

	ClearNext: function () {
		this.nextContext.clearRect(0, 0, this.game.width, this.game.height);
	},

	DrawWalls: function () {
		this.gameContext.beginPath();
		this.gameContext.fillStyle = '#646464'; //grey
		for (var i = 0; i < up; i++) {
			this.gameContext.rect(0, i * this.blocksize, this.blocksize, this.blocksize);
			this.gameContext.fill();
			this.gameContext.stroke();
			gameMatrix[0][i] = 1;
			colourMatrix[0][i] = '#646464'; //Grey
		}
		for (i = 0; i < up; i++) {
			this.gameContext.rect(this.game.width - this.blocksize, i * this.blocksize, this.blocksize, this.blocksize);
			this.gameContext.fill();
			this.gameContext.stroke();
			gameMatrix[across - 1][i] = 1;
			colourMatrix[across - 1][i] = '#646464'; //Grey
		}
		for (i = 0; i < across; i++) {
			this.gameContext.rect(i * this.blocksize, this.game.height - this.blocksize, this.blocksize, this.blocksize);
			this.gameContext.fill();
			this.gameContext.stroke();
			gameMatrix[i][up - 1] = 1;
			colourMatrix[i][up - 1] = '#646464'; //Grey
		}
		this.gameContext.closePath();
	},

	DrawGrid: function () {
		for (var x = 1; x < across - 1; x++) {
			for (var y = 0; y < up - 1; y++) {
				DrawBox(x * this.blocksize, y * this.blocksize, 'white', this.gameContext, this.blocksize);
			}
		}
	},

	ClearGameArea: function () {
		this.gameContext.clearRect(this.blocksize, 0, (across - 2) * this.blocksize, (up - 1) * this.blocksize);
	},

	DrawGame: function () {
		this.gameContext.beginPath();
		for (var x = 1; x < across - 1; x++) {
			for (var y = 0; y < up - 1; y++) {
				if (gameMatrix[x][y] === 0) {
					DrawBox(x * this.blocksize, y * this.blocksize, 'white', this.gameContext, this.blocksize);
				} else {
					DrawBox(x * this.blocksize, y * this.blocksize, colourMatrix[x][y], this.gameContext, this.blocksize);
				}
			}
		}
		this.gameContext.closePath();
	},

	ResetGame: function () {
		ClearMatrixes();
		this.ClearGame();
		this.ClearNext();
		this.DrawWalls();
		this.DrawGrid();
	}

};

setInterval(function () {
	if (socket != undefined) {
		socket.emit('keyPress', flag);
		flag = 0;
	}
}, 425);