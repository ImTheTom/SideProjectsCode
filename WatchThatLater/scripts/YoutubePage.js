// YOUTUBE PAGE MAIN SCRIPT

// Defining global variables. As well as accessing storage.
var mainURL = "https://www.youtube.com";

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
    checkCurrentURL();
    var checkTheURL = setInterval(function(){
        var currentURL = window.location.href;
        var index = videoURLS.indexOf(currentURL);
        if(videoURLS.includes(currentURL)){
            index = videoURLS.indexOf(currentURL);
            videoURLS.splice(index, 1);
            imageURLS.splice(index, 1);
            videoCreators.splice(index, 1);
            videoNames.splice(index, 1);
            videoTimes.splice(index, 1);
            setToStorage(videoCreators, videoNames, videoTimes, videoURLS, imageURLS);
        }
    }, 5000);
});
console.log("Loaded from chrome storage");




// This waits for the page to load and then runs an anonymous function that
// adds outputs a message in the console and adds an event listener that
// listens to clicks on the page.
window.addEventListener("load", function () {
    console.log('YoutubePage.js Loaded');
    document.addEventListener("click", main, true);
});

function addClickListener() {
    document.addEventListener("click", main, true);
}

function removeClickListener() {
    document.removeEventListener("click", main, true);
}

// This is the main function that controls everything.
function main(e) {

    removeClickListener();

    var target = getElementWasClicked(e);

    if (!check(target)) {
        console.log("Exited Function");
        addClickListener();
        return;
    }

    var thumbnail = getThumbnail(target);
    var thumbnailParent = getOverall(thumbnail);

    var videoName = getNameOfVideo(thumbnailParent);
    var videoUploader = getNameOfVideoUploader(thumbnailParent);
    var videoLength = getVideoLength(thumbnail);
    var thumbnailImageURL = getThumbnailImage(thumbnail);
    var thumbnailVideoURL = thumbnailURL(thumbnail);

    addToStorage(thumbnailVideoURL, thumbnailImageURL, videoLength, videoUploader, videoName);

    setToStorage(videoCreators, videoNames, videoTimes, videoURLS, imageURLS);

    addClickListener();
}

// This function gets the element that was clicked.
function getElementWasClicked(e) {
    e = e || window.event;
    var target = e.target || e.srcElement;
    return target;
}

// This function checks if the watch later overlay was clicked.
function check(target) {
    var targetClass = target.className;
    var searchClass = "ytd-thumbnail-overlay-toggle-button-renderer";
    if (targetClass.includes(searchClass)) {
        return true;
    } else {
        return false;
    }
}

// This function gets the overall thumbnail parent of element that was clicked.
function getThumbnail(target) {
    var targetParent = target.parentNode;
    var overlaysElement = targetParent.parentNode;
    var tumbnailElement = overlaysElement.parentNode;
    return tumbnailElement;
}

// Gets the thumbnail image child
function getThumbnailImage(thumbnail) {
    var thumbnailChild = thumbnail.childNodes;
    thumbnailChild = thumbnailChild[1];
    thumbnailChild = thumbnailChild.childNodes;
    thumbnailChild = thumbnailChild[0];
    var thumbnailImageURL = thumbnailChild.getAttribute("src");
    return thumbnailImageURL;
}

// Gets the thumbnail url that is linked to the thumbnail
function thumbnailURL(thumbnail) {
    var thumbnailURL = thumbnail.getAttribute("href");
    thumbnailURL = mainURL + thumbnailURL;
    return thumbnailURL;
}

// This gets the overall parent of the thumbnail that was clicked on
function getOverall(target) {
    target = target.parentNode;
    target = target.parentNode;
    return target;
}

// This gets the video name of the clicked on element
function getNameOfVideo(target) {
    var currentURL = window.location.href;
    if(currentURL.includes("watch")){
        console.log(target);
        var child = target.children[1];
        child = child.children[0];
        child = child.children[1];
        var title = child.getAttribute("Title");
        return title;
    }else{
    var child = target.children[1];
    child = child.children[0];
    child = child.children[0];
    child = child.children[1];
    var title = child.getAttribute("Title");
    console.log(title);
    return title;
    }
}

// This gets the video creator of the clicked on element
function getNameOfVideoUploader(target) {
    var currentURL = window.location.href;
    if(currentURL.includes("watch")){
        var child = target.children[1];
        child = child.children[1];
        child = child.children[0];
        child = child.children[0];
        child = child.children[0];
        child = child.children[0];
        child = child.innerHTML;
        return child;
    }else{
    var child = target.children[1];
    child = child.children[0];
    child = child.children[1];
    child = child.children[0];
    child = child.children[0];
    child = child.children[0];
    child = child.children[0];
    child = child.innerHTML;
    return child;
    }
}

// This gets the video length of the clicked on element
function getVideoLength(target) {
    var child = target.children[2];
    child = child.children[0];
    child = child.children[0];
    child = child.innerHTML;
    child = child.replace(/ /g, '');
    child = child.replace(/\s/g, '');
    return child;
}

// This function either adds the video urls and the relating variables values to the variables if it doesn't
// Already exists. If it does then it removes it.
function addToStorage(thumbnailVideoURL, thumbnailImageURL, videoLength, videoUploader, videoName) {
    if (videoURLS.includes(thumbnailVideoURL)) {
        var index = videoURLS.indexOf(thumbnailVideoURL);
        videoURLS.splice(index, 1);
        imageURLS.splice(index, 1);
        videoCreators.splice(index, 1);
        videoNames.splice(index, 1);
        videoTimes.splice(index, 1);
    } else {
        videoURLS.push(thumbnailVideoURL);
        imageURLS.push(thumbnailImageURL);
        videoCreators.push(videoUploader);
        videoNames.push(videoName);
        videoTimes.push(videoLength);
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
}

// This function checks if the current url is within the video urls variable
// If so it deletes it from the array and all the relating variables values
// Then calls the set to storage function
function checkCurrentURL(){
    console.log("Current check of URL called");
    var currentURL = window.location.href;
    var index = videoURLS.indexOf(currentURL);
    console.log(index);
    if(videoURLS.includes(currentURL)){
        console.log("yes");
        index = videoURLS.indexOf(currentURL);
        videoURLS.splice(index, 1);
        imageURLS.splice(index, 1);
        videoCreators.splice(index, 1);
        videoNames.splice(index, 1);
        videoTimes.splice(index, 1);
        setToStorage(videoCreators, videoNames, videoTimes, videoURLS, imageURLS);
    }
}