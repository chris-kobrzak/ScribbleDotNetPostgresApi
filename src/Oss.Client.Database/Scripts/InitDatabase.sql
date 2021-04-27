CREATE USER scribble WITH
  LOGIN
  PASSWORD 'foo';

CREATE DATABASE oss WITH
  OWNER = scribble
  ENCODING = 'UTF8'
  CONNECTION LIMIT = -1;

\c oss

CREATE TABLE users (id SERIAL, name VARCHAR, active BOOLEAN);

INSERT INTO users VALUES (1, 'Chris', TRUE);
INSERT INTO users VALUES (2, 'Donna', FALSE);
INSERT INTO users VALUES (3, 'Carmen', TRUE);


CREATE FUNCTION public.get_users()
RETURNS TABLE(
  id INTEGER,
  name VARCHAR,
  active BOOLEAN
)
LANGUAGE SQL
SECURITY DEFINER
AS $BODY$
  SELECT * FROM public.users
$BODY$;

ALTER FUNCTION public.get_users() OWNER TO scribble;


CREATE FUNCTION public.get_users(_active BOOLEAN)
RETURNS TABLE(
  id INTEGER,
  name VARCHAR
)
LANGUAGE SQL
SECURITY DEFINER
AS $BODY$
  SELECT id, name FROM public.users WHERE active = _active
$BODY$;

ALTER FUNCTION public.get_users() OWNER TO scribble;