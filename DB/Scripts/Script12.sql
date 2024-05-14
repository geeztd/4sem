SET SERVEROUTPUT ON;


--1. добавить столбцы
alter table TEACHER add BIRTHDAY date;
alter table TEACHER add SALARY number;
select * from TEACHER;
update TEACHER set BIRTHDAY = TO_DATE('01-12-1960', 'DD-MM-YYYY') where TEACHER = 'СМЛВ';
update TEACHER set BIRTHDAY = TO_DATE('02-12-1970', 'DD-MM-YYYY') where TEACHER = 'АКНВЧ';
update TEACHER set BIRTHDAY = TO_DATE('03-12-1980', 'DD-MM-YYYY') where TEACHER = 'КЛСНВ';
update TEACHER set BIRTHDAY = TO_DATE('04-12-1990', 'DD-MM-YYYY') where TEACHER = 'ГРМН';
update TEACHER set BIRTHDAY = TO_DATE('05-12-1990', 'DD-MM-YYYY') where TEACHER = 'ЛЩНК';
update TEACHER set BIRTHDAY = TO_DATE('11-11-1965', 'DD-MM-YYYY') where TEACHER = 'БРКВЧ';
update TEACHER set BIRTHDAY = TO_DATE('12-01-1975', 'DD-MM-YYYY') where TEACHER = 'ДДК';
update TEACHER set BIRTHDAY = TO_DATE('13-02-1985', 'DD-MM-YYYY') where TEACHER = 'КБЛ';
update TEACHER set BIRTHDAY = TO_DATE('14-03-1995', 'DD-MM-YYYY') where TEACHER = 'УРБ';
update TEACHER set BIRTHDAY = TO_DATE('15-04-1985', 'DD-MM-YYYY') where TEACHER = 'РМНК';
update TEACHER set BIRTHDAY = TO_DATE('21-05-1970', 'DD-MM-YYYY') where TEACHER = 'ПСТВЛВ';
update TEACHER set BIRTHDAY = TO_DATE('22-06-1980', 'DD-MM-YYYY') where TEACHER = '?';
update TEACHER set BIRTHDAY = TO_DATE('23-07-1990', 'DD-MM-YYYY') where TEACHER = 'ГРН';
update TEACHER set BIRTHDAY = TO_DATE('24-08-1980', 'DD-MM-YYYY') where TEACHER = 'ЖЛК';
update TEACHER set BIRTHDAY = TO_DATE('25-09-1990', 'DD-MM-YYYY') where TEACHER = 'БРТШВЧ';
update TEACHER set BIRTHDAY = TO_DATE('26-10-1985', 'DD-MM-YYYY') where TEACHER = 'ЮДНКВ';
update TEACHER set BIRTHDAY = TO_DATE('27-11-1990', 'DD-MM-YYYY') where TEACHER = 'БРНВСК';
update TEACHER set BIRTHDAY = TO_DATE('28-12-1965', 'DD-MM-YYYY') where TEACHER = 'НВРВ';
update TEACHER set BIRTHDAY = TO_DATE('29-12-1970', 'DD-MM-YYYY') where TEACHER = 'РВКЧ';
update TEACHER set BIRTHDAY = TO_DATE('30-01-1975', 'DD-MM-YYYY') where TEACHER = 'ДМДК';
update TEACHER set BIRTHDAY = TO_DATE('29-03-1983', 'DD-MM-YYYY') where TEACHER = 'МШКВСК';
update TEACHER set BIRTHDAY = TO_DATE('08-10-1982', 'DD-MM-YYYY') where TEACHER = 'ЛБХ';
update TEACHER set BIRTHDAY = TO_DATE('30-11-1991', 'DD-MM-YYYY') where TEACHER = 'ЗВГЦВ';
update TEACHER set BIRTHDAY = TO_DATE('16-12-1982', 'DD-MM-YYYY') where TEACHER = 'БЗБРДВ';
update TEACHER set BIRTHDAY = TO_DATE('17-04-1973', 'DD-MM-YYYY') where TEACHER = 'ПРКПЧК';
update TEACHER set BIRTHDAY = TO_DATE('14-04-1980', 'DD-MM-YYYY') where TEACHER = 'НСКВЦ';
update TEACHER set BIRTHDAY = TO_DATE('18-10-1990', 'DD-MM-YYYY') where TEACHER = 'МХВ';
update TEACHER set BIRTHDAY = TO_DATE('04-05-1966', 'DD-MM-YYYY') where TEACHER = 'ЕЩНК';
update TEACHER set BIRTHDAY = TO_DATE('11-11-1988', 'DD-MM-YYYY') where TEACHER = 'ЖРСК';
update TEACHER set SALARY = 1234 where TEACHER = 'СМЛВ';
update TEACHER set SALARY = 123 where TEACHER = 'АКНВЧ';
update TEACHER set SALARY = 432 where TEACHER = 'КЛСНВ';
update TEACHER set SALARY = 465 where TEACHER = 'ГРМН';
update TEACHER set SALARY = 678 where TEACHER = 'ЛЩНК';
update TEACHER set SALARY = 423 where TEACHER = 'БРКВЧ';
update TEACHER set SALARY = 987 where TEACHER = 'ДДК';
update TEACHER set SALARY = 324 where TEACHER = 'КБЛ';
update TEACHER set SALARY = 534 where TEACHER = 'УРБ';
update TEACHER set SALARY = 131 where TEACHER = 'РМНК';
update TEACHER set SALARY = 364 where TEACHER = 'ПСТВЛВ';
update TEACHER set SALARY = 353 where TEACHER = '?';
update TEACHER set SALARY = 576 where TEACHER = 'ГРН';
update TEACHER set SALARY = 890 where TEACHER = 'ЖЛК';
update TEACHER set SALARY = 900 where TEACHER = 'БРТШВЧ';
update TEACHER set SALARY = 1322 where TEACHER = 'ЮДНКВ';
update TEACHER set SALARY = 900 where TEACHER = 'БРНВСК';
update TEACHER set SALARY = 800 where TEACHER = 'НВРВ';
update TEACHER set SALARY = 700 where TEACHER = 'РВКЧ';
update TEACHER set SALARY = 800 where TEACHER = 'ДМДК';
update TEACHER set SALARY = 900 where TEACHER = 'МШКВСК';
update TEACHER set SALARY = 120 where TEACHER = 'ЛБХ';
update TEACHER set SALARY = 100 where TEACHER = 'ЗВГЦВ';
update TEACHER set SALARY = 105 where TEACHER = 'БЗБРДВ';
update TEACHER set SALARY = 125 where TEACHER = 'ПРКПЧК';
update TEACHER set SALARY = 800 where TEACHER = 'НСКВЦ';
update TEACHER set SALARY = 715 where TEACHER = 'МХВ';
update TEACHER set SALARY = 905 where TEACHER = 'ЕЩНК';
update TEACHER set SALARY = 850 where TEACHER = 'ЖРСК';

