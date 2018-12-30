// This is the current total variable that is used to set the thumnails
var currentTotal = 0;

// This gets the variables
chrome.storage.local.get(["videoURLS"], function (result) {
    videoURLS = result.videoURLS;
    //videoURLS = [];
    console.log(videoURLS);
});
chrome.storage.local.get(["imageURLS"], function (result) {
    imageURLS = result.imageURLS;
    //imageURLS = [];    
    console.log(imageURLS);
});
chrome.storage.local.get(["videoTimes"], function (result) {
    videoTimes = result.videoTimes;
    //videoTimes = [];
    console.log(videoTimes);
});
chrome.storage.local.get(["videoNames"], function (result) {
    videoNames = result.videoNames;
    //videoNames = [];
    console.log(videoNames);
});
chrome.storage.local.get(["videoCreators"], function (result) {
    videoCreators = result.videoCreators;
    //videoCreators = [];
    console.log(videoCreators);
    setImages();
});

// Adds the events when the thumnail has a mouse over the thumbnail to show the details or hides when it isn't
document.getElementById("First").addEventListener("mouseover", showFirstDetails);
document.getElementById("First").addEventListener("mouseout", hideFirstDetails);

// Adds the events when the thumnail has a mouse over the delete button to show the details or hides when it isn't
document.getElementsByName("firstDelete")[0].addEventListener("mouseover", showFirstDetails);

// Adds the events when the thumnail has a mouse over the shadow to show the details
document.getElementsByName("firstOverlay")[0].addEventListener("mouseover", showFirstDetails);
document.getElementsByName("firstOverlay")[0].addEventListener("mouseout", hideFirstDetails);

// This function deletes a specific thumbnail and it's values from the lists
document.getElementsByName("firstDelete")[0].addEventListener("click", function(){
    var current = document.getElementsByName("firstDelete")[0];
    current = current.parentNode;
    current = current.parentNode;
    current = current.childNodes[1];
    current = current.childNodes[3];
    var currentURL = current.getAttribute("href");
    deleteSpecificURL(currentURL);
});

document.getElementsByName("firstOverlay")[0].addEventListener("click", function(){
    var current = document.getElementsByName("firstOverlay")[0];
    current = current.parentNode;
    current = current.parentNode;
    current = current.childNodes[1];
    current = current.childNodes[3];
    var currentURL = current.getAttribute("href");
    window.location.href = currentURL;
});

// Adds the events when the thumnail has a mouse over the thumbnail to show the details or hides when it isn't
document.getElementById("Second").addEventListener("mouseover", showSecondDetails);
document.getElementById("Second").addEventListener("mouseout", hideSecondDetails);

// Adds the events when the thumnail has a mouse over the delete button to show the details or hides when it isn't
document.getElementsByName("secondDelete")[0].addEventListener("mouseover", showSecondDetails);

// Adds the events when the thumnail has a mouse over the shadow to show the details
document.getElementsByName("secondOverlay")[0].addEventListener("mouseover", showSecondDetails);
document.getElementsByName("secondOverlay")[0].addEventListener("mouseout", hideSecondDetails);


// This function deletes a specific thumbnail and it's values from the lists
document.getElementsByName("secondDelete")[0].addEventListener("click", function(){
    var current = document.getElementsByName("secondDelete")[0];
    current = current.parentNode;
    current = current.parentNode;
    current = current.childNodes[1];
    current = current.childNodes[3];
    var currentURL = current.getAttribute("href");
    deleteSpecificURL(currentURL);
});

// Adds the events when the thumnail has a mouse over the thumbnail to show the details or hides when it isn't
document.getElementById("Third").addEventListener("mouseover", showThirdDetails);
document.getElementById("Third").addEventListener("mouseout", hideThirdDetails);

// Adds the events when the thumnail has a mouse over the delete button to show the details
document.getElementsByName("thirdDelete")[0].addEventListener("mouseover", showThirdDetails);

// Adds the events when the thumnail has a mouse over the shadow to show the details
document.getElementsByName("thirdOverlay")[0].addEventListener("mouseover", showThirdDetails);
document.getElementsByName("thirdOverlay")[0].addEventListener("mouseout", hideThirdDetails);

// This function deletes a specific thumbnail and it's values from the lists
document.getElementsByName("thirdDelete")[0].addEventListener("click", function(){
    var current = document.getElementsByName("thirdDelete")[0];
    current = current.parentNode;
    current = current.parentNode;
    current = current.childNodes[1];
    current = current.childNodes[3];
    var currentURL = current.getAttribute("href");
    deleteSpecificURL(currentURL);
});

// This updates the thumbnails
document.getElementById("leftButton").addEventListener("click",function(){
    currentTotal=currentTotal-4;
    if(currentTotal<0){
        currentTotal = videoURLS.length+currentTotal;
    }
    setImages();
});

// This updates the thumbnails
document.getElementById("rightButton").addEventListener("click",function(){
    currentTotal-=2;
    if (currentTotal<0){
        currentTotal = videoURLS.length+currentTotal;
    }
    setImages();
});

// Clears the variables and sets to the storage when the reset button is clicked
document.getElementById("resetButton").addEventListener("click",function(){
    videoURLS = [];
    imageURLS = [];    
    videoTimes = [];
    videoNames = [];
    videoCreators = [];
    chrome.storage.local.set({ "videoURLS": videoURLS }, function () {
        console.log(videoURLS);
    });
    chrome.storage.local.set({ "imageURLS": imageURLS }, function () {
        console.log(imageURLS);
    });
    chrome.storage.local.set({ "videoTimes": videoTimes }, function () {
        console.log(videoTimes);
    });
    chrome.storage.local.set({ "videoNames": videoNames }, function () {
        console.log(videoNames);
    });
    chrome.storage.local.set({ "videoCreators": videoCreators }, function () {
        console.log(videoCreators);
    });
    location.reload();
});

