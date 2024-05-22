set serverout on;

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

insert into TABLE_KVI values (11, 'Кофеёк', 200);
update TABLE_KVI set Cost = 300 where id = 11;
delete TABLE_KVI where id = 11;


select * from AUDITS;
--DELETE FROM audits;
--1. таблица
create table table_kvi (id NUMBER primary key, NAME varchar(50), cost NUMBER);

--2. заполнить
INSERT INTO table_kvi (id, NAME, cost) VALUES (1, 'молоко', 40);
INSERT INTO table_kvi (id, NAME, cost) VALUES (2, 'кефир', 43);
INSERT INTO table_kvi (id, NAME, cost) VALUES (3, 'батон', 39);
INSERT INTO table_kvi (id, NAME, cost) VALUES (4, 'хлеб', 34);
INSERT INTO table_kvi (id, NAME, cost) VALUES (5, 'чипсы', 70);
INSERT INTO table_kvi (id, NAME, cost) VALUES (6, 'кола', 60);
INSERT INTO table_kvi (id, NAME, cost) VALUES (7, 'конфеты', 134);
INSERT INTO table_kvi (id, NAME, cost) VALUES (8, 'курица', 142);
INSERT INTO table_kvi (id, NAME, cost) VALUES (9, 'бекон', 314);
INSERT INTO table_kvi (id, NAME, cost) VALUES (10, 'тушенка', 218);

--3. before уровня оператора
create or replace trigger insert_before
before insert on table_kvi
begin DBMS_OUTPUT.PUT_LINE('Before insert(оператор)'); 
end;
    
create or replace trigger update_before
before update on table_kvi
begin DBMS_OUTPUT.PUT_LINE('Before update(оператор)'); 
end;

create or replace trigger delete_before
before delete on table_kvi
begin DBMS_OUTPUT.PUT_LINE('Before delete(оператор)'); 
end;

--4. before уровня строк
create or replace trigger before_insert
before insert on table_kvi
for each row
begin DBMS_OUTPUT.PUT_LINE('Before insert(уровня строк)');
end; 
    
create or replace trigger before_update
before update on table_kvi
for each row
begin DBMS_OUTPUT.PUT_LINE('Before insert(уровня строк)');
end;
    
create or replace trigger before_delete
before delete on table_kvi
for each row
begin DBMS_OUTPUT.PUT_LINE('Before delete(уровня строк)');
end; 

--5. inserting deleting updating
create or replace trigger predicate_trigger
before insert or update or delete on table_kvi
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
after insert on table_kvi
begin DBMS_OUTPUT.PUT_LINE('After insert(оператор)'); 
end;

create or replace trigger after_update
after update on table_kvi
begin DBMS_OUTPUT.PUT_LINE('After update(оператор)'); 
end;
    
create or replace trigger after_delete
after delete on table_kvi
begin DBMS_OUTPUT.PUT_LINE('After delete(оператор)'); 
end;

--7. after строки
create or replace trigger insert_after
after insert on table_kvi
for each row
begin DBMS_OUTPUT.PUT_LINE('After insert(уровня строки)');
end;

create or replace trigger update_after
after update on table_kvi
for each row
begin DBMS_OUTPUT.PUT_LINE('After insert(уровня строки)');
end;
    
create or replace trigger delete_after
after delete on table_kvi
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
before insert or update or delete on TABLE_KVI
for each row
begin
if INSERTING then
insert into AUDITS(OPERATIONDATE, OPERATIONTYPE, TRIGGERNAME, data) values
(
localtimestamp,
'Insert', 
'TRIGGER_AUDIT',
:new.id || ' ' ||:new.NAME || ' ' || :new.cost
);
elsif UPDATING then
insert into AUDITS(OPERATIONDATE, OPERATIONTYPE, TRIGGERNAME, DATA)
values
(
localtimestamp, 
'Update', 
'TRIGGER_AUDIT',
:old.id || ' ' || :old.name || ' ' ||  :old.cost || ' -> ' || :new.id || ' ' || :new.NAME || ' ' ||  :new.cost
);      
elsif DELETING then
insert into AUDITS(OPERATIONDATE, OPERATIONTYPE, TRIGGERNAME, data)
values
(
localtimestamp, 
'Delete', 
'TRIGGER_AUDIT',
:old.id || ' ' || :old.NAME || ' ' ||  :old.cost
);
end if;
end;

--10. операция с нарушением целостности
INSERT INTO TABLE_KVI (ID, NAME, COST) VALUES (1, 'Молоко2', 500);

--11. drop исходную(все триггеры улетели)
drop table TABLE_KVI;

--триггер с запретом на дроп
create or replace trigger trigger_no_drop
before drop on KVICORE.schema
begin
if DICTIONARY_OBJ_NAME = 'TABLE_KVI'
then
RAISE_APPLICATION_ERROR (-20000, 'Запрещено.');
end if;
end;
    
--12. дроп аудит
drop table AUDITS;

--13. представление 
create view MY_VIEW as 
select * 
from TABLE_KVI;

create or replace trigger inst_of
instead of insert on MY_VIEW
begin
if INSERTING then
DBMS_OUTPUT.PUT_LINE('inst_of');
insert into TABLE_KVI values (:new.id, :new.name, :new.cost);
end if;
end inst_of;

--14.

CREATE OR REPLACE TRIGGER trigger1
BEFORE INSERT ON TABLE_KVI
FOR EACH ROW
BEGIN
  DBMS_OUTPUT.PUT_LINE('Trigger 1');
END;

CREATE OR REPLACE TRIGGER trigger2
BEFORE INSERT ON TABLE_KVI
FOR EACH ROW
BEGIN
  DBMS_OUTPUT.PUT_LINE('Trigger 2');
END;

CREATE OR REPLACE TRIGGER trigger3
BEFORE INSERT ON TABLE_KVI
FOR EACH ROW
BEGIN
  DBMS_OUTPUT.PUT_LINE('Trigger 3');
END;

INSERT INTO TABLE_KVI (id, NAME, Cost) VALUES (1, 'Копейка', 400);