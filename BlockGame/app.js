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

var initialTime = 425;

var gameOver = false;

var counters = [0];

var next;

var flag = 0;

var mysql = require('mysql');

var express = require('express');

var app = express();

var serv = require('http').Server(app);

app.get('/', function (req, res) {
	res.sendFile(__dirname + '/client/home.html');
});

app.get('/singleplayer', function (req, res) {
	res.sendFile(__dirname + '/client/singleplayer.html');
});

app.get('/multiplayer', function (req, res) {
	res.sendFile(__dirname + '/client/multiplayer.html');
});

app.get('/highscores', function (req, res) {
	res.sendFile(__dirname + '/client/highscores.html');
});

app.use('/client', express.static(__dirname + '/client'));

serv.listen(3675);

console.log("Server started.");

var SOCKET_LIST = {};

var io = require('socket.io')(serv, {});

io.sockets.on('connection', function (socket) {
	socket.id = Math.random();
	SOCKET_LIST[socket.id] = socket;

	socket.emit('loginStatus', {
		status: true
	});
	EmitNext();

	socket.on('restart', function () {
		if (gameOver) {
			ClearSetIntervals();

			counters = [0];

			seconds = 0;

			score = 0;

			current = undefined;

			gameOver = false;

			EmitMixer("Default");

			EmitScore();

			EmitScore();

			EmitGameOver();

			run();
		}
	});

	socket.on('requestLatest', function (data) {
		var con = mysql.createConnection({
			host: "localhost",
			user: "root",
			password: "test",
			database: "blockgame"
		});
		con.connect(function (err) {
			if (err) throw err;
			con.query("SELECT * FROM scores ORDER BY idscores DESC LIMIT 1", function (err, result, fields) {
				if (err) throw err;
				result = result[0];
				socket.emit('sendLatest', {
					result
				});
			});
		});
	});

	socket.on('requestTable', function (data) {
		var con = mysql.createConnection({
			host: "localhost",
			user: "root",
			password: "test",
			database: "blockgame"
		});
		con.connect(function (err) {
			if (err) throw err;
			con.query("SELECT * FROM scores ORDER BY score DESC LIMIT 10", function (err, result, fields) {
				if (err) throw err;
				socket.emit('sendTable', {
					result
				});
			});
		});
	});

	socket.on('submitScore', function (data) {
		var con = mysql.createConnection({
			host: "localhost",
			user: "root",
			password: "test",
			database: "blockgame"
		});

		var enteredName = data.enteredName;
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

		if (data.seconds === 0) {
			return;
		}
		if (data.score === 0) {
			return;
		}

		con.connect(function (err) {
			if (err) throw err;
			var statement = "'" + enteredName + "'," + data.score + ",'" + data.seconds + "','" + newdate + "')";
			var sql = "INSERT INTO scores (name, score,seconds,date) VALUES (" + statement;
			console.log(sql);
			con.query(sql, function (err, result) {
				if (err) throw err;
				console.log("1 record inserted");
			});
		});

	});

	socket.on('disconnect', function () {
		console.log("Connection terminated");
		delete SOCKET_LIST[socket.id];
	});

	socket.on('keyPress', function (data) {
		flag = data;
		if (current != undefined) {
			switch (flag) {
			case 1:
				current.GoRight();
				break;
			case 2:
				current.DropDown();
				break;
			case 3:
				current.GoLeft();
				break;
			case 4:
				current.RotateShape();
				break;
			}
			flag = 0;
		}
	});
});

setInterval(function () {
	if (current != undefined) {
		current.CopyTo();
	}
	for (var i in SOCKET_LIST) {
		var socket = SOCKET_LIST[i];
		socket.emit('newMatrix', {
			gameMatrix,
			colourMatrix
		});
	}
	if (current != undefined) {
		current.RemoveFromMatrix();
	}
	flag = 0;
}, 425);

