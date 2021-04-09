function range(start, end) {
	let array = [];
	for(let i=start; i<=end; i++) {
		array.push(i);
	}
	return array;
}

console.log(range(1,10))

function sum(array) {
	let total = 0;
	for (let i = 0; i< array.length; i++) {
		total += array[i];
	}
	return total;
}

console.log(sum(range(1, 10)));

function reverseArray(array) {
	let newArray = [];
	let currentIndex = 0;
	for (let i = array.length-1; i >= 0; i--) {
		newArray[currentIndex] = array[i];
		currentIndex++;
	}
	return newArray;
}

console.log(reverseArray(["A", "B", "C"]));

function reverseInPlace(array) {
	for (let i=0; i< Math.round(array.length/2); i++) {
		let old = array[i];
		let newIndex = array.length - i - 1;
		array[i] = array[newIndex];
		array[newIndex] = old;
	}
}

let arrayV = [1,2,3,4,5];
reverseInPlace(arrayV);
console.log(arrayV);

function arrayToList(array) {

}

function listToArray(list) {
	let array = [];
	return array;
}

function prepend(list, value) {
	return list;
}

function nth(list, index) {
	return 0;
}