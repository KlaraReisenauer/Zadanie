 RESTORE DATABASE [Employee_DB] FROM DISK = '/tmp/Employee_DB.bak'
WITH FILE = 1,
MOVE 'Employee_DB' TO '/var/opt/mssql/data/Employee_DB.mdf',
MOVE 'Employee_DB_log' TO '/var/opt/mssql/data/Employee_DB.ldf',
NOUNLOAD, REPLACE, STATS = 5
GO