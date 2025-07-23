<?php
$fruits = [
    'apple'=>'りんご',
    'grap'=>'ぶどう',
    'lemon'=>'レモン',
    'tomato'=>'トマト',
    'peach'=>'もも', 
];

foreach($fruits as $english => $japanese)
{
    print($english.' : '.$japanese."<br>");
}