var EmitScore = function () {
	for (var i in SOCKET_LIST) {
		var socket = SOCKET_LIST[i];
		socket.emit('newScore', {
			score
		});
	}
};

var EmitSeconds = function () {
	for (var i in SOCKET_LIST) {
		var socket = SOCKET_LIST[i];
		socket.emit('newSeconds', {
			seconds
		});
	}
};

var EmitNext = function () {
	for (var i in SOCKET_LIST) {
		var socket = SOCKET_LIST[i];
		socket.emit('newNext', {
			nextSelection
		});
	}
};

var EmitGameOver = function () {
	for (var i in SOCKET_LIST) {
		var socket = SOCKET_LIST[i];
		socket.emit('newStatus', {
			gameOver
		});
	}
};

var EmitMixer = function (status) {
	for (var i in SOCKET_LIST) {
		var socket = SOCKET_LIST[i];
		socket.emit('newMixer', {
			status
		});
	}
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
	EmitMixer("Game Flipped");
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
	EmitMixer("Lean Right");
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
	EmitMixer("Lean Left");
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
	EmitMixer("Game Jumbled");
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
	EmitMixer("Game Mirrored");
};

var ChangeNext = function () {
	currentSelection = blocks[nextSelection];

	nextSelection = Math.floor(Math.random() * blocks.length);

	current = Block;

	current.init(currentSelection());

	EmitNext();
};

var mixers = [ChangeNext, Mirror, Jumble, LeanLeft, LeanRight, FlipBoard];

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
	EmitScore();
};

var DropRows = function (index, matrix, colourMat) {
	for (var j = index; j > 0; j--) {
		for (var i = 1; i < across - 1; i++) {
			matrix[i][j] = matrix[i][j - 1];
			colourMat[i][j] = colourMat[i][j - 1];
		}
	}
};

var SetWalls = function () {
	for (var i = 0; i < up; i++) {
		gameMatrix[0][i] = 1;
		colourMatrix[0][i] = '#646464'; //Grey
	}
	for (i = 0; i < up; i++) {
		gameMatrix[across - 1][i] = 1;
		colourMatrix[across - 1][i] = '#646464'; //Grey
	}
	for (i = 0; i < across; i++) {
		gameMatrix[i][up - 1] = 1;
		colourMatrix[i][up - 1] = '#646464'; //Grey
	}
};

var Block = {
	init: function (shape) {
		this.x = shape.x;
		this.y = shape.y;
		this.stateIndex = 0;
		this.state = shape.states[0];
		this.states = shape.states;
		this.colour = shape.colour;
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

	RemoveFromMatrix: function () {
		for (var i = 0; i < this.state.length; i++) {
			for (var j = 0; j < this.state[i].length; j++) {
				if (this.state[i][j] === 1) {
					gameMatrix[this.x + i][this.y + j] = 0;
					colourMatrix[this.x + i][this.y + j] = 'white';
				}
			}
		}
	},
};

var CheckForGameOver = function () {
	if (current.y < 0) {

		gameOver = true;

		ClearSetIntervals();

		EmitGameOver();
	}
};

var ClearSetIntervals = function () {
	clearInterval(runGame);

	clearInterval(runSeconds);
};

var run = function () {
	CreateMatrixes();

	SetWalls();

	nextSelection = Math.floor(Math.random() * blocks.length);

	startTime = new Date();

	runSeconds = setInterval(UpdateTime, 1000);

	runGame = setInterval(UpdateGame, initialTime);
};

var UpdateTime = function () {
	currentTime = new Date();

	timeDifference = (currentTime - startTime) / 1000;

	seconds = Math.round(timeDifference);

	EmitSeconds();
};

var UpdateGame = function () {
	if (!gameOver) {
		if (current == undefined) {
			currentSelection = blocks[nextSelection];

			nextSelection = Math.floor(Math.random() * blocks.length);

			current = Block;

			current.init(currentSelection());

			EmitNext();
		}

		var result = current.DropDown();

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

run();