ALTER SESSION SET "_oracle_script" = TRUE; 

SELECT file_name, tablespace_name FROM dba_data_files;
SELECT file_name, tablespace_name FROM cdb_data_files;
SELECT name FROM v$pdbs;

ALTER PLUGGABLE DATABASE KVI_PBD OPEN;

CREATE TABLESPACE U1_TS
DATAFILE 'U1_TS.dbf'
SIZE 7M
AUTOEXTEND ON NEXT 5M
MAXSIZE 30M;

CREATE TEMPORARY TABLESPACE U1_TS_TEMP
TEMPFILE 'U1_TS_TEMP.dbf'
SIZE 5M
AUTOEXTEND ON NEXT 3M
MAXSIZE 20M;

CREATE ROLE U1_KVI_Role;
GRANT CREATE SESSION TO U1_KVI_Role;
GRANT CREATE TABLE TO U1_KVI_Role;
GRANT CREATE VIEW TO U1_KVI_Role;
GRANT CREATE PROCEDURE TO U1_KVI_Role;

CREATE PROFILE U1_PF LIMIT
    PASSWORD_LIFE_TIME 180
    SESSIONS_PER_USER 3
    FAILED_LOGIN_ATTEMPTS 7
    PASSWORD_LOCK_TIME 1
    PASSWORD_REUSE_TIME 10
    PASSWORD_GRACE_TIME DEFAULT
    CONNECT_TIME 180
    IDLE_TIME 30;

CREATE USER U1_KVI_PDB
IDENTIFIED BY 1234
DEFAULT TABLESPACE U1_TS
TEMPORARY TABLESPACE U1_TS_TEMP
PROFILE U1_PF
ACCOUNT UNLOCK;

GRANT U1_KVI_Role TO U1_KVI_PDB;

ALTER USER U1_KVI_PDB QUOTA 2M on U1_TS; 
DROP USER C##KVI ;
CREATE USER C##KVI IDENTIFIED BY 1234;
GRANT CREATE SESSION TO C##KVI;
GRANT CREATE TABLE TO C##KVI;
SELECT SYS_CONTEXT('USERENV', 'CON_NAME') AS CON_NAME FROM DUAL;
SELECT * from v$pdbs;
SELECT tablespace_name
FROM dba_tablespaces;

SELECT file_name, tablespace_name
FROM dba_data_files;

SELECT * FROM dba_role_privs;

SELECT profile FROM dba_profiles;


SELECT * FROM dba_role_privs
WHERE grantee IN (SELECT username FROM dba_users);

DROP USER C##KVI;
CREATE USER C##KVI IDENTIFIED BY 1234
CONTAINER = ALL;
GRANT CREATE SESSION TO C##KVI;
GRANT CREATE TABLE TO C##KVI;

SELECT * FROM V$INSTANCE;

ALTER PLUGGABLE DATABASE KVI_PBD OPEN;
ALTER USER U1_KVI_PDB QUOTA 2M on U1_TS; 
ALTER USER C##KVI QUOTA 2M on U1_TS; 


select NAME,PDB,CON_ID from v$services;
SELECT * FROM all_objects WHERE owner = 'C##KVI';
SELECT * FROM all_objects WHERE owner = 'U1_KVI_PDB';

