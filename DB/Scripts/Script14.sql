set serveroutput on;

alter pluggable database gdv_pdb open;

select OBJECT_NAME, STATUS from USER_OBJECTS where OBJECT_TYPE = 'TRIGGER';

/*
drop table dormitory;
drop table audits;
drop trigger insert_before;
drop trigger update_before;
drop trigger delete_before;
drop trigger before_insert;
drop trigger before_update;
drop trigger before_delete;
drop trigger predicate_trigger;
drop trigger after_insert;
drop trigger after_update;
drop trigger after_delete;
drop trigger insert_after;
drop trigger update_after;
drop trigger delete_after;
drop trigger trigger_audit;
*/

insert into dormitory values (11, 'Не копейка', 200);
update dormitory set CAPACITY = 300 where NUM = 11;
delete dormitory where NUM = 11;


select * from AUDITS;

--1. таблица
create table dormitory (NUM NUMBER primary key, NON_OFFICIAL_NAME varchar(50), CAPACITY NUMBER);

--2. заполнить
INSERT INTO dormitory (NUM, NON_OFFICIAL_NAME, CAPACITY) VALUES (1, 'Копейка', 400);
INSERT INTO dormitory (NUM, NON_OFFICIAL_NAME, CAPACITY) VALUES (2, 'Двойка', 400);
INSERT INTO dormitory (NUM, NON_OFFICIAL_NAME, CAPACITY) VALUES (3, 'Тройка', 400);
INSERT INTO dormitory (NUM, NON_OFFICIAL_NAME, CAPACITY) VALUES (4, 'Четверка', 1000);
INSERT INTO dormitory (NUM, NON_OFFICIAL_NAME, CAPACITY) VALUES (5, 'Пятерка', 1000);
INSERT INTO dormitory (NUM, NON_OFFICIAL_NAME, CAPACITY) VALUES (6, 'Гурского', 1111);
INSERT INTO dormitory (NUM, NON_OFFICIAL_NAME, CAPACITY) VALUES (7, 'Петры', 1234);
INSERT INTO dormitory (NUM, NON_OFFICIAL_NAME, CAPACITY) VALUES (8, 'Dorm A', 542);
INSERT INTO dormitory (NUM, NON_OFFICIAL_NAME, CAPACITY) VALUES (9, 'Dorm B', 414);
INSERT INTO dormitory (NUM, NON_OFFICIAL_NAME, CAPACITY) VALUES (10, 'Dorm C', 718);

--3. before уровня оператора
create or replace trigger insert_before
before insert on dormitory
begin DBMS_OUTPUT.PUT_LINE('Before insert(оператор)'); 
end;
    
create or replace trigger update_before
before update on dormitory
begin DBMS_OUTPUT.PUT_LINE('Before update(оператор)'); 
end;

create or replace trigger delete_before
before delete on dormitory
begin DBMS_OUTPUT.PUT_LINE('Before delete(оператор)'); 
end;

--4. before уровня строк
create or replace trigger before_insert
before insert on dormitory
for each row
begin DBMS_OUTPUT.PUT_LINE('Before insert(уровня строк)');
end; 
    
create or replace trigger before_update
before update on dormitory
for each row
begin DBMS_OUTPUT.PUT_LINE('Before insert(уровня строк)');
end;
    
create or replace trigger before_delete
before delete on dormitory
for each row
begin DBMS_OUTPUT.PUT_LINE('Before delete(уровня строк)');
end; 

--5. inserting deleting updating
create or replace trigger predicate_trigger
before insert or update or delete on dormitory
begin
if INSERTING then
DBMS_OUTPUT.PUT_LINE('Before insert(предикат)');
elsif UPDATING then
DBMS_OUTPUT.PUT_LINE('Before update(предикат)');
elsif DELETING then
DBMS_OUTPUT.PUT_LINE('Before delete(предикат)');
end if;
end;

--6. alter триггер опер
create or replace trigger after_insert
after insert on dormitory
begin DBMS_OUTPUT.PUT_LINE('After insert(оператор)'); 
end;

create or replace trigger after_update
after update on dormitory
begin DBMS_OUTPUT.PUT_LINE('After update(оператор)'); 
end;
    
