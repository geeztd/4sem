ALTER SESSION SET "_oracle_script" = TRUE; 

CREATE TABLESPACE TS_KVI
DATAFILE 'TS_KVI.dbf'
SIZE 7M
AUTOEXTEND ON NEXT 5M
MAXSIZE 30M;

CREATE TEMPORARY TABLESPACE TS_KVI_TEMP
TEMPFILE 'TS_KVI_TEMP.dbf'
SIZE 5M
AUTOEXTEND ON NEXT 3M
MAXSIZE 20M;

SELECT tablespace_name FROM dba_tablespaces;
SELECT file_name FROM dba_data_files;

CREATE ROLE RL_KVICORE;
GRANT CREATE SESSION TO RL_KVICORE;
GRANT CREATE TABLE TO RL_KVICORE;
GRANT CREATE VIEW TO RL_KVICORE;
GRANT CREATE PROCEDURE TO RL_KVICORE;

SELECT role FROM dba_roles WHERE role = 'RL_KVICORE';
SELECT * FROM dba_sys_privs WHERE grantee = 'RL_KVICORE';

CREATE PROFILE PF_KVICORE LIMIT
    PASSWORD_LIFE_TIME 180
    SESSIONS_PER_USER 3
    FAILED_LOGIN_ATTEMPTS 7
    PASSWORD_LOCK_TIME 1
    PASSWORD_REUSE_TIME 10
    PASSWORD_GRACE_TIME DEFAULT
    CONNECT_TIME 180
    IDLE_TIME 30;

SELECT profile FROM dba_profiles;
SELECT * FROM dba_profiles WHERE profile = 'PF_KVICORE';
SELECT * FROM dba_profiles WHERE profile = 'DEFAULT';

CREATE USER KVICORE
IDENTIFIED BY 1111
DEFAULT TABLESPACE TS_KVI
TEMPORARY TABLESPACE TS_KVI_TEMP
PROFILE PF_KVICORE
ACCOUNT UNLOCK
PASSWORD EXPIRE;

GRANT RL_KVICORE TO KVICORE;
SELECT * from DBA_USERS WHERE USERNAME = 'KVICORE';

ALTER USER KVICORE QUOTA 2M on TS_KVI; 
ALTER TABLESPACE TS_KVI OFFLINE;
ALTER TABLESPACE TS_KVI ONLINE;





