--1. неявный курсор+точная выборка
set SERVEROUTPUT on;
declare faculty_rec faculty%rowtype;
begin
    select * into faculty_rec from faculty where faculty = 'ИДиП';
    dbms_output.put_line(faculty_rec.faculty || ' ' || faculty_rec.faculty_name);
end;

--2. неточная выборка 
declare faculty_rec faculty%rowtype;
begin
    select * into faculty_rec from faculty where faculty like 'И%';
    dbms_output.put_line(faculty_rec.faculty || ' ' || faculty_rec.faculty_name);
exception 
when others
then dbms_output.put_line('Sqlerrm: ' || sqlerrm || ' sqlcode: ' || sqlcode);
end;

--3. when to many rows
declare faculty_rec faculty%rowtype;
begin
    select * into faculty_rec from faculty where faculty like 'И%';
    dbms_output.put_line(faculty_rec.faculty || ' ' || faculty_rec.faculty_name);
exception 
when too_many_rows
then dbms_output.put_line('много строк: ' || sqlerrm || sqlcode);
end;

--4. no data found + атрибуты курсора
declare faculty_rec faculty%rowtype;
begin
    select * into faculty_rec from faculty where faculty = 'втфо';
    dbms_output.put_line(faculty_rec.faculty || ' ' || faculty_rec.faculty_name);
    if sql%isopen
    then dbms_output.put_line('курсор открыт');
    else dbms_output.put_line('курсор закрыт');
    end if;
    if sql%found
    then dbms_output.put_line('найден тру');
    else dbms_output.put_line('найден фолс');
    end if;
    if sql%notfound
    then dbms_output.put_line('не найден тру');
    else dbms_output.put_line('не найден фолс');
    end if;
    dbms_output.put_line(sql%rowcount);
exception 
when no_data_found
then dbms_output.put_line('не найдено: ' || sqlerrm || sqlcode);
end;

--5. нарушение целостности. обработка искл
select * from faculty;

begin
    update faculty 
    set faculty = 'ИТ', 
    faculty_name = 'Информационные технологии'
    where faculty = 'ТОВ';
    --commit;
    rollback;
    dbms_output.put_line('update');
exception 
when others
then dbms_output.put_line('ошибка: ' || sqlerrm || sqlcode);
end;

--6. явный курсор + teacher
declare cursor cursor_teacher is 
select * from teacher;
teach teacher.teacher%type;
teacher_name teacher.teacher_name%type;
teacher_pulpit teacher.pulpit%type;
begin
open cursor_teacher;
loop
fetch cursor_teacher into teach, teacher_name, teacher_pulpit;
exit when cursor_teacher%notfound;
dbms_output.put_line('' || cursor_teacher%rowcount || ' ' || teach || ' '
|| teacher_name || ' ' || teacher_pulpit);
end loop;
close cursor_teacher;
end;

--7. subject + while + %rowtype
declare
  cursor cursor_subject is
    select subject, subject_name, pulpit 
    from subject;
  rec_subject subject%rowtype;
begin
  open cursor_subject;
  fetch cursor_subject into rec_subject;
  while (cursor_subject%found)
  loop
    DBMS_OUTPUT.PUT_LINE('' || cursor_subject%rowcount || ' ' || rec_subject.subject || ' '
    || rec_subject.subject_name || ' ' || rec_subject.pulpit);
    fetch cursor_subject into rec_subject;
  end loop;
  close cursor_subject;
  exception when others then
    DBMS_OUTPUT.PUT_LINE(sqlerrm);
end;

--8. аудитории с какой-то вместимостью. три способа организации цикла 
declare
  cursor cursor_auditorium(minCapacity number, maxCapacity number) is 
    select auditorium, auditorium_capacity 
    from auditorium
    where auditorium_capacity >= minCapacity 
    and auditorium_capacity <= maxCapacity;
  curs_row cursor_auditorium%rowtype;
begin
  dbms_output.put_line('CAPACITY < 20');
  for aum in cursor_auditorium(0,20)
  loop
    dbms_output.put_line(' '||aum.auditorium||' '||aum.auditorium_capacity);
  end loop;
  dbms_output.put_line('CAPACITY from 21 to 30');
  for aum in cursor_auditorium(21,30)
  loop
    dbms_output.put_line(' '||aum.auditorium||' '||aum.auditorium_capacity);
  end loop;
  dbms_output.put_line('CAPACITY from 31 to 60 ');
  for aum in cursor_auditorium(31,60)
  loop
    dbms_output.put_line(' '||aum.auditorium||' '||aum.auditorium_capacity);
  end loop;
  dbms_output.put_line('CAPACITY from 61 to 80 ');
  for aum in cursor_auditorium(61,80)
  loop
    dbms_output.put_line(' '||aum.auditorium||' '||aum.auditorium_capacity);
  end loop;
  dbms_output.put_line('CAPACITY > 80 ');
  for aum in cursor_auditorium(80,1000)
  loop
    dbms_output.put_line(' '||aum.auditorium||' '||aum.auditorium_capacity);
  end loop;
  exception when others then
    dbms_output.put_line(sqlerrm);
end;

--9. ref + курсор с параметрами
declare
  type reff is ref cursor return auditorium%rowtype;
  curs reff;
  curs_row curs%rowtype;
begin
  open curs for select * from auditorium 
  where auditorium_capacity >= 0 and auditorium_capacity <= 20;
  fetch curs into curs_row;
  loop
    exit when curs%notfound;
    dbms_output.put_line(' '||curs_row.auditorium||' '||curs_row.auditorium_capacity);
    fetch curs into curs_row;
  end loop;
  close curs;
  exception when others then
    dbms_output.put_line(sqlerrm);
end;

--10. все аудитории между 40 и 80 < на 10%. явный+параметры+for+update current of
declare
  cursor cursor_auditorium(mincapacity number, maxcapacity number) is 
    select *
    from auditorium
    where auditorium_capacity >= mincapacity 
    and auditorium_capacity <= maxcapacity
    for update of auditorium_capacity;
begin
  for aud in cursor_auditorium(40, 80) loop
    update auditorium
    set auditorium_capacity = aud.auditorium_capacity * 0.9
    where current of cursor_auditorium;
    dbms_output.put_line(' ' || aud.auditorium || ' ' || aud.auditorium_capacity);
    end loop;
  rollback;
exception
  when others then
    dbms_output.put_line(sqlerrm);
end;
