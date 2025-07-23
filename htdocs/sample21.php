<?php
$zip ='987-6543';

$zip = mb_convert_kana($zip,'a','UTF-8');

if(preg_match(("/ \A \d {3} [-] \d {4} \z /"),$zip))
{
    print('郵便番号:〒'.$zip);
}
else
{
    print('※郵便番号を123-4567の形式でご記入ください。');
}