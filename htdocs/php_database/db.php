<?php

class Db
{
    const Host = 'localhost';
    const DbName = 'php_database';
    const User = 'root';
    const Pass = null;

    static function get_pdo()
    {
        return new PDO('mysql:host=' . self::Host . ';dbname=' . self::DbName,
            self::User, self::Pass);
    }
}
