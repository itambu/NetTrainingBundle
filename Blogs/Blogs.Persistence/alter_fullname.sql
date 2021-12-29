Alter Table Users Drop Column FullName  
ALTER TABLE Users ADD FullName AS (FirstName+' '+LastName)