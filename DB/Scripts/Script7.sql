SELECT SUM(value) AS "SGA Size (bytes)" FROM v$sga;

SELECT * FROM v$sga;

SELECT COMPONENT, CURRENT_SIZE, GRANULE_SIZE FROM v$sga_dynamic_components
WHERE COMPONENT IN ('shared pool', 'large pool', 'java pool', 'streams pool');

SELECT CURRENT_SIZE FROM v$sga_dynamic_free_memory;

SELECT COMPONENT, CURRENT_SIZE, GRANULE_SIZE FROM v$sga_dynamic_components
WHERE COMPONENT LIKE '%cache';

CREATE TABLE KVITKEEP (
    id NUMBER PRIMARY KEY,
    name VARCHAR2(50)
) STORAGE (BUFFER_POOL KEEP);

CREATE TABLE KVITDEF (
    id NUMBER PRIMARY KEY,
    name VARCHAR2(50)
) STORAGE (BUFFER_POOL DEFAULT);

SELECT segment_name, segment_type, bytes
FROM dba_segments;


SELECT GROUP#, BYTES FROM v$log;

SELECT SUM(bytes) AS "Free Memory (bytes)" FROM v$sgastat
WHERE pool = 'large pool' AND name = 'free memory';

SELECT name, value FROM v$parameter
WHERE name IN ('shared_servers', 'dedicated_server');

SELECT program FROM v$session WHERE type = 'BACKGROUND';

SELECT program FROM v$session WHERE type = 'USER';

SELECT COUNT(*) AS "DBWn Processes" FROM v$process
WHERE program LIKE 'DBW%';

SELECT DISTINCT NAME, NETWORK_NAME FROM v$active_services;

SELECT * FROM v$dispatcher;


