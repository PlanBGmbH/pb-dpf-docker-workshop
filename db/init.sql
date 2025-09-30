CREATE TABLE "Messages" (
    "Id" SERIAL PRIMARY KEY,
    "Text" VARCHAR(255) NOT NULL
);

INSERT INTO "Messages" ("Text") VALUES ('Hello from Postgres init.sql!');
