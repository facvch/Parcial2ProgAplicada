Es una modifiacion del proyecto de marianoEsquibel18 con soporte para sqlite.

Otras cosas que añade/modifica son: 
  - Una carpeta Migrations/DeprecatedMigrations, ignorada por Infrastructure.csproj para mover migraciones "deprecadas" y así no eliminar DataBases por error.
  - Enum DatabaseType para sqlite. 
  - La opcion de CreateDataBase sqlite.
  - Un IServiceCollection AddSqliteRepositories.
  - Y un repositorio especial para sqlite, con su StoreDbContext y StoreDbContextFactory correspondientes.

![alt text](https://billiken.lat/wp-content/uploads/2025/04/dt-23.jpg)

Guia de uso!
  - Cuando se abre el proyecto en VisualStudio, abrir la carpeta "Backend" así te deja elegir Template-API como proyecto principal.
  - Click secundario en la carpeta Template-API y "Abrir en terminal"
  - Ejecutar los siguientes comandos:
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    dotnet add package Microsoft.EntityFrameworkCore.Tools
  -  Lo anterior te instala las dependencias de Sqlite, ahora ejecutas esto:
    dotnet build
    (opcional, si te da error move los archivos sueltos de "Migrations" a "DeprecatedMigrations") dotnet ef migrations remove --project ../Infrastructure --startup-project ../Template-API --context Infrastructure.Repositories.Sqlite.StoreDbContext
    dotnet ef migrations add InitialCreate --project ../Infrastructure --startup-project ../Template-API --context Infrastructure.Repositories.Sqlite.StoreDbContext --output-dir Migrations
    dotnet ef database update --project ../Infrastructure --startup-project ../Template-API --context Infrastructure.Repositories.Sqlite.StoreDbContext
  - Y ahí tendria que estar funcionando el programa. 
