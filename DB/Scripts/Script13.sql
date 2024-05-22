set SERVEROUT on;

--список преподавателей на кафедре
create or replace procedure GET_TEACHERS (PCODE TEACHER.PULPIT%type) is
cursor curs is
select TEACHER_NAME, TEACHER from TEACHER 
where PULPIT = PCODE;
T_NAME TEACHER.TEACHER_NAME%type;
T_CODE TEACHER.TEACHER%type;
begin
open curs;
LOOP
DBMS_OUTPUT.PUT_LINE(curs%rowcount || ' ' || T_CODE || ' ' || T_NAME);
FETCH curs into T_NAME, T_CODE;
EXIT when curs%NOTFOUND;
end LOOP;
close curs;
end;

begin
    GET_TEACHERS('ИСиТ');
end;

--2.число преподавателей на кафедре
create or replace function GET_NUM_TEACHERS(PCODE TEACHER.PULPIT%type)
return number is
TEACH_COUNT number;
begin
select COUNT(*) into TEACH_COUNT 
from TEACHER where PULPIT = PCODE;
return TEACH_COUNT;
end;

begin
  DBMS_OUTPUT.PUT_LINE(GET_NUM_TEACHERS('ИСиТ'));
end;

--3. список преподов на факультете
CREATE OR REPLACE PROCEDURE PGET_TEACHERS(FCODE Sys.FACULTY.FACULTY%TYPE) IS
BEGIN
  FOR teacher_rec IN (SELECT * FROM TEACHER WHERE PULPIT IN (SELECT PULPIT FROM PULPIT WHERE FACULTY = FCODE)) LOOP
    DBMS_OUTPUT.PUT_LINE('Code: ' || teacher_rec.TEACHER);
    DBMS_OUTPUT.PUT_LINE('Teacher Name: ' || teacher_rec.TEACHER_NAME);
    DBMS_OUTPUT.PUT_LINE('Pulpit: ' || teacher_rec.PULPIT);
    DBMS_OUTPUT.PUT_LINE('------------------------');
  END LOOP;
END;

BEGIN
  PGET_TEACHERS('ТОВ');
END;

--список дисциплин за кафедрой
CREATE OR REPLACE PROCEDURE GET_SUBJECTS(PCODE SUBJECT.PULPIT%TYPE) IS
BEGIN
  FOR subject_rec IN (SELECT * FROM SUBJECT WHERE PULPIT = PCODE) LOOP
    DBMS_OUTPUT.PUT_LINE('Code: ' || subject_rec.SUBJECT);
    DBMS_OUTPUT.PUT_LINE('Subject Name: ' || subject_rec.SUBJECT_NAME);
    DBMS_OUTPUT.PUT_LINE('Pulpit: ' || subject_rec.PULPIT);
    DBMS_OUTPUT.PUT_LINE('------------------------');
  END LOOP;
END;

BEGIN
  GET_SUBJECTS('ИСиТ');
END;

--4. количество преподов на факультете
CREATE OR REPLACE FUNCTION FGET_NUM_TEACHERS(FCODE FACULTY.FACULTY%TYPE) RETURN NUMBER IS
  teacher_count NUMBER;
BEGIN
  SELECT COUNT(*) INTO teacher_count FROM TEACHER WHERE PULPIT IN (SELECT PULPIT FROM PULPIT WHERE FACULTY = FCODE);
  RETURN teacher_count;
END;

DECLARE
  faculty_code FACULTY.FACULTY%TYPE := 'ТОВ';
  num_teachers NUMBER;
BEGIN
  num_teachers := FGET_NUM_TEACHERS(faculty_code);
  DBMS_OUTPUT.PUT_LINE('Number of Teachers: ' || num_teachers);
END;
--функция с количеством дисциплин
CREATE OR REPLACE FUNCTION FGET_NUM_SUBJECTS(PCODE SUBJECT.PULPIT%TYPE) RETURN NUMBER IS
  subject_count NUMBER;
BEGIN
  SELECT COUNT(*) INTO subject_count FROM SUBJECT WHERE PULPIT = PCODE;
  RETURN subject_count;
