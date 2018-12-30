var across = 12;

var up = 25;

var gameMatrix;

var colourMatrix;

var tempMatrix;

var tempColourMatrix;

var nextSelection;

var currentSelection;

var current;

var flag = 0;

var score = 0;

var startTime;

var currentTime;

var timeDifference;

var seconds;

var runGame;

var runSeconds;

var gameCanvas;

var initialTime = 475;

var gameOver = false;

var counters = [0];

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

document.onkeydown = function (event) {
	if (event.keyCode === 68) //d
		flag = 1;
	else if (event.keyCode === 83) //s
		flag = 2;
	else if (event.keyCode === 65) //a
		flag = 3;
	else if (event.keyCode === 69) //e
		flag = 4;
	else if (event.keyCode === 32) //Space
		flag = 5;
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

var CreateMatrixes = function () {
	gameMatrix = [];
	colourMatrix = [];
	tempMatrix = [];
	tempColourMatrix = [];
	for (var i = 0; i < across; i++) {
		gameMatrix[i] = [];
		colourMatrix[i] = [];
		tempMatrix[i] = [];
		tempColourMatrix[i] = [];
		for (var j = 0; j < up; j++) {
			gameMatrix[i][j] = 0;
			colourMatrix[i][j] = 'white';
			tempMatrix[i][j] = 0;
			tempColourMatrix[i][j] = 'white';
		}
	}
};

var ClearMatrixes = function () {
	for (var i = 0; i < across; i++) {
		for (var j = 0; j < up; j++) {
			gameMatrix[i][j] = 0;
			colourMatrix[i][j] = 'white';
			tempMatrix[i][j] = 0;
			tempColourMatrix[i][j] = 'white';
		}
	}
};

var ClearTempMatrixes = function () {
	for (var i = 0; i < across; i++) {
		for (var j = 0; j < up; j++) {
			tempMatrix[i][j] = 0;
			tempColourMatrix[i][j] = 'white';
		}
	}
};

var CheckForRows = function (matrix, colourMat) {
	for (var j = up - 2; j > 0; j--) {
		var can = true;
		for (var i = 1; i < across - 1; i++) {
			if (matrix[i][j] != 1) {
				can = false;
				break;
			}
		}
		if (can) {
			DeleteRow(j, matrix, colourMat);
			j++;
		}
	}
};

var DeleteRow = function (index, matrix, colourMat) {
	for (var i = 1; i < across - 1; i++) {
		matrix[i][index] = 0;
		colourMat[i][index] = 'white';
	}
	DropRows(index, matrix, colourMat);
	score += 1;
	document.getElementById("scoreText").innerHTML = "Score " + score;
};

var DropRows = function (index, matrix, colourMat) {
	for (var j = index; j > 0; j--) {
		for (var i = 1; i < across - 1; i++) {
			matrix[i][j] = matrix[i][j - 1];
			colourMat[i][j] = colourMat[i][j - 1];
		}
	}
};

var CheckForGameOver = function () {
	if (current.y < 0) {
		document.getElementById("gameoverText").innerHTML = "Game is over";

		document.getElementById("status").innerHTML = "Default";

		ClearSetIntervals();

		return true;
	}
	return false;
};

var ClearSetIntervals = function () {
	clearInterval(runGame);

	clearInterval(runSeconds);
};

var FlipBoard = function () {
	var i, j;
	for (j = up - 1; j > 0; j--) {
		for (i = 1; i < across - 1; i++) {
			tempMatrix[i][Math.abs(up - 1 - j)] = gameMatrix[i][j];
			tempColourMatrix[i][Math.abs(up - 1 - j)] = colourMatrix[i][j];
		}
	}

	for (i = 1; i < across - 1; i++) {
		tempMatrix[i][0] = 0;
		tempColourMatrix[i][0] = 'white';
	}

	var amount = 0;
	for (j = 0; j < up - 2; j++) {
		var seen = false;
		for (i = 1; i < across - 1; i++) {
			if (gameMatrix[i][j] === 1) {
				seen = true;
				break;
			}
		}
		if (!seen) {
			amount++;
		} else {
			break;
		}
	}
	amount--;
	console.log(amount);
	while (amount > 0) {
		DropRows(23, tempMatrix, tempColourMatrix);
		amount--;
	}

	for (j = up - 1; j > 0; j--) {
		for (i = 1; i < across - 1; i++) {
			gameMatrix[i][j] = tempMatrix[i][j];
			colourMatrix[i][j] = tempColourMatrix[i][j];
		}
	}

	for (i = 1; i < across - 1; i++) {
		gameMatrix[i][24] = 1;
		colourMatrix[i][0] = '#646464'; //Grey
	}

	ClearTempMatrixes();
	document.getElementById("status").innerHTML = "Game Flipped";
};

var LeanRight = function () {
	var i, j;
	for (j = 0; j < up - 1; j++) {
		for (i = 1; i < across - 1; i++) {
			tempMatrix[i][j] = gameMatrix[i][j];
			tempColourMatrix[i][j] = colourMatrix[i][j];
		}
	}
	for (j = 0; j < up - 1; j++) {
		for (i = across - 2; i > 1; i--) {
			var amount = 1;
			while (tempMatrix[i][j] === 0) {
				tempMatrix[i][j] = tempMatrix[i - amount][j];
				tempColourMatrix[i][j] = tempColourMatrix[i - amount][j];
				tempMatrix[i - amount][j] = 0;
				tempColourMatrix[i - amount][j] = 'white';
				amount++;
				if (amount - i === 0) {
					break;
				}
			}
		}
	}
	for (j = 0; j < up - 1; j++) {
		for (i = 1; i < across - 1; i++) {
			gameMatrix[i][j] = tempMatrix[i][j];
			colourMatrix[i][j] = tempColourMatrix[i][j];
		}
	}
	ClearTempMatrixes();
	document.getElementById("status").innerHTML = "Lean Right";
};

var LeanLeft = function () {
	var i, j;
	for (j = 0; j < up - 1; j++) {
		for (i = 1; i < across - 1; i++) {
			tempMatrix[i][j] = gameMatrix[i][j];
			tempColourMatrix[i][j] = colourMatrix[i][j];
		}
	}
	for (j = 0; j < up - 1; j++) {
		for (i = 1; i < across - 2; i++) {
			var amount = 1;
			while (tempMatrix[i][j] === 0) {
				tempMatrix[i][j] = tempMatrix[i + amount][j];
				tempColourMatrix[i][j] = tempColourMatrix[i + amount][j];
				tempMatrix[i + amount][j] = 0;
				tempColourMatrix[i + amount][j] = 'white';
				amount++;
				if (i + amount === across - 1) {
					break;
				}
			}
		}
	}
	for (j = 0; j < up - 1; j++) {
		for (i = 1; i < across - 1; i++) {
			gameMatrix[i][j] = tempMatrix[i][j];
			colourMatrix[i][j] = tempColourMatrix[i][j];
		}
	}
	ClearTempMatrixes();
	document.getElementById("status").innerHTML = "Lean Left";
};

var Jumble = function () {
	var i, j;
	for (j = 0; j < up - 1; j++) {
		for (i = 1; i < across - 1; i++) {
			tempMatrix[i][j] = gameMatrix[i][j];
			tempColourMatrix[i][j] = colourMatrix[i][j];
		}
	}

	for (j = 0; j < up - 1; j++) {
		for (i = 1; i < across - 1; i++) {
			var selection = Math.floor(Math.random() * (across - 2)) + 1;
			var tempValue = tempMatrix[selection][j];
			var tempColour = tempColourMatrix[selection][j];
			tempMatrix[selection][j] = tempMatrix[i][j];
			tempColourMatrix[selection][j] = tempColourMatrix[i][j];
			tempMatrix[i][j] = tempValue;
			tempColourMatrix[i][j] = tempColour;
		}
	}
	for (j = 0; j < up - 1; j++) {
		for (i = 1; i < across - 1; i++) {
			gameMatrix[i][j] = tempMatrix[i][j];
			colourMatrix[i][j] = tempColourMatrix[i][j];
		}
	}
	ClearTempMatrixes();
	document.getElementById("status").innerHTML = "Game Jumbled";
};

var Mirror = function () {
	var i, j;
	for (j = 0; j < up - 1; j++) {
		for (i = 1; i < across - 1; i++) {
			tempMatrix[i][j] = gameMatrix[i][j];
			tempColourMatrix[i][j] = colourMatrix[i][j];
		}
	}

	for (j = 0; j < up - 1; j++) {
		for (i = 1; i < Math.round((across / 2)); i++) {
			var tempValue = tempMatrix[across - i - 1][j];
			var tempColour = tempColourMatrix[across - i - 1][j];
			tempMatrix[across - i - 1][j] = tempMatrix[i][j];
			tempColourMatrix[across - i - 1][j] = tempColourMatrix[i][j];
			tempMatrix[i][j] = tempValue;
			tempColourMatrix[i][j] = tempColour;
		}
	}

	for (j = 0; j < up - 1; j++) {
		for (i = 1; i < across - 1; i++) {
			gameMatrix[i][j] = tempMatrix[i][j];
			colourMatrix[i][j] = tempColourMatrix[i][j];
		}
	}
	ClearTempMatrixes();
	document.getElementById("status").innerHTML = "Game Mirrored";
};

var ChangeNext = function () {
	currentSelection = nextSelection;
	nextSelection = blocks[Math.floor(Math.random() * blocks.length)];
	next = Block;
	next.init(nextSelection(), gameCanvas.blocksize);
	gameCanvas.ClearNext();
	next.DrawPreview(gameCanvas.nextContext);
	current = Block;
	current.init(currentSelection(), gameCanvas.blocksize);
	document.getElementById("status").innerHTML = "Next Swapped Out";
};

var mixers = [ChangeNext, Mirror, Jumble, LeanLeft, LeanRight, FlipBoard];

var SendScore = function () {
	if (score > 2 && gameOver) {
		var enteredName = document.getElementById("name").value;
		enteredName = enteredName.replace(/[^a-zA-Z ]/g, "");
		enteredName = enteredName.replace(/fuck|ass*|nig|shit|cunt|fag*|/gi, ""); //Replace bad words
		enteredName = enteredName.substring(0, 20);
		if (enteredName.length === 0) {
			enteredName = "Anon";
		}

		var dateObj = new Date();
		var month = dateObj.getUTCMonth() + 1; //months from 1-12
		var day = dateObj.getUTCDate();
		var year = dateObj.getUTCFullYear();

		newdate = year + "/" + month + "/" + day;

		if (seconds === 0) {
			alert("Seconds is equal to zero didn't insert data!");
			return;
		}

		if (year === 0) {
			alert("Year is equal to zero didn't insert data!");
			return;
		}

		if (month === 0) {
			alert("month is equal to zero didn't insert data!");
			return;
		}

		if (day === 0) {
			alert("day is equal to zero didn't insert data!");
			return;
		}

		if (score === 0) {
			alert("score is equal to zero didn't insert data!");
			return;
		}

		var socket = io();
		socket.emit('submitScore', {
			enteredName,
			seconds,
			score,
			newdate
		});
		alert("success");
		socket.emit('disconnect');
		socket = undefined;

		score = 0;
		seconds = 0;

	} else {
		alert("Need a score of 3 or greater and game must be over");
	}

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

	DropDown: function () {
		var can = true;
		for (var i = 0; i < this.state.length; i++) {
			for (var j = 0; j < this.state[i].length; j++) {
				if (gameMatrix[this.x + i][this.y + 1 + j] === 1 && this.state[i][j] === 1) {
					can = false;
				}
			}
		}
		if (can) {
			this.y += 1;
			return false;
		} else {
			this.CopyTo();
			return true;
		}
	},

	GoLeft: function () {
		var can = true;
		for (var i = 0; i < this.state.length; i++) {
			for (var j = 0; j < this.state[i].length; j++) {
				if (gameMatrix[this.x - 1 + i][this.y + j] === 1 && this.state[i][j] === 1) {
					can = false;
				}
			}
		}
		if (can) {
			this.x -= 1;
		}
	},

	GoRight: function () {
		var can = true;
		for (var i = 0; i < this.state.length; i++) {
			for (var j = 0; j < this.state[i].length; j++) {
				if (gameMatrix[this.x + 1 + i][this.y + j] === 1 && this.state[i][j] === 1) {
					can = false;
				}
			}
		}
		if (can) {
			this.x += 1;
		}
	},

	RotateShape: function () {
		var can = true;
		var temp = this.stateIndex + 1;
		if (temp >= this.states.length) {
			temp = 0;
		}
		var newState = this.states[temp];
		for (var i = 0; i < newState.length; i++) {
			for (var j = 0; j < newState[i].length; j++) {
				if (gameMatrix[this.x + i][this.y + j] === 1 && newState[i][j] === 1) {
					can = false;
				}
			}
		}
		if (can) {
			this.stateIndex = temp;
			this.state = this.states[temp];
		}
	},

	DrawShape: function (context) {
		for (var i = 0; i < this.state.length; i++) {
			for (var j = 0; j < this.state[i].length; j++) {
				if (this.state[i][j] === 1) {
					var x = this.blocksize * (this.x + i);
					var y = this.blocksize * (this.y + j);
					DrawBox(x, y, this.colour, context, this.blocksize);
				}
			}
		}
	},

	CopyTo: function () {
		for (var i = 0; i < this.state.length; i++) {
			for (var j = 0; j < this.state[i].length; j++) {
				if (this.state[i][j] === 1) {
					gameMatrix[this.x + i][this.y + j] = 1;
					colourMatrix[this.x + i][this.y + j] = this.colour;
				}
			}
		}
	},

	GoToBottom: function () {
		var status = false;
		while (!status) {
			status = this.DropDown();
		}
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

var run = function () {
	var tetrisCanvas = document.getElementById('tetrisCanvas');

	var previewCanvas = document.getElementById('previewCanvas');

	CreateMatrixes();

	gameCanvas = TetrisCanavas;

	gameCanvas.init(tetrisCanvas, previewCanvas);

	gameCanvas.ResetGame();

	nextSelection = blocks[Math.floor(Math.random() * blocks.length)];

	next = Block;

	next.init(nextSelection(), gameCanvas.blocksize);

	next.DrawPreview(gameCanvas.nextContext);

	startTime = new Date();

	runSeconds = setInterval(UpdateTime, 1000);

	runGame = setInterval(UpdateGame, initialTime);
};

var UpdateTime = function () {
	currentTime = new Date();

	timeDifference = (currentTime - startTime) / 1000;

	seconds = Math.round(timeDifference);

	document.getElementById("timeText").innerHTML = "Time " + seconds;
};

var UpdateGame = function () {
	if (!gameOver) {
		if (current == undefined) {
			currentSelection = nextSelection;
			nextSelection = blocks[Math.floor(Math.random() * blocks.length)];
			next = Block;
			next.init(nextSelection(), gameCanvas.blocksize);
			gameCanvas.ClearNext();
			next.DrawPreview(gameCanvas.nextContext);
			current = Block;
			current.init(currentSelection(), gameCanvas.blocksize);
		}

		switch (flag) {
		case 1:
			current.GoRight();
			flag = 0;
			break;
		case 2:
			current.DropDown();
			flag = 0;
			break;
		case 3:
			current.GoLeft();
			flag = 0;
			break;
		case 4:
			current.RotateShape();
			flag = 0;
			break;
		case 5:
			current.GoToBottom();
			flag = 0;
			break;
		}

		var result = current.DropDown();

		gameCanvas.ClearGameArea();
		gameCanvas.DrawGame();
		current.DrawShape(gameCanvas.gameContext);

		if (result) {
			CheckForRows(gameMatrix, colourMatrix);
			CheckForGameOver();
			current = undefined;
			var count = Math.floor(seconds / 25);
			if (!counters.includes(count)) {
				counters.push(count);
				var randomSelection = mixers[Math.floor(Math.random() * mixers.length)];
				randomSelection();
			}
		}
	}
};

var StartOver = function () {

	ClearSetIntervals();

	counters = [0];

	seconds = 0;

	score = 0;

	document.getElementById("timeText").innerHTML = "Time " + seconds;

	document.getElementById("gameoverText").innerHTML = "Game is not over";

	document.getElementById("scoreText").innerHTML = "Score " + score;

	document.getElementById("status").innerHTML = "Default";

	current = undefined;

	run();

	gameOver = false;
};

var EndGame = function () {
	ClearSetIntervals();
	gameCanvas.ClearGameArea();
	gameCanvas.DrawGame();
	document.getElementById("gameoverText").innerHTML = "Game is over";
	document.getElementById("status").innerHTML = "Default";
	current = undefined;
	gameOver = true;
};