create or replace trigger after_delete
after delete on dormitory
begin DBMS_OUTPUT.PUT_LINE('After delete(оператор)'); 
end;

--7. after строки
create or replace trigger insert_after
after insert on dormitory
for each row
begin DBMS_OUTPUT.PUT_LINE('After insert(уровня строки)');
end;

create or replace trigger update_after
after update on dormitory
for each row
begin DBMS_OUTPUT.PUT_LINE('After insert(уровня строки)');
end;
    
create or replace trigger delete_after
after delete on dormitory
for each row
begin DBMS_OUTPUT.PUT_LINE('After delete(уровня строки)');
end; 

--8. audit 
create table AUDITS
(
OPERATIONDATE timestamp, 
OPERATIONTYPE varchar2(50), 
TRIGGERNAME varchar2(30),
DATA varchar2(300)   
);

--9. изменить триггеры
create or replace trigger trigger_audit
before insert or update or delete on AUDITS
for each row
begin
if INSERTING then
DBMS_OUTPUT.PUT_LINE('Before insert(предикат)' );
insert into AUDITS(OPERATIONDATE, OPERATIONTYPE, TRIGGERNAME, data) values
(
localtimestamp,
'Insert', 
'TRIGGER_AUDIT',
:new.NUM || ' ' ||:new.NON_OFFICIAL_NAME || ' ' || :new.CAPACITY
);
elsif UPDATING then
DBMS_OUTPUT.PUT_LINE('Before update(предикат)');
insert into AUDITS(OPERATIONDATE, OPERATIONTYPE, TRIGGERNAME, DATA)
values
(
localtimestamp, 
'Update', 
'TRIGGER_AUDIT',
:old.NUM || ' ' || :old.NON_OFFICIAL_NAME || ' ' ||  :old.CAPACITY || ' -> ' || :new.NUM || ' ' || :new.NON_OFFICIAL_NAME || ' ' ||  :new.CAPACITY
);      
elsif DELETING then
DBMS_OUTPUT.PUT_LINE('Before delete(предикат)');
insert into AUDITS(OPERATIONDATE, OPERATIONTYPE, TRIGGERNAME, data)
values
(
localtimestamp, 
'Delete', 
'TRIGGER_AUDIT',
:old.NUM || ' ' || :old.NON_OFFICIAL_NAME || ' ' ||  :old.CAPACITY
);
end if;
end;

--10. операция с нарушением целостности
INSERT INTO dormitory (NUM, NON_OFFICIAL_NAME, CAPACITY) VALUES (1, 'Новая Копейка', 500);

--11. drop исходную(все триггеры улетели)
drop table dormitory;

--триггер с запретом на дроп
create or replace trigger trigger_ne_drop
before drop on SYSTEM.schema
begin
if DICTIONARY_OBJ_NAME = 'dormitory'
then
RAISE_APPLICATION_ERROR (-20000, 'Запрещено.');
end if;
end;
    
--12. дроп аудит
drop table AUDITS;

--13. представление 
create view MY_VIEW as 
select * 
from dormitory;

create or replace trigger inst_of
instead of insert on MY_VIEW
begin
if INSERTING then
DBMS_OUTPUT.PUT_LINE('inst_of');
insert into dormitory values (12, 'Четырик', 1234);
end if;
end inst_of;

--14.

CREATE OR REPLACE TRIGGER trigger1
BEFORE INSERT ON dormitory
FOR EACH ROW
BEGIN
  DBMS_OUTPUT.PUT_LINE('Trigger 1');
END;

CREATE OR REPLACE TRIGGER trigger2
BEFORE INSERT ON dormitory
FOR EACH ROW
BEGIN
  DBMS_OUTPUT.PUT_LINE('Trigger 2');
END;

CREATE OR REPLACE TRIGGER trigger3
BEFORE INSERT ON dormitory
FOR EACH ROW
BEGIN
  DBMS_OUTPUT.PUT_LINE('Trigger 3');
END;

INSERT INTO dormitory (NUM, NON_OFFICIAL_NAME, CAPACITY) VALUES (1, 'Копейка', 400);