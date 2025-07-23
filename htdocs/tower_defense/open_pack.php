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

if(!isset($_REQUEST['gold_or_gem']))
{
     echo 'gold_or_gemパラメータがありません';
    exit;
}

$user_id = htmlspecialchars( $_REQUEST['user_id'], ENT_QUOTES );
$count = htmlspecialchars( $_REQUEST['count'], ENT_QUOTES );
$gold_or_gem = htmlspecialchars( $_REQUEST['gold_or_gem'], ENT_QUOTES );

$character_ids = array();

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
            $character_ids[] = $recode['character_id'];
            break;
        }
    }
}

//トランザクションを張る
$pdo->beginTransaction();
try
{
    foreach($character_ids as $character_id)
    {
        //カードテーブルの追加
        $stmt = $pdo->prepare('INSERT INTO `card` (user_id, character_id) VALUES (:user_id, :character_id)');
        $stmt->bindValue(':user_id',$user_id);
        $stmt->bindValue(':character_id',$character_id);
        $stmt->execute();
    }

    if($gold_or_gem == 0)
    {
        //ゲーム内通貨を減らす
        $stmt = $pdo->prepare('UPDATE `payment` SET `gold` = `gold` - 500 * :count WHERE user_id=:user_id');
        $stmt->bindValue('count',$count);
        $stmt->bindValue('user_id',$user_id);
        $stmt->execute();
    }
    else
    {
         //石を減らす
        $stmt = $pdo->prepare('UPDATE `payment` SET `gem` = `gem` - 100 * :count WHERE user_id=:user_id');
        $stmt->bindValue('count',$count);
        $stmt->bindValue('user_id',$user_id);
        $stmt->execute();
    }

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
$result['character_ids'] = $character_ids;

// json形式で出力
header('HTTP/1.1 200 OK');
header('Content-Type: application/json; charset=utf-8');
echo json_encode($result, JSON_UNESCAPED_UNICODE);
