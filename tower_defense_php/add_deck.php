<?php
require_once('db.php');

if(!isset($_REQUEST['user_id']))
{
    echo 'user_idパラメータがありません';
    exit;
}

if(!isset($_REQUEST['character_id']))
{
    echo 'character_idsパラメータがありません';
    exit;
}

$user_id = htmlspecialchars( $_REQUEST['user_id'], ENT_QUOTES );
$character_id = htmlspecialchars($_REQUEST['character_id'], ENT_QUOTES );

// データベース接続
$pdo = Db::get_pdo();

//トランザクションを張る
$pdo->beginTransaction();
try
{    
    // SQL実行
    $stmt = $pdo->prepare('INSERT INTO user_deck (`user_id`,`in_deck_card`) VALUES (:user_id,:character_id)');
    $stmt->bindValue(':user_id', $user_id);
    $stmt->bindValue(':character_id', $character_id);
    $stmt->execute();
        
    //トランザクション確定
    $pdo->commit();
}
catch(PDOException $e)
{
    //巻き戻す
    $pdo->rollBack();
}

// json形式で出力
header('HTTP/1.1 200 OK');
header('Content-Type: application/json; charset=utf-8');
echo json_encode($result, JSON_UNESCAPED_UNICODE);
