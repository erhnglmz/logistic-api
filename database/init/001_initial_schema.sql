-- Logistic Automation - Initial schema bootstrap
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

-- Schema version tracking
CREATE TABLE IF NOT EXISTS "SchemaVersion" (
    "Version"     VARCHAR(50) PRIMARY KEY,
    "AppliedAt"   TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "Description" TEXT
);

INSERT INTO "SchemaVersion" ("Version", "Description")
VALUES ('001', 'Initial schema bootstrap')
ON CONFLICT ("Version") DO NOTHING;