// Sets the images in the three images to the values that are stored in the variables
// using the overall currenttTotal variable
function setImages() {
    if(videoURLS.length == 0){
        document.querySelectorAll("#MainTable")[0].style.visibility = "hidden";
    }
    if(currentTotal<0){
        currentTotal=0;
    }
    for (var i = 0; i < 3; i++) {
        document.querySelectorAll("#URL")[i].setAttribute("href", videoURLS[currentTotal]);
        document.querySelectorAll("#URLOverlay")[i].setAttribute("href", videoURLS[currentTotal]);
        document.querySelectorAll("#Image")[i].setAttribute("src", imageURLS[currentTotal]);
        document.querySelectorAll("#Name")[i].innerHTML = videoNames[currentTotal];
        document.querySelectorAll("#Time")[i].innerHTML = videoTimes[currentTotal];
        document.querySelectorAll("#Creator")[i].innerHTML = videoCreators[currentTotal];
        currentTotal += 1;
        if (currentTotal >= videoURLS.length) {
            currentTotal = 0;
        }
    }
}

// This function hdies all the text that overlays ontop of the thumbnails to visible in the first image
function showFirstDetails() {
    document.querySelectorAll("#Shadow")[0].style.display = "block";    
    document.querySelectorAll("#Delete")[0].style.display = "block";
    document.querySelectorAll("#Name")[0].style.visibility = "visible";
    document.querySelectorAll("#Time")[0].style.visibility = "visible";
    document.querySelectorAll("#Creator")[0].style.visibility = "visible";
}

// This function hdies all the text that overlays ontop of the thumbnails to hidden in the first image
function hideFirstDetails() {
    document.querySelectorAll("#Shadow")[0].style.display = "none"; 
    document.querySelectorAll("#Delete")[0].style.display = "none";
    document.querySelectorAll("#Name")[0].style.visibility = "hidden";
    document.querySelectorAll("#Time")[0].style.visibility = "hidden";
    document.querySelectorAll("#Creator")[0].style.visibility = "hidden";
}

// This function hdies all the text that overlays ontop of the thumbnails to visible in the second image
function showSecondDetails() {
    document.querySelectorAll("#Shadow")[1].style.display = "block";
    document.querySelectorAll("#Delete")[1].style.display = "block";
    document.querySelectorAll("#Name")[1].style.visibility = "visible";
    document.querySelectorAll("#Time")[1].style.visibility = "visible";
    document.querySelectorAll("#Creator")[1].style.visibility = "visible";
}

// This function hdies all the text that overlays ontop of the thumbnails to hidden in the second image
function hideSecondDetails() {
    document.querySelectorAll("#Shadow")[1].style.display = "none"; 
    document.querySelectorAll("#Delete")[1].style.display = "none";
    document.querySelectorAll("#Name")[1].style.visibility = "hidden";
    document.querySelectorAll("#Time")[1].style.visibility = "hidden";
    document.querySelectorAll("#Creator")[1].style.visibility = "hidden";
}

// This function hdies all the text that overlays ontop of the thumbnails to visible in the third image
function showThirdDetails() {
    document.querySelectorAll("#Shadow")[2].style.display = "block"; 
    document.querySelectorAll("#Delete")[2].style.display = "block";
    document.querySelectorAll("#Name")[2].style.visibility = "visible";
    document.querySelectorAll("#Time")[2].style.visibility = "visible";
    document.querySelectorAll("#Creator")[2].style.visibility = "visible";
}

// This function hdies all the text that overlays ontop of the thumbnails to hidden in the third image
function hideThirdDetails() {
    document.querySelectorAll("#Shadow")[2].style.display = "none"; 
    document.querySelectorAll("#Delete")[2].style.display = "none";
    document.querySelectorAll("#Name")[2].style.visibility = "hidden";
    document.querySelectorAll("#Time")[2].style.visibility = "hidden";
    document.querySelectorAll("#Creator")[2].style.visibility = "hidden";
}

// This function deletes the specific videos url from the arries and calls the set to storage function
function deleteSpecificURL(URL){
    var index = videoURLS.indexOf(URL);
    console.log(index);
    if(videoURLS.includes(URL)){
        index = videoURLS.indexOf(URL);
        videoURLS.splice(index, 1);
        imageURLS.splice(index, 1);
        videoCreators.splice(index, 1);
        videoNames.splice(index, 1);
        videoTimes.splice(index, 1);
        setToStorage(videoCreators, videoNames, videoTimes, videoURLS, imageURLS);
    }
}

// This function sets the arraries that were edited to the storage
function setToStorage(videoCreators, videoNames, videoTimes, videoURLS, imageURLS) {
    chrome.storage.local.set({ "videoURLS": videoURLS }, function () {
        console.log(videoURLS);
    });
    chrome.storage.local.set({ "imageURLS": imageURLS }, function () {
        console.log(imageURLS);
    });
    chrome.storage.local.set({ "videoTimes": videoTimes }, function () {
        console.log(videoTimes);
    });
    chrome.storage.local.set({ "videoNames": videoNames }, function () {
        console.log(videoNames);
    });
    chrome.storage.local.set({ "videoCreators": videoCreators }, function () {
        console.log(videoCreators);
    });
    console.log("storage updated");
    currentTotal-=1;
    setImages();
}