<?php
    $pdo = new PDO('mysql:host=localhost;dbname=short', 'root', 'test');
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    $time = strtotime('-24 hours');
    $databasePastDate = date("Y-m-d H:i:s", $time);

    try {
        $sql = "DELETE FROM short.urls WHERE created < '$databasePastDate';";
        $pdo->exec($sql);
    } catch (PDOException $e) {
        echo $e->getMessage();
    }

?>