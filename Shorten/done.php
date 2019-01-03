<!DOCTYPE html>
<html>

<head>
    <title>Tom Bowyer Short Created</title>
    <meta name="description" content="Tom Bowyer Short Page">
    <meta charset="UTF-8" name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="short.css" rel="stylesheet" type="text/css" />
    <script src="short.js"></script>
    <link rel="icon" href="Image/icon.jpg" sizes="16x16">
    <link rel="icon" href="Image/icon.jpg" sizes="32x32">
</head>

<body>

    <h1>URL Created!</h1>

    <?php

    session_start();

    $_SESSION['Url'] = "";
    if ($_SESSION['shorten'] == NULL) {
        header('Location: http://localhost/short/home.php');
    }

    $createdURL = 'http://localhost/short/s.php?u=' . $_SESSION['shorten'];

    echo 'Shareable link here: ';

    echo '<input type="text" value="' . $createdURL . '" id="urlInput">';

    echo '<button onclick="copyText()" id="urlButton">Copy URL</button>';

    echo '<p>Return home ';

    $homelink = 'http://localhost/short/home.php';

    echo '<a href="' . $homelink . '">' . $homelink . '</a><br><br>';

    echo 'Reminder that you can only create one link every 30 seconds.</p>';

    ?> 

</body>