 SET SERVEROUTPUT ON;

BEGIN
  NULL;
END;

BEGIN
    DBMS_OUTPUT.PUT_LINE('Hello Word');
END;

SELECT keyword FROM v$reserved_words WHERE length = 1;
SELECT keyword FROM v$reserved_words WHERE length > 1 ORDER BY keyword;

BEGIN
  DECLARE
    num1 NUMBER := 10;
    num2 NUMBER := 3;
    numfloat NUMBER(5,2) := 12.34;
    f NUMBER(5,-2) := 1234.56;
    numE NUMBER := 2.5E2; 
    d DATE := SYSDATE;
    hello VARCHAR2(20) := 'Hello';
    world CHAR(10) := 'World';
    bool BOOLEAN := TRUE;
  BEGIN
    DBMS_OUTPUT.PUT_LINE('a + b = ' || (num1 + num2));
    DBMS_OUTPUT.PUT_LINE('a - b = ' || (num1 - num2));
    DBMS_OUTPUT.PUT_LINE('a * b = ' || (num1 * num2));
    DBMS_OUTPUT.PUT_LINE('a / b = ' || (num1 / num2));
    DBMS_OUTPUT.PUT_LINE('a mod b = ' || (num1 mod num2));
    DBMS_OUTPUT.PUT_LINE('f = ' || f);
    DBMS_OUTPUT.PUT_LINE('e = ' || numE);
    DBMS_OUTPUT.PUT_LINE('date = ' || d);
    DBMS_OUTPUT.PUT_LINE('h || i = ' || hello || world);
    IF bool THEN
      DBMS_OUTPUT.PUT_LINE('bool is TRUE');
    END IF;
  END;
END;

DECLARE
  c_name CONSTANT VARCHAR2(30) := 'John Doe';
  c_initial CONSTANT CHAR := 'J';
  c_age CONSTANT NUMBER := 30;
BEGIN
  DBMS_OUTPUT.PUT_LINE('Name: ' || c_name);
  DBMS_OUTPUT.PUT_LINE('Initial: ' || c_initial);
  DBMS_OUTPUT.PUT_LINE('Age: ' || (c_age - 5));
  -- c_name := 'Jane Doe'; -- Ошибка
END;

DECLARE
  l_fac FACULTY.FACULTY_NAME%TYPE;
BEGIN
  SELECT FACULTY_NAME INTO l_fac FROM FACULTY WHERE ROWNUM = 1;
  DBMS_OUTPUT.PUT_LINE('faculty: ' || l_fac);
END;

DECLARE
  l_fac_rec FACULTY%ROWTYPE;
BEGIN
  SELECT * INTO l_fac_rec FROM FACULTY WHERE ROWNUM = 1;
  DBMS_OUTPUT.PUT_LINE('faculty: ' || l_fac_rec.FACULTY);
  DBMS_OUTPUT.PUT_LINE('name: ' || l_fac_rec.FACULTY_NAME);
END;

BEGIN
    DECLARE
    a NUMBER := 10;
    BEGIN
    IF a > 0 THEN
        DBMS_OUTPUT.PUT_LINE('a is positive');
    ELSIF a < 0 THEN
        DBMS_OUTPUT.PUT_LINE('a is negative');
    ELSE
        DBMS_OUTPUT.PUT_LINE('a is zero');
    END IF;
    END;
END;

BEGIN
    DECLARE
    grade CHAR := 'B';
    BEGIN
    CASE grade
        WHEN 'A' THEN DBMS_OUTPUT.PUT_LINE('Excellent');
        WHEN 'B' THEN DBMS_OUTPUT.PUT_LINE('Good');
        WHEN 'C' THEN DBMS_OUTPUT.PUT_LINE('Average');
        ELSE DBMS_OUTPUT.PUT_LINE('Unknown grade');
    END CASE;
    END;
END;

BEGIN
    DECLARE
    i NUMBER := 1;
    BEGIN
    LOOP
        DBMS_OUTPUT.PUT_LINE(i);
        i := i + 1;
        EXIT WHEN i > 5;
    END LOOP;
    END;
END;

BEGIN
    DECLARE
    i NUMBER := 1;
    BEGIN
    WHILE i <= 5 LOOP
        DBMS_OUTPUT.PUT_LINE(i);
        i := i + 1;
    END LOOP;
    END;
END;

BEGIN
  FOR i IN 1..5 LOOP
    DBMS_OUTPUT.PUT_LINE(i);
  END LOOP;
END;

