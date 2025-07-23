<?php
require_once('db.php');

$user_id = $_REQUEST['user_id'];

$pdo = Db::get_pdo();

$stmt = $pdo->prepare('DELETE FROM `user_deck` WHERE user_id=:user_id');
$stmt->bindValue(':user_id', $user_id);
$stmt->execute();

header('HTTP/1.1 200 OK');
header('Content-Type: application/json; charset=utf-8');
echo json_encode($result, JSON_UNESCAPED_UNICODE);
