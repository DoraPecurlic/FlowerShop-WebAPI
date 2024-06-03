-- Insert skripta za tablicu "User"
INSERT INTO "User" ("FirstName", "LastName", "Role")
VALUES ('Dora', 'Pec', 'ADMIN'),
       ('Sara', 'Beg', 'BUYER'),
		('Dino', 'Roz', 'BUYER');

-- Insert skripta za tablicu "OrderType"
INSERT INTO "OrderType" ("OrderType", "IsActive", "CreatedByUserId", "UpdatedByUserId")
VALUES ('Bouquet', TRUE, 1, 1),
       ('Flower Basket', TRUE, 1, 1),
       ('Flower Box', TRUE, 1, 1);

-- Insert skripta za tablicu "Order"
INSERT INTO "Order" ("FlowerType", "Quantity", "OrderTypeId", "IsActive", "CreatedByUserId", "UpdatedByUserId")
VALUES ('Rose', 5, 1, TRUE, 2, 2),
       ('Tulips', 7, 2, TRUE, 3, 3);
       
