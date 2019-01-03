<!DOCTYPE html>
<html>

<head>
    <title>Tom Bowyer Short Home</title>
    <meta name="description" content="Tom Bowyer Short Page">
    <meta charset="UTF-8" name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="short.css" rel="stylesheet" type="text/css" />
    <link rel="icon" href="Image/icon.jpg" sizes="16x16">
    <link rel="icon" href="Image/icon.jpg" sizes="32x32">
</head>

<body>

    <h1>Create a short url here!</h1>

    <?php

    session_start();

    if ($_SESSION['error'] !== "") {
        echo $_SESSION['error'];
        $_SESSION['error'] = "";
    }

    $_SESSION['shorten'] = "";

    echo '<form id="urlCreator" action="/short/create.php" method="post" enctype="url-form-encoded">';
    echo '<label id="urlLabel">Url </label>';
    echo '<input type="text" id="url" name="Url" value = "' . $_SESSION['Url'] . '" placeholder = "Enter Url Here..." required>';

    $_SESSION['Url'] = "";

    ?>

    <input type="submit" id="submit" value="Submit">

    <h2> Information </h2>

    <p>
        These pages will create a link to any site in a form that can be easily shared. Links older than 1 day will expire.<br><br>
        You can only create a link once every 30 seconds and anyone from the world can only create a link once every 2 seconds. <br><br>
        The repository for the code can be found here - <a href='https://github.com/ImTheTom/SideProjectsCode/tree/master/Shorten'>The Repository.</a><br><br>
        The author is Tom Bowyer. My website can be found here - <a href='https://tombowyer.com'>His website.</a>
    </p>

</body>