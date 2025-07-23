<?php

class Db
{
    const Host = 'localhost';
    const DbName = 'towerdefense';
    const User = 'root';
    const Pass = null;

    static function get_pdo()
    {
        return new PDO('mysql:host='.self::Host . ';dbname=' . self::DbName.';charset=utf8',
            self::User, self::Pass);
    }
}
