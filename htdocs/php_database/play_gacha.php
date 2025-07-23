<?php
require_once('db.php');

if(!isset($_REQUEST['user_id']))
{
    echo 'user_idパラメータがありません';
    exit;
}

if(!isset($_REQUEST['count']))
{
    echo 'countパラメータがありません';
    exit;
}

$user_id = htmlspecialchars( $_REQUEST['user_id'], ENT_QUOTES );
$count = htmlspecialchars( $_REQUEST['count'], ENT_QUOTES );

$card_ids = array();

// データベース接続
$pdo = Db::get_pdo();

//マスターテーブルからカードを毎の重みを取得 
$stmt = $pdo->prepare('SELECT * FROM master_gacha');
$stmt->execute();
$master_gacha = $stmt->fetchAll();

//重みの合計を計算
$sum_wieght = 0;
foreach($master_gacha as $recode)
{
    $sum_wieght += $recode['weight'];
}

//指定された回数分回す
for($i=0;$i<$count;$i++)
{
    //重み内で乱数
    $rand_weight = mt_rand(1,$sum_wieght);
    
    //重みを減らして、0になった時のカードが抽選結果
    foreach($master_gacha as $recode)
    {
        $rand_weight -= $recode['weight'];
        if($rand_weight <= 0)
        {
            $card_ids[] = $recode['card_id'];
            break;
        }
    }
}

//トランザクションを張る
$pdo->beginTransaction();
try
{
    foreach($card_ids as $card_id)
    {
        //カードテーブルの追加
        $stmt = $pdo->prepare('INSERT INTO `card` (user_id, card_id) VALUES (:user_id, :card_id)');
        $stmt->bindValue(':user_id',$user_id);
        $stmt->bindValue(':card_id',$card_id);
        $stmt->execute();
    }
    
    //課金石を減らす
    $stmt = $pdo->prepare('UPDATE `payment` SET `amount` = `amount` - 300 * :count WHERE user_id=:user_id');
    $stmt->bindValue('count',$count);
    $stmt->bindValue('user_id',$user_id);
    $stmt->execute();
    //トランザクション確定
    $pdo->commit();
}
catch(PDOException $e)
{
    //巻き戻す
    $pdo->rollBack();
    throw $e;
}

// 出力を成形
$result = array();
$result['card_ids'] = $card_ids;

// json形式で出力
header('HTTP/1.1 200 OK');
header('Content-Type: application/json; charset=utf-8');
echo json_encode($result, JSON_UNESCAPED_UNICODE);
