<?php
$age = 'あいうえお';

$age = mb_convert_kana($age,'n','UTF-8');
if(is_numeric($age))
{
    print($age.'歳');
}
else
{
    print('※年齢が数字ではありません。');
}