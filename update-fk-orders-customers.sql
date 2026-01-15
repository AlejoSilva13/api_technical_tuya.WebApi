
USE CustomerOrdersDb;
GO

-- Elimina la FK actual
ALTER TABLE Orders DROP CONSTRAINT FK_Orders_Customers;

-- Crea una nueva FK
ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId)
REFERENCES Customers(Id)
ON DELETE CASCADE;

