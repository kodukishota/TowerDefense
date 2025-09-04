CREATE TABLE `master_gacha`(
    `character_id` INT UNSIGNED PRIMARY KEY,
    `weight` INT UNSIGNED NOT NULL
);

INSERT INTO `master_gacha`(`character_id`, `weight`) VALUES
(1,1),
(2,1),
(3,1)