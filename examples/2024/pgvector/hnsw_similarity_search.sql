-- Enable the extension and create an HNSW index on a documents table.
-- Assumes a 1536-dim embedding (text-embedding-3-small output).
-- Run in your Postgres 15+ instance after: CREATE EXTENSION IF NOT EXISTS vector;

ALTER TABLE documents ADD COLUMN IF NOT EXISTS embedding vector(1536);

CREATE INDEX IF NOT EXISTS documents_embedding_hnsw
    ON documents
    USING hnsw (embedding vector_cosine_ops)
    WITH (m = 16, ef_construction = 64);

-- Nearest-neighbor search: returns the 10 most similar chunks to a query embedding.
-- Bind $1 to the query vector (cast from your application's float array).
SELECT
    id,
    content,
    metadata,
    1 - (embedding <=> $1::vector) AS similarity
FROM documents
ORDER BY embedding <=> $1::vector
LIMIT 10;
