//This function sets all the variables to empty arraries

function resetLocalStorage() {
    chrome.storage.local.get(["videoURLS"], function (result) {
        videoURLS = result.videoURLS;
        videoURLS = [videoURLS];
        videoURLS = [];
        console.log(videoURLS);
    });
    chrome.storage.local.get(["imageURLS"], function (result) {
        imageURLS = result.imageURLS;
        imageURLS = [imageURLS];
        imageURLS = [];
        console.log(imageURLS);
    });
    chrome.storage.local.get(["videoTimes"], function (result) {
        videoTimes = result.videoTimes;
        videoTimes = [];
        console.log(videoTimes);
    });
    chrome.storage.local.get(["videoNames"], function (result) {
        videoNames = result.videoNames;
        videoNames = [];
        console.log(videoNames);
    });
    chrome.storage.local.get(["videoCreators"], function (result) {
        videoCreators = result.videoCreators;
        videoCreators = [];
        console.log(videoCreators);
    });
    chrome.storage.local.set({ "videoURLS": emptyArray }, function () {
        console.log(videoURLS);
    });
    chrome.storage.local.set({ "imageURLS": emptyArray }, function () {
        console.log(imageURLS);
    });
    chrome.storage.local.set({ "videoTimes": emptyArray }, function () {
        console.log(videoTimes);
    });
    chrome.storage.local.set({ "videoNames": emptyArray }, function () {
        console.log(videoNames);
    });
    chrome.storage.local.set({ "videoCreators": emptyArray }, function () {
        console.log(videoCreators);
    });

}