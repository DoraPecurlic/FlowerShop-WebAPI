CREATE TABLE "User"(
    "Id" SERIAL PRIMARY KEY,
    "FirstName" VARCHAR(25),
    "LastName" VARCHAR(30),
	"Role" VARCHAR(20) NOT NULL CHECK ("Role" IN ('ADMIN', 'BUYER'))
);

CREATE TABLE "OrderType"(
    "Id" SERIAL PRIMARY KEY,
    "OrderType" VARCHAR(100) NOT NULL,
	
    "IsActive" BOOL NOT NULL DEFAULT TRUE, 
    "DateCreated" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,          
    "DateUpdated" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

	"CreatedByUserId" INT NOT NULL,
    "UpdatedByUserId" INT NOT NULL,

	FOREIGN KEY ("CreatedByUserId") REFERENCES "User"("Id"),
    FOREIGN KEY ("UpdatedByUserId") REFERENCES "User"("Id")
);

CREATE TABLE "Order"(
    "Id" SERIAL PRIMARY KEY,
    "FlowerType" VARCHAR(25) NOT NULL,
    "Quantity" INT NOT NULL,
    "OrderTypeId" INT NOT NULL,
	
    "IsActive" BOOL NOT NULL DEFAULT TRUE,
    "DateCreated" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "DateUpdated" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	
    "CreatedByUserId" INT NOT NULL,
    "UpdatedByUserId" INT NOT NULL,
	
    FOREIGN KEY ("OrderTypeId") REFERENCES "OrderType"("Id"),
    FOREIGN KEY ("CreatedByUserId") REFERENCES "User"("Id"),
    FOREIGN KEY ("UpdatedByUserId") REFERENCES "User"("Id")
);
