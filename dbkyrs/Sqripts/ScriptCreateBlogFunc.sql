CREATE OR REPLACE FUNCTION AddBlog (
  p_user_id IN NUMBER,
  p_name IN VARCHAR2,
  p_description IN VARCHAR2
) RETURN NUMBER AS
  p_blog_id NUMBER;
BEGIN
  INSERT INTO Blog (blog_id, user_id, name, Discription)
  VALUES (blog_seq.NEXTVAL, p_user_id, p_name, p_description)
  RETURNING blog_id INTO p_blog_id;
  DBMS_OUTPUT.PUT_LINE('Блог добавлен');
  RETURN p_blog_id;

  EXCEPTION
    WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Ошибка: ' || SQLERRM);
    RETURN -1;
END;

CREATE OR REPLACE FUNCTION UpdBlog (
  p_blog_id IN NUMBER,
  p_name IN VARCHAR2 DEFAULT NULL,
  p_description IN VARCHAR2 DEFAULT NULL
) RETURN NUMBER AS
  l_updated_count NUMBER;
BEGIN
  UPDATE Blog
  SET name = NVL(p_name, name),
      discription = NVL(p_description, discription)
  WHERE blog_id = p_blog_id;
  
  l_updated_count := SQL%ROWCOUNT;
    DBMS_OUTPUT.PUT_LINE('Блог обновлен');
  RETURN l_updated_count;
  EXCEPTION
    WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Ошибка: ' || SQLERRM);
    RETURN -1;
END;

CREATE OR REPLACE FUNCTION DelBlog (
  p_blog_id IN NUMBER
) RETURN NUMBER AS
  l_deleted_count NUMBER;
BEGIN
  DELETE FROM Blog
  WHERE blog_id = p_blog_id;
  
  l_deleted_count := SQL%ROWCOUNT;
      DBMS_OUTPUT.PUT_LINE('Блог удален');
  RETURN l_deleted_count;
  EXCEPTION
    WHEN NO_DATA_FOUND THEN
    DBMS_OUTPUT.PUT_LINE('Блог не найден');
    RETURN -1;
    WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Ошибка: ' || SQLERRM);
    RETURN -1;
END;

CREATE OR REPLACE FUNCTION GetBlogById (
  p_blog_id IN NUMBER
) RETURN Blog%ROWTYPE AS
  l_blog Blog%ROWTYPE;
BEGIN
  SELECT * INTO l_blog
  FROM Blog
  WHERE blog_id = p_blog_id;
  
  RETURN l_blog;
  
  EXCEPTION
  WHEN NO_DATA_FOUND THEN
  DBMS_OUTPUT.PUT_LINE('Блог не найден');
  RETURN NULL;
  WHEN OTHERS THEN
  DBMS_OUTPUT.PUT_LINE('Ошибка: ' || SQLERRM);
  RETURN NULL;
END;

CREATE OR REPLACE FUNCTION GetAllBlogs RETURN SYS_REFCURSOR AS
  l_cursor SYS_REFCURSOR;
BEGIN
  OPEN l_cursor FOR
    SELECT * 
    FROM Blog;
  
  RETURN l_cursor;
END;

CREATE OR REPLACE FUNCTION GETBLOGFORUSER(
  p_user_id IN NUMBER,
  p_start_row IN NUMBER,
  p_count_row IN NUMBER) RETURN SYS_REFCURSOR AS
 l_cursor SYS_REFCURSOR;
 BEGIN
  OPEN l_cursor FOR
    SELECT * FROM (
      SELECT b.*, ROWNUM as rn FROM Blog b
      WHERE USER_ID = p_user_id ORDER BY b."TIMESTAMP" ASC
    ) 
    WHERE rn BETWEEN p_start_row AND (p_start_row + p_count_row);
  return l_cursor;
end;

