osql -S WIN-OP9N72R91I2\SQLEXPRESS -d MedFacSystem -E -Q "BACKUP DATABASE MedFacSystem TO DISK = '%1' WITH INIT"
pause