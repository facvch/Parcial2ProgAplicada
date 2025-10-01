Es una modifiacion del proyecto de marianoEsquibel18 con soporte para sqlite.

Otras cosas que añade/modifica son: 
  - Una carpeta Migrations/DeprecatedMigrations, ignorada por Infrastructure.csproj para mover migraciones "deprecadas" y así no eliminar cosas por error.
  - Enum DatabaseType para sqlite. 
  - La opcion de CreateDataBase sqlite.
  - Un IServiceCollection AddSqliteRepositories.
  - Y un repositorio especial para sqlite, con su StoreDbContext y StoreDbContextFactory correspondientes.

![alt text]([https://billiken.lat/wp-content/uploads/2025/04/dt-23.jpg])
