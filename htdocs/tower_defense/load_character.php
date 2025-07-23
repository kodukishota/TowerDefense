<?php
require_once('db.php');

if(!isset($_REQUEST['character_id']))
{
     echo 'character_idパラメータがありません';
    exit;
}

$character_id = $_REQUEST['character_id'];

$pdo = Db::get_pdo();

$stmt = $pdo->prepare('SELECT `name`, `hp`, `atk`, `speed`, `cost` FROM `character` WHERE id=:character_id');
$stmt->bindValue(':character_id', $character_id);
$stmt->execute();

$record = $stmt->fetch();

$result = array();
$result['name'] = $record['name'];
$result['hp'] = $record['hp'];
$result['atk'] = $record['atk'];
$result['speed'] = $record['speed'];
$result['cost'] = $record['cost'];

header('HTTP/1.1 200 OK');
header('Content-Type: application/json; charset=utf-8');
echo json_encode($result, JSON_UNESCAPED_UNICODE);