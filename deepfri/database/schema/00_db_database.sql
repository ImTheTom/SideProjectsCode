DO $$
BEGIN
   IF EXISTS (SELECT FROM pg_database WHERE datname = 'deepfri') THEN
      RAISE NOTICE 'deepfri database already exists';
   ELSE
      PERFORM dblink_exec('dbname=' || current_database(), 'CREATE DATABASE deepfri');
   END IF;
END
$$;