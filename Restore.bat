osql -S WIN-OP9N72R91I2\SQLEXPRESS -E -q "RESTORE DATABASE MedFacSystem 
FROM DISK = '%1' WITH MOVE 'MedFacSystem' 
TO 'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\MedFacSystem.mdf', 
MOVE 'MedicalFacility_Log' 
TO 'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\MedFacSystem_Log.ldf'"