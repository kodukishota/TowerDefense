<?php
require_once('db.php');

if(!isset($_REQUEST['name']))
{
    echo 'nameパラメータがありません';
    exit;
}
$name = htmlspecialchars( $_REQUEST['name'], ENT_QUOTES );

// データベース接続
$pdo = Db::get_pdo();

//トランザクションを張る
$pdo->beginTransaction();
try
{
    // SQL実行
    $stmt = $pdo->prepare('INSERT INTO user (`name`) VALUES (:name)');
    $stmt->bindValue(':name', $name);
    $stmt->execute();

    // オートインクリメントのIDを取得
    $user_id = $pdo->lastInsertId();
    
    //トランザクション確定
    $pdo->commit();
}
catch(PDOException $e)
{
    //巻き戻す
    $pdo->rollBack();
}

// 出力を成形
$result = array();
$result['user_id'] = $user_id;

// json形式で出力
header('HTTP/1.1 200 OK');
header('Content-Type: application/json; charset=utf-8');
echo json_encode($result, JSON_UNESCAPED_UNICODE);
