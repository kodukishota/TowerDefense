ご予約日：
<?php
foreach($_POST['reserve'] as $key => $reserve)
{
    print(htmlspecialchars($reserve, ENT_QUOTES));

    if($key !== array_key_last($_POST['reserve']))print ', ';
}
?>