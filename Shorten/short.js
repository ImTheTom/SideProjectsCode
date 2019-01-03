function copyText() {
    var copyText = document.getElementById("urlInput");
  
    copyText.select();
  
    document.execCommand("copy");
  
    document.getElementById("urlButton").innerHTML="Copied!";
} 