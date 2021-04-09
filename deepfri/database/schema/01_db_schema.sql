CREATE TABLE IF NOT EXISTS images
(
	images_id SERIAL PRIMARY KEY,
	images_original_name TEXT NOT NULL,
	images_file_name TEXT,
	images_filter TEXT NOT NULL,
	images_caption TEXT NOT NULL,
	images_url TEXT,
	images_width INTEGER DEFAULT 0,
	images_height INTEGER DEFAULT 0,
	images_timestamp TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW()
);