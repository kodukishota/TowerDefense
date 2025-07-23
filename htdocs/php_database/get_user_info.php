<?php
require_once('db.php');

$user_id = $_REQUEST['user_id'];

// データベース接続
$pdo = Db::get_pdo();

//===========ユーザ名取得===========
// SQL実行
$stmt = $pdo->prepare('SELECT `name`, `amount` FROM `user` LEFT JOIN `payment` ON `user`.`id` = `payment`.`user_id` WHERE id=:user_id');
$stmt->bindValue(':user_id', $user_id);
$stmt->execute();

// SQL結果を取得
$record = $stmt->fetch();   // fetchは1レコード取得

// 出力を成形
$result = array();
$result['name'] = $record['name'];
$result['paid'] = $record['amount'];

//===========カード一覧取得===========
// SQL実行
$stmt = $pdo->prepare('SELECT card_id FROM `card` WHERE user_id=:user_id');
$stmt->bindValue(':user_id', $user_id);
$stmt->execute();

// SQL結果を取得
$records = $stmt->fetchAll();   // fetchAllは複数レコード取得

// 出力を成形
$result['cards'] = array();
foreach($records as $r)
{
    $result['cards'][] = $r['card_id'];
}


// json形式で出力
header('HTTP/1.1 200 OK');
header('Content-Type: application/json; charset=utf-8');
echo json_encode($result, JSON_UNESCAPED_UNICODE);
