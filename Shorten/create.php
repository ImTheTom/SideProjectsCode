<?php

session_start();

$url = $_POST['Url'];

if (empty($url)) {

    $_SESSION['error'] = 'Error';
    header('Location: http://localhost/short/home.php');

} else {

    $_SESSION['Url'] = $_POST['Url'];

    if (!isset($_COOKIE["created"])) {

        $pdo = new PDO('mysql:host=localhost;dbname=short', 'root', 'test');
        $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

        $time = strtotime('-3 seconds');
        $databasePastDate = date("Y-m-d H:i:s", $time);

        try {
            $result = $pdo->query("SELECT * FROM short.urls WHERE created > '$databasePastDate';");
        }
        catch(PDOException $e) {
            echo $e->getMessage();
        }

        $count = $result->rowCount();
        if ($count !== 0) {
            $_SESSION['error'] = "Someone recently entered into the database try again.";
            header('Location: http://localhost/short/home.php');
        }

        if ($url[0] !== 'h') {
            $url = 'http://' . $url;
        }

        $url = filter_var(strip_tags($url), FILTER_SANITIZE_URL);

        $characters = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
        $result = '';

        for ($i = 0;$i < 6;$i++) $result.= $characters[mt_rand(0, 61) ];

        $databaseDate = date("Y-m-d H:i:s");
        $sql = "INSERT INTO short.urls (original, short, created) VALUES (?,?,?)";

        $stmt = $pdo->prepare($sql);
        $stmt->execute([$url, $result, $databaseDate]);

        $_SESSION['shorten'] = $result;

        setcookie("created", 1, time() + (30), "/");

        header('Location: http://localhost/short/done.php');

    } else {

        $_SESSION['error'] = 'You have recently created a short url, please wait 30 seconds';
        header('Location: http://localhost/short/home.php');

    }

}

?>