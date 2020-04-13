DO $$
BEGIN
   IF EXISTS (SELECT FROM pg_database WHERE datname = 'fit') THEN
      RAISE NOTICE 'fit database already exists';
   ELSE
      PERFORM dblink_exec('dbname=' || current_database(), 'CREATE DATABASE fit');
   END IF;
END
$$;