-- Enable the extension once per database.
CREATE EXTENSION IF NOT EXISTS vector;

-- Store a document with its embedding (1536 dims for ada-002).
CREATE TABLE documents (
    id      SERIAL PRIMARY KEY,
    body    TEXT NOT NULL,
    embedding VECTOR(1536)
);

-- Build an approximate index. lists = sqrt(row count) is a rough starting point.
CREATE INDEX ON documents USING ivfflat (embedding vector_cosine_ops)
    WITH (lists = 64);

-- Find the 5 most semantically similar documents to a query vector.
-- :query_vec is a 1536-float array bound at the application layer.
SELECT id, body,
       1 - (embedding <=> :query_vec) AS similarity
FROM   documents
ORDER  BY embedding <=> :query_vec
LIMIT  5;
