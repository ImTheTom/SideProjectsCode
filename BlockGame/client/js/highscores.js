var latest;
var topscores;
var GetData = function () {
	var socket = io();

	socket.emit('requestLatest');

	socket.on('sendLatest', function (data) {
		latest = data;
		var statement = 'The latest result is from ' + latest.result.name + ' with a score of ' + latest.result.score +
			' in ' + latest.result.seconds + ' seconds on ' + latest.result.date + '.';
		document.getElementById("latest").innerHTML = statement;
	});

	socket.emit('requestTable');
	socket.on('sendTable', function (data) {
		topscores = data.result;
		console.log(topscores);
		var body = document.getElementsByTagName('body')[0];
		var tbl = document.createElement('table');
		var tbdy = document.createElement('tbody');
		var headingRow = document.createElement('tr');
		for (var k = 0; k < 5; k++) {
			var headerElement = document.createElement('td');
			switch (k) {
			case 0:
				headerElement.appendChild(document.createTextNode("Rank"));
				break;
			case 1:
				headerElement.appendChild(document.createTextNode("Name"));
				break;
			case 2:
				headerElement.appendChild(document.createTextNode("Score"));
				break;
			case 3:
				headerElement.appendChild(document.createTextNode("Seconds"));
				break;
			case 4:
				headerElement.appendChild(document.createTextNode("Date"));
				break;
			default:
				headerElement.appendChild(document.createTextNode('Error'));
			}
			headingRow.appendChild(headerElement);
		}
		tbdy.appendChild(headingRow);
		for (var i = 0; i < 10; i++) {
			var tr = document.createElement('tr');
			for (var j = 0; j < 5; j++) {
				var td = document.createElement('td');
				if (j === 0) {
					td.appendChild(document.createTextNode(i + 1));
				} else {
					switch (j) {
					case 1:
						td.appendChild(document.createTextNode(topscores[i].name));
						break;
					case 2:
						td.appendChild(document.createTextNode(topscores[i].score));
						break;
					case 3:
						td.appendChild(document.createTextNode(topscores[i].seconds));
						break;
					case 4:
						td.appendChild(document.createTextNode(topscores[i].date));
						break;
					default:
						td.appendChild(document.createTextNode('Error'));
					}
				}
				tr.appendChild(td);
			}
			tbdy.appendChild(tr);
		}
		tbl.appendChild(tbdy);
		body.appendChild(tbl)
	});

};