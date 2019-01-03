<?php

$page = $_GET['u'];

if ($page === NULL) {
    
    header('Location: http://localhost/short/home.php');
    
} else {
    
    $pdo = new PDO('mysql:host=localhost;dbname=short', 'root', 'test');
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    
    try {
        $result = $pdo->query("SELECT * FROM short.urls where short='$page';");
    }
    catch (PDOException $e) {
        echo $e->getMessage();
    }
    
    $count = $result->rowCount();
    
    foreach ($result as $index) {
        $redirectURL = $index['original'];
    }
    
    header("Location: " . $redirectURL);
    
    if ($count == 0) {
        session_start();
        
        $_SESSION['error'] = 'URL expired or no never existed.';
        
        header('Location: http://localhost/short/home.php');
    }
    
}

?> 