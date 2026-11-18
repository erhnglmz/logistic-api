-- Logistic Automation - Company / Customers module

CREATE TABLE "Company" (
    "Id"                      UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "Name"                    VARCHAR(200) NOT NULL,
    "CompanyType"             VARCHAR(50) NOT NULL,
    "TaxOffice"               VARCHAR(100),
    "TaxNumber"               VARCHAR(20),
    "NationalId"              VARCHAR(11),
    "Address"                 TEXT,
    "Phone"                   VARCHAR(20),
    "Email"                   VARCHAR(254),
    "AccountingContactInfo"   TEXT,
    "DefaultPaymentTermDays"  INTEGER,
    "DefaultVatRate"          NUMERIC(5, 2),
    "IsActive"                BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedDate"             TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "ModifiedDate"            TIMESTAMPTZ,
    "CreatedById"             UUID,
    "ModifiedById"            UUID,
    CONSTRAINT "UqCompanyTaxNumber" UNIQUE ("TaxNumber"),
    CONSTRAINT "UqCompanyNationalId" UNIQUE ("NationalId")
);

CREATE INDEX "IxCompanyName" ON "Company" ("Name");
CREATE INDEX "IxCompanyIsActive" ON "Company" ("IsActive");

CREATE TABLE "CompanyRole" (
    "Id"              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "CompanyId"       UUID NOT NULL REFERENCES "Company" ("Id") ON DELETE CASCADE,
    "Role"            VARCHAR(50) NOT NULL,
    "CreatedDate"     TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "ModifiedDate"    TIMESTAMPTZ,
    "CreatedById"     UUID,
    "ModifiedById"    UUID,
    CONSTRAINT "UqCompanyRoleCompanyRole" UNIQUE ("CompanyId", "Role")
);

CREATE INDEX "IxCompanyRoleRole" ON "CompanyRole" ("Role");

CREATE TABLE "CompanyContact" (
    "Id"              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "CompanyId"       UUID NOT NULL REFERENCES "Company" ("Id") ON DELETE CASCADE,
    "FirstName"       VARCHAR(100) NOT NULL,
    "LastName"        VARCHAR(100) NOT NULL,
    "Title"           VARCHAR(100),
    "Phone"           VARCHAR(20),
    "Email"           VARCHAR(254),
    "IsPrimary"       BOOLEAN NOT NULL DEFAULT FALSE,
    "CreatedDate"     TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "ModifiedDate"    TIMESTAMPTZ,
    "CreatedById"     UUID,
    "ModifiedById"    UUID
);

CREATE INDEX "IxCompanyContactCompanyId" ON "CompanyContact" ("CompanyId");

CREATE TABLE "CompanyNote" (
    "Id"              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "CompanyId"       UUID NOT NULL REFERENCES "Company" ("Id") ON DELETE CASCADE,
    "NoteText"        TEXT NOT NULL,
    "ContactDate"     DATE,
    "CreatedDate"     TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "ModifiedDate"    TIMESTAMPTZ,
    "CreatedById"     UUID,
    "ModifiedById"    UUID
);

CREATE INDEX "IxCompanyNoteCompanyId" ON "CompanyNote" ("CompanyId");

CREATE TABLE "CompanyBankAccount" (
    "Id"              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "CompanyId"       UUID NOT NULL REFERENCES "Company" ("Id") ON DELETE CASCADE,
    "Iban"            VARCHAR(34) NOT NULL,
    "BankName"        VARCHAR(100),
    "IsDefault"       BOOLEAN NOT NULL DEFAULT FALSE,
    "CreatedDate"     TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    "ModifiedDate"    TIMESTAMPTZ,
    "CreatedById"     UUID,
    "ModifiedById"    UUID
);

CREATE INDEX "IxCompanyBankAccountCompanyId" ON "CompanyBankAccount" ("CompanyId");

INSERT INTO "SchemaVersion" ("Version", "Description")
VALUES ('002', 'Company / Customers module')
ON CONFLICT ("Version") DO NOTHING;