END;

DECLARE
  pulpit_code SUBJECT.PULPIT%TYPE := 'ИСиТ';
  num_subjects NUMBER;
BEGIN
  num_subjects := FGET_NUM_SUBJECTS(pulpit_code);
  DBMS_OUTPUT.PUT_LINE('Number of Subjects: ' || num_subjects);
END;

--5. пакет с процедурами
CREATE OR REPLACE PACKAGE TEACHERS IS
  PROCEDURE GET_TEACHERS(FCODE FACULTY.FACULTY%TYPE);
  PROCEDURE GET_SUBJECTS(PCODE SUBJECT.PULPIT%TYPE);
  FUNCTION GET_NUM_TEACHERS(FCODE FACULTY.FACULTY%TYPE) RETURN NUMBER;
  FUNCTION GET_NUM_SUBJECTS(PCODE SUBJECT.PULPIT%TYPE) RETURN NUMBER;
END TEACHERS;

CREATE OR REPLACE PACKAGE BODY TEACHERS IS
  PROCEDURE GET_TEACHERS(FCODE FACULTY.FACULTY%TYPE) IS
  BEGIN
    FOR teacher_rec IN (SELECT * FROM TEACHER WHERE PULPIT IN (SELECT PULPIT FROM PULPIT WHERE FACULTY = FCODE)) LOOP
      DBMS_OUTPUT.PUT_LINE('Code: ' || teacher_rec.TEACHER);
      DBMS_OUTPUT.PUT_LINE('Teacher Name: ' || teacher_rec.TEACHER_NAME);
      DBMS_OUTPUT.PUT_LINE('Pulpit: ' || teacher_rec.PULPIT);
      DBMS_OUTPUT.PUT_LINE('------------------------');
    END LOOP;
  END;
  PROCEDURE GET_SUBJECTS(PCODE SUBJECT.PULPIT%TYPE) IS
  BEGIN
    FOR subject_rec IN (SELECT * FROM SUBJECT WHERE PULPIT = PCODE) LOOP
      DBMS_OUTPUT.PUT_LINE('Subject ID: ' || subject_rec.SUBJECT);
      DBMS_OUTPUT.PUT_LINE('Subject Name: ' || subject_rec.SUBJECT_NAME);
      DBMS_OUTPUT.PUT_LINE('Pulpit: ' || subject_rec.PULPIT);
      DBMS_OUTPUT.PUT_LINE('------------------------');
    END LOOP;
  END;
   FUNCTION GET_NUM_TEACHERS(FCODE FACULTY.FACULTY%TYPE) RETURN NUMBER IS
    teacher_count NUMBER;
  BEGIN
    SELECT COUNT(*) INTO teacher_count FROM TEACHER WHERE PULPIT IN (SELECT PULPIT FROM PULPIT WHERE FACULTY = FCODE);
    RETURN teacher_count;
  END;
  FUNCTION GET_NUM_SUBJECTS(PCODE SUBJECT.PULPIT%TYPE) RETURN NUMBER IS
    subject_count NUMBER;
  BEGIN
    SELECT COUNT(*) INTO subject_count FROM SUBJECT WHERE PULPIT = PCODE;
    RETURN subject_count;
  END;
END TEACHERS;

BEGIN
  TEACHERS.GET_TEACHERS('ТОВ');
  TEACHERS.GET_SUBJECTS('ИСиТ');

  DECLARE
    faculty_code FACULTY.FACULTY%TYPE := 'ТОВ';
    num_teachers NUMBER;
    pulpit_code SUBJECT.PULPIT%TYPE := 'ИСиТ';
    num_subjects NUMBER;
  BEGIN
    num_teachers := TEACHERS.GET_NUM_TEACHERS(faculty_code);
    DBMS_OUTPUT.PUT_LINE('Number of Teachers: ' || num_teachers);
    num_subjects := TEACHERS.GET_NUM_SUBJECTS(pulpit_code);
    DBMS_OUTPUT.PUT_LINE('Number of Subjects: ' || num_subjects);
  END;
END;