create table T_RANGE (
  id NUMBER,
  data VARCHAR2(100),
  CONSTRAINT pk_t_range PRIMARY KEY (id)
)
PARTITION BY RANGE (id) (
  PARTITION p1 VALUES LESS THAN (1000),
  PARTITION p2 VALUES LESS THAN (2000),
  PARTITION p3 VALUES LESS THAN (3000),
  PARTITION p4 VALUES LESS THAN (MAXVALUE)
);
--2
CREATE TABLE T_INTERVAL (
  id NUMBER,
  data VARCHAR2(100),
  CONSTRAINT pk_t_interval PRIMARY KEY (id)
)
PARTITION BY RANGE (id) INTERVAL (100)
(
  PARTITION p1 VALUES LESS THAN (100),
  PARTITION p2 VALUES LESS THAN (200),
  PARTITION p3 VALUES LESS THAN (300)
);
--3
CREATE TABLE T_HASH (
  id NUMBER,
  data VARCHAR2(100),
  CONSTRAINT pk_t_hash PRIMARY KEY (id)
)
PARTITION BY HASH (id)
PARTITIONS 4;
--4
CREATE TABLE T_LIST (
  id NUMBER,
  data VARCHAR2(100),
  CONSTRAINT pk_t_list PRIMARY KEY (id)
)
PARTITION BY LIST (data) (
  PARTITION p1 VALUES ('A'),
  PARTITION p2 VALUES ('B'),
  PARTITION p3 VALUES ('C'),
  PARTITION p4 VALUES (DEFAULT)
);
-- drop table T_RANGE;
-- drop table T_INTERVAL;
-- drop table T_LIST;
-- drop table T_HASH;

--5
INSERT INTO T_RANGE (id, data) VALUES (50, 'Data for p1');
INSERT INTO T_RANGE (id, data) VALUES (150, 'Data for p2');
INSERT INTO T_RANGE (id, data) VALUES (250, 'Data for p3');
INSERT INTO T_RANGE (id, data) VALUES (350, 'Data for p4');

SELECT id, data FROM T_RANGE;

INSERT INTO T_INTERVAL (id, data) VALUES (50, 'Data for p1');
INSERT INTO T_INTERVAL (id, data) VALUES (150, 'Data for p2');
INSERT INTO T_INTERVAL (id, data) VALUES (250, 'Data for p3');
INSERT INTO T_INTERVAL (id, data) VALUES (350, 'Data for p4');

SELECT id, data FROM T_INTERVAL;

INSERT INTO T_HASH (id, data) VALUES (1, 'Data for p1');
INSERT INTO T_HASH (id, data) VALUES (2, 'Data for p2');
INSERT INTO T_HASH (id, data) VALUES (3, 'Data for p3');
INSERT INTO T_HASH (id, data) VALUES (4, 'Data for p4');

SELECT id, data FROM T_HASH;

INSERT INTO T_LIST (id, data) VALUES (1, 'A data');
INSERT INTO T_LIST (id, data) VALUES (2, 'B data');
INSERT INTO T_LIST (id, data) VALUES (3, 'C data');
INSERT INTO T_LIST (id, data) VALUES (4, 'Default data');

SELECT id, data FROM T_LIST;

--6

--7-9
DESCRIBE T_RANGE;

-- Выполним оператор ALTER TABLE MERGE для объединения разделов p1 и p2
ALTER TABLE T_RANGE MERGE PARTITIONS part1, part2 INTO PARTITION p_merged;

-- Проверим измененную структуру таблицы T_RANGE после объединения разделов
DESCRIBE T_RANGE;

DESCRIBE T_INTERVAL;

-- Выполним оператор ALTER TABLE SPLIT для разделения раздела p1 на два новых раздела
ALTER TABLE T_INTERVAL SPLIT PARTITION p1 INTO (PARTITION p2 VALUES LESS THAN (100), PARTITION p3 VALUES LESS THAN (200));

DESCRIBE T_INTERVAL;