--2. список преподов которые родились в пн
select teacher, 
       to_char(birthday, 'dd') as day,
       to_char(birthday, 'mm') as month,
       to_char(birthday, 'yyyy') as year,
       to_char(birthday, 'day') as weekday
from teacher;
----------------------------
select regexp_substr(teacher_name,'(\S+)',1, 1)||' '||
substr(regexp_substr(teacher_name,'(\S+)',1, 2),1, 1)||'. '||
substr(regexp_substr(teacher_name,'(\S+)',1, 3),1, 1)||'. ' as ФИО
from teacher where TO_CHAR((birthday), 'd') = 1;

--3. представление
create view teachers_birthdays as
select teacher, to_char(birthday, 'dd/mm/yyyy') as birthdate
from teacher
where extract(month from birthday) = extract(month from sysdate) + 1;

select * from teachers_birthdays;

--4. в каждом месяце
create view teachers_birthdays_every_month as
select to_char(birthday, 'month') as month_name, count(*) as count
from teacher group by to_char(birthday, 'month');

select * from teachers_birthdays_every_month;

--5. курсор и юбилей
DECLARE
cursor teach_curs(teacher%rowtype) 
return teacher%rowtype is
select * from teacher
where mod((to_char(sysdate,'yyyy') - to_char(birthday, 'yyyy') + 1), 10) = 0;

DECLARE
  cursor teach_curs is
    select * from teacher
    where mod((to_char(sysdate,'yyyy') - to_char(birthday, 'yyyy') + 1), 10) = 0;
  
  teacher_rec teacher%rowtype;

BEGIN
  OPEN teach_curs;
    LOOP
    FETCH teach_curs into teacher_rec;
    EXIT WHEN teach_curs%NOTFOUND;
    
    DBMS_OUTPUT.PUT_LINE('Имя: ' || teacher_rec.TEACHER_NAME);
    DBMS_OUTPUT.PUT_LINE('Дата рождения: ' || teacher_rec.birthday);
    DBMS_OUTPUT.PUT_LINE('Исполнится: ' || (EXTRACT(YEAR FROM  SYSDATE())- EXTRACT(YEAR FROM teacher_rec.birthday)+1));
  END LOOP;
  CLOSE teach_curs;
