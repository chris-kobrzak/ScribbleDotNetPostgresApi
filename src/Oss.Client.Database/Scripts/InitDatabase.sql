CREATE USER scribble WITH
  LOGIN
  PASSWORD 'foo';

CREATE DATABASE oss WITH
  OWNER = scribble
  ENCODING = 'UTF8'
  CONNECTION LIMIT = -1;

\c oss

CREATE EXTENSION pgcrypto;

CREATE TABLE users (
  id UUID NOT NULL DEFAULT gen_random_uuid(),
  active BOOLEAN NOT NULL DEFAULT TRUE,
  role_id INT,
  email VARCHAR(100),
  password CHAR(60),
  first_name VARCHAR(100),
  last_name VARCHAR(100)
);

ALTER TABLE users OWNER TO scribble;

INSERT INTO users (active, role_id, email, password, first_name)
  VALUES (TRUE, 2, 'chris@gmail.com', 'foo', 'Chris');
INSERT INTO users (active, role_id, email, password, first_name)
  VALUES (FALSE, 2, 'donna@gmail.com', 'foo', 'Donna');
INSERT INTO users (active, role_id, email, password, first_name)
  VALUES (TRUE, 2, 'carmen@gmail.com', 'foo', 'Carmen');


CREATE FUNCTION public.get_users(_active BOOLEAN)
RETURNS TABLE(
  id UUID,
  active BOOLEAN,
  role_id INT,
  email VARCHAR,
  first_name VARCHAR,
  last_name VARCHAR
)
LANGUAGE SQL
SECURITY DEFINER
AS $BODY$
  SELECT id, active, role_id, email, first_name, last_name
  FROM public.users
  WHERE active = _active
$BODY$;

ALTER FUNCTION public.get_users(BOOLEAN) OWNER TO scribble;


CREATE FUNCTION public.get_user_by_credentials(_email VARCHAR, _password VARCHAR)
RETURNS TABLE(
  id UUID,
  role_id INT,
  email VARCHAR,
  first_name VARCHAR,
  last_name VARCHAR
)
LANGUAGE SQL
SECURITY DEFINER
AS $BODY$
  SELECT id, role_id, email, first_name, last_name
  FROM public.users
  WHERE email = _email AND password = _password
  LIMIT 1
$BODY$;

ALTER FUNCTION public.get_user_by_credentials(VARCHAR, VARCHAR) OWNER TO scribble;