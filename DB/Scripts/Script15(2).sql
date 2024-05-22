CREATE TABLE T_RANGE (
    id NUMBER,
    data VARCHAR2(100),
    partition_key NUMBER
)
PARTITION BY RANGE (partition_key) (
    PARTITION p1 VALUES LESS THAN (100),
    PARTITION p2 VALUES LESS THAN (200),
    PARTITION p3 VALUES LESS THAN (300),
    PARTITION p4 VALUES LESS THAN (400)
);
--2
CREATE TABLE T_INTERVAL (
    id NUMBER,
    data VARCHAR2(100),
    partition_key DATE
)
PARTITION BY RANGE (partition_key) 
INTERVAL (NUMTOYMINTERVAL(1, 'MONTH')) (
    PARTITION p0 VALUES LESS THAN (TO_DATE('2024-01-01', 'YYYY-MM-DD'))
);


--3
CREATE TABLE T_HASH (
    id NUMBER,
    data VARCHAR2(100),
    partition_key VARCHAR2(50)
)
PARTITION BY HASH (partition_key) 
PARTITIONS 4;
--4
CREATE TABLE T_LIST (
    id NUMBER,
    data VARCHAR2(100),
    partition_key CHAR(1)
)
PARTITION BY LIST (partition_key) (
    PARTITION pA VALUES ('A'),
    PARTITION pB VALUES ('B'),
    PARTITION pC VALUES ('C'),
    PARTITION pD VALUES ('D')
);
--5
INSERT INTO T_RANGE (id, data, partition_key) VALUES (1, 'Data for p1', 50);
INSERT INTO T_RANGE (id, data, partition_key) VALUES (2, 'Data for p2', 150);
INSERT INTO T_RANGE (id, data, partition_key) VALUES (3, 'Data for p3', 250);
INSERT INTO T_RANGE (id, data, partition_key) VALUES (4, 'Data for p4', 350);

INSERT INTO T_INTERVAL (id, data, partition_key) VALUES (1, 'Data for p1', TO_DATE('2023-12-15', 'YYYY-MM-DD'));
INSERT INTO T_INTERVAL (id, data, partition_key) VALUES (2, 'Data for p2', TO_DATE('2024-01-15', 'YYYY-MM-DD'));
INSERT INTO T_INTERVAL (id, data, partition_key) VALUES (3, 'Data for p3', TO_DATE('2024-02-15', 'YYYY-MM-DD'));
INSERT INTO T_INTERVAL (id, data, partition_key) VALUES (4, 'Data for p4', TO_DATE('2024-03-15', 'YYYY-MM-DD'));

INSERT INTO T_HASH (id, data, partition_key) VALUES (1, 'Data for p1', 'A');
INSERT INTO T_HASH (id, data, partition_key) VALUES (2, 'Data for p2', 'B');
INSERT INTO T_HASH (id, data, partition_key) VALUES (3, 'Data for p3', 'C');
INSERT INTO T_HASH (id, data, partition_key) VALUES (4, 'Data for p4', 'D');

INSERT INTO T_LIST (id, data, partition_key) VALUES (1, 'Data for pA', 'A');
INSERT INTO T_LIST (id, data, partition_key) VALUES (2, 'Data for pB', 'B');
INSERT INTO T_LIST (id, data, partition_key) VALUES (3, 'Data for pC', 'C');
INSERT INTO T_LIST (id, data, partition_key) VALUES (4, 'Data for pD', 'D');


SELECT DISTINCT T_RANGE.PARTITION_KEY
FROM T_RANGE;

SELECT * FROM T_RANGE;
SELECT * FROM T_INTERVAL;
SELECT * FROM T_HASH;
SELECT * FROM T_LIST;
--6 
UPDATE T_RANGE SET partition_key = 320 WHERE PARTITION_KEY = 250;

UPDATE T_INTERVAL SET partition_key = TO_DATE('2024-02-15', 'YYYY-MM-DD') WHERE id = 1;

UPDATE T_HASH SET partition_key = 'E' WHERE id = 1;

UPDATE T_LIST SET partition_key = 'B' WHERE id = 1;

--7
ALTER TABLE T_RANGE MERGE PARTITIONS p1, p2 INTO PARTITION p_merged;
--8
ALTER TABLE T_RANGE SPLIT PARTITION p_merged AT (100) INTO (PARTITION p1, PARTITION p2);
--9
CREATE TABLE T_HASH_EXCHANGE (
    id NUMBER,
    data VARCHAR2(100),
    partition_key VARCHAR2(50)
);

ALTER TABLE T_HASH EXCHANGE PARTITION PARTITION_KEY WITH TABLE T_HASH_EXCHANGE;-- не работает 
--10
SELECT table_name FROM all_part_tables;
SELECT partition_name FROM user_tab_partitions WHERE table_name = 'T_RANGE';
SELECT * FROM T_RANGE PARTITION (p1);
SELECT * FROM T_INTERVAL WHERE partition_key >= TO_DATE('2023-12-01', 'YYYY-MM-DD') AND partition_key < TO_DATE('2024-01-01', 'YYYY-MM-DD');