END;


--6. курсор и зарплата
--средняя
set serveroutput on
declare
cursor dept_avg_cursor is
select pulpit, floor(avg(salary)) as avg_salary
from teacher
group by pulpit;
v_dept_rec dept_avg_cursor%rowtype;
v_total_avg_salary number := 0;
begin
open dept_avg_cursor;
loop
fetch dept_avg_cursor into v_dept_rec;
exit when dept_avg_cursor%notfound;
dbms_output.put_line('Кафедра: ' || v_dept_rec.pulpit);
dbms_output.put_line('Средняя зарплата: ' || v_dept_rec.avg_salary);
dbms_output.put_line('------------------------');
v_total_avg_salary := v_total_avg_salary + v_dept_rec.avg_salary;
end loop;
close dept_avg_cursor;
dbms_output.put_line('Итоговая средняя зарплата по всем факультетам: ' || v_total_avg_salary);
end;

--средняя для факультета
declare
cursor dept_avg_cursor is
select p.faculty, round(avg(t.salary)) as avg_salary
from teacher t
join pulpit p on t.pulpit = p.pulpit
group by p.faculty;
v_dept_rec dept_avg_cursor%rowtype;
v_total_avg_salary number := 0;
begin
open dept_avg_cursor;
loop
fetch dept_avg_cursor into v_dept_rec;
exit when dept_avg_cursor%notfound;
dbms_output.put_line('Факультет: ' || v_dept_rec.faculty);
dbms_output.put_line('Средняя зарплата: ' || v_dept_rec.avg_salary);
dbms_output.put_line('------------------------');
v_total_avg_salary := v_total_avg_salary + v_dept_rec.avg_salary;
end loop;
close dept_avg_cursor;
dbms_output.put_line('Итоговая средняя зарплата по всем факультетам: ' || v_total_avg_salary);
end;

--7. деление на ноль
declare
v_dividend number := 10;
v_divisor number := 0;
v_result number;
begin
if v_divisor = 0 then
raise_application_error(-20001, 'Ошибка: деление на 0 недопустимо.');
end if;
v_result := v_dividend / v_divisor;
dbms_output.put_line('Результат деления: ' || v_result);
exception
when zero_divide then
dbms_output.put_line('Ошибка: деление на 0.');
when others then
dbms_output.put_line('Ошибка: ' || sqlerrm);
end;

--8. препод по коду
declare
v_teacher_name varchar2(100);
v_teacher_code char(20) := 'кто?';
begin
select teacher_name into v_teacher_name
from teacher
where teacher = v_teacher_code;
dbms_output.put_line('найден преподаватель: ' || v_teacher_name);
exception
when no_data_found then
dbms_output.put_line('преподаватель не найден!');
end;

--9. 
DECLARE
  ex_custom_exception EXCEPTION;
  ex_other_exception EXCEPTION;
  PRAGMA EXCEPTION_INIT(ex_custom_exception, -20001);
BEGIN
  DBMS_OUTPUT.PUT_LINE('Начало основного блока');
  DECLARE
    v_nested_block_exception EXCEPTION;
    PRAGMA EXCEPTION_INIT(v_nested_block_exception, -20001);
  BEGIN
    DBMS_OUTPUT.PUT_LINE('Начало вложенного блока');
    RAISE v_nested_block_exception;
    DBMS_OUTPUT.PUT_LINE('Конец вложенного блока');
  EXCEPTION
    WHEN ex_custom_exception THEN
      DBMS_OUTPUT.PUT_LINE('Обработка исключения в основном блоке: ex_custom_exception');
    WHEN ex_other_exception THEN
      DBMS_OUTPUT.PUT_LINE('Обработка исключения в основном блоке: ex_other_exception');
    WHEN OTHERS THEN
      DBMS_OUTPUT.PUT_LINE('Обработка исключения в основном блоке: OTHERS');
  END;
  DBMS_OUTPUT.PUT_LINE('Конец основного блока');
EXCEPTION
  WHEN ex_custom_exception THEN
    DBMS_OUTPUT.PUT_LINE('Обработка исключения в основном блоке: ex_custom_exception');
  WHEN ex_other_exception THEN
    DBMS_OUTPUT.PUT_LINE('Обработка исключения в основном блоке: ex_other_exception');
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Обработка исключения в основном блоке: OTHERS');
END;

--10.
declare
v_max_value number;
begin
select max(salary) into v_max_value
from teacher
where teacher = 'кто?';
dbms_output.put_line('максимальная зарплата: ' || v_max_value);
exception
when no_data_found then
dbms_output.put_line('нет данных');
end;