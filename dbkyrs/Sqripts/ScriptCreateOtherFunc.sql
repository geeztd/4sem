CREATE OR REPLACE FUNCTION UpdSubscription (
  SubscriberId IN NUMBER,
  BlogId IN NUMBER
) return boolean
AS
BEGIN
  INSERT INTO SUBSCRIPTION(subscriber_id, blog_id) VALUES(SubscriberId, BlogId);
  DBMS_OUTPUT.PUT_LINE('Пользователь подписался.');
  COMMIT;
  return true;
EXCEPTION
  WHEN DUP_VAL_ON_INDEX THEN BEGIN
    DELETE FROM SUBSCRIPTION
    WHERE subscriber_id = SubscriberId
      AND blog_id = BlogId;
    COMMIT;
  DBMS_OUTPUT.PUT_LINE('Пользователь отписался.');
  return true;
  END;
  When OTHERS Then BEGIN
  DBMS_OUTPUT.PUT_LINE('Произошла ошибка: ' || SQLERRM);
  return false;
  END;
END UpdSubscription;

CREATE OR REPLACE FUNCTION UpdFavorite (
  UserId IN NUMBER,
  PostId IN NUMBER
) return boolean
IS
BEGIN
  INSERT INTO Favorite(user_id, post_id) VALUES(UserId, PostId);
  DBMS_OUTPUT.PUT_LINE('Пост добавлен в избранное.');
  COMMIT;
  return true;
EXCEPTION
  WHEN DUP_VAL_ON_INDEX THEN
    DELETE FROM Favorite
    WHERE user_id = UserId
      AND post_id = PostId;
    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Пост удален из избранного.');
    return true;
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Произошла ошибка' || SQLERRM);
    RETURN FALSE;
END UpdFavorite;

CREATE OR REPLACE FUNCTION UpdLike (
  p_user_id IN NUMBER,
  p_comment_id IN NUMBER
) return boolean
IS
BEGIN
  INSERT INTO Like_Table(user_id, comment_id) VALUES(p_user_id, p_comment_id);
  DBMS_OUTPUT.PUT_LINE('Лайк добавлен');
  COMMIT;
  return true;
EXCEPTION
  WHEN DUP_VAL_ON_INDEX THEN
    DELETE FROM Like_Table
    WHERE user_id = p_user_id
      AND comment_id = p_comment_id;
    COMMIT;
    DBMS_OUTPUT.PUT_LINE('Лайк удален');
    return true;
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Произошла ошибка' || SQLERRM);
    RETURN FALSE;
END UpdLike;

CREATE OR REPLACE FUNCTION GETCOUNTSUBFORBLOG(
  p_blog_id IN NUMBER
) RETURN INTEGER AS l_count_sub INTEGER;
BEGIN
  SELECT COUNT(*) INTO l_count_sub FROM
  SUBSCRIPTION s WHERE s.BLOG_ID = p_blog_id;
  return l_count_sub;
END;

CREATE OR REPLACE FUNCTION GETCOUNTSUBFORUSER(
  p_user_id IN NUMBER
) RETURN INTEGER AS l_count_sub INTEGER;
BEGIN
  SELECT COUNT(*) INTO l_count_sub FROM
  SUBSCRIPTION s WHERE s.SUBSCRIBER_ID = p_user_id;
  return l_count_sub;
END;

CREATE OR REPLACE FUNCTION GETCOUNTLIKE(
  p_com_id IN NUMBER
) RETURN INTEGER AS l_count_like INTEGER;
BEGIN
  SELECT COUNT(*) INTO l_count_like FROM
  LIKE_TABLE l WHERE l.COMMENT_ID = p_com_id;
  return l_count_like;
END;

CREATE OR REPLACE FUNCTION GETCOUNTFAVFORUSER(
  p_user_id IN NUMBER
) RETURN INTEGER AS l_count_fav INTEGER;
BEGIN
  SELECT COUNT(*) INTO l_count_fav FROM
  FAVORITE f WHERE f.USER_ID = p_user_id;
  return l_count_fav;
END;

CREATE OR REPLACE FUNCTION GETCOUNTFAVFORPOST(
  p_post_id IN NUMBER
) RETURN INTEGER AS l_count_fav INTEGER;
BEGIN
  SELECT COUNT(*) INTO l_count_fav FROM
  FAVORITE f WHERE f.POST_ID = p_post_id;
  return l_count_fav;
END;

CREATE OR REPLACE FUNCTION SEARCH(p_text IN NVARCHAR2) RETURN SYS_REFCURSOR AS
  l_cursor SYS_REFCURSOR;
BEGIN
  OPEN l_cursor FOR
    SELECT p.*,
           SCORE(1) AS score_containing,
           SCORE(2) AS score_name
    FROM Post p
    WHERE CONTAINS(p.CONTAINING, p_text, 1) > 0
       OR CONTAINS(p."NAME", p_text, 2) > 0
    ORDER BY score_containing DESC, score_name DESC;  

  RETURN l_cursor;
EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.PUT_LINE('Ошибка: ' || SQLERRM);
    RETURN NULL;
END;