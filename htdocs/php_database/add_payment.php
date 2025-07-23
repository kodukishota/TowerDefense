<?php
require_once('db.php');

if(!isset($_REQUEST['user_id']))
{
    echo 'user_idパラメータがありません';
    exit;
}
$user_id = htmlspecialchars( $_REQUEST['user_id'], ENT_QUOTES );

$card_id = 0;

// データベース接続
$pdo = Db::get_pdo();

//課金石を減らす
$stmt = $pdo->prepare('UPDATE `payment` SET `amount` = `amount` + 3000 WHERE user_id=:user_id');
$stmt->bindValue('user_id',$user_id);
$stmt->execute();

// 出力を成形
$result = array();
$result['result'] = 1;

// json形式で出力
header('HTTP/1.1 200 OK');
header('Content-Type: application/json; charset=utf-8');
echo json_encode($result, JSON_UNESCAPED_UNICODE);
