SELECT tablespace_name FROM dba_tablespaces;

CREATE TABLESPACE KVI_QDATA DATAFILE 'KVI_QDATA.DBF'
SIZE 10M AUTOEXTEND
ON NEXT 10M MAXSIZE 
UNLIMITED OFFLINE;

ALTER TABLESPACE KVI_QDATA ONLINE;
ALTER USER KVICORE QUOTA 2M ON KVI_QDATA;
-- CREATE TABLE KVI_T1 (
--     column1 NUMBER PRIMARY KEY,
--     column2 VARCHAR2(50)
-- );
-- INSERT INTO KVI_T1 VALUES (1, 'Row 1');
-- INSERT INTO KVI_T1 VALUES (2, 'Row 2');
-- INSERT INTO KVI_T1 VALUES (3, 'Row 3');

SELECT segment_name FROM dba_segments WHERE tablespace_name = 'KVI_QDATA';
SELECT segment_name FROM dba_segments WHERE segment_type = 'TABLE' AND segment_name = 'KVI_T1';
SELECT segment_name FROM dba_segments WHERE tablespace_name = 'KVI_QDATA' AND segment_name != 'KVI_T1';
-- DROP TABLE KVI_T1;

SELECT segment_name FROM dba_segments WHERE tablespace_name = 'KVI_QDATA';
-- FLASHBACK TABLE KVI_T1 TO BEFORE DROP;

-- DECLARE
--     i NUMBER := 1;
-- BEGIN
--     FOR i IN 1..10000 LOOP
--         INSERT INTO KVI_T1 VALUES (i, 'Data ' || i);
--     END LOOP;
-- END;
-- SELECT ROWID FROM KVI_T1;
-- SELECT ORA_ROWSCN AS ROWSCN FROM KVI_T1;

SELECT extent_id, bytes/1024 AS kb, bytes FROM dba_extents WHERE segment_name = 'KVI_T1';
SELECT tablespace_name, segment_name, extent_id, file_id, block_id, bytes FROM dba_extents;

DROP TABLESPACE KVI_QDATA INCLUDING CONTENTS AND DATAFILES;



