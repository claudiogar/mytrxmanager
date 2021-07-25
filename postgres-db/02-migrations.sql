CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Transactions" (
    "Id" SERIAL PRIMARY KEY,
    "Date" timestamp without time zone NOT NULL,
    "Amount" money NOT NULL,
    "Recipient" text COLLATE pg_catalog."default",
    "Currency" text COLLATE pg_catalog."default",
    "ProductType" integer NOT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210724173046_Initial', '5.0.8');

COMMIT;

