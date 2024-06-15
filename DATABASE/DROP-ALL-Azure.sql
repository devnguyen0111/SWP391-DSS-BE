-- Declare variables to store the SQL statements
DECLARE @fkSql NVARCHAR(MAX) = N'';
DECLARE @tableSql NVARCHAR(MAX) = N'';

-- Generate DROP CONSTRAINT statements for foreign keys
SELECT @fkSql += 'ALTER TABLE ' + QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) 
                 + ' DROP CONSTRAINT ' + QUOTENAME(CONSTRAINT_NAME) + ';'
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE CONSTRAINT_TYPE = 'FOREIGN KEY';

-- Generate DROP TABLE statements
SELECT @tableSql += 'DROP TABLE ' + QUOTENAME(TABLE_SCHEMA) + '.' + QUOTENAME(TABLE_NAME) + ';'
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE';

-- Print the generated SQL for verification
PRINT @fkSql;
PRINT @tableSql;

-- Execute the generated SQL
EXEC sp_executesql @fkSql;
EXEC sp_executesql @tableSql;