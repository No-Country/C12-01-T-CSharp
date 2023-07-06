# MercadoLibro
MercadoLibro es un e-commerce especializado en la venta de libros

## Mapeo de tablas de Postgres a DbContext(Ef Core)
Crear 
- `dotnet ef dbcontext scaffold "Server=localhost;Port=5432;Database=BookCart;User Id=postgres;Password=Pa55w0rd!" Npgsql.EntityFrameworkCore.PostgreSQL -o Models -p .\API\ -s .\MercadoLibro.sln`
  >Nota: Ejecutar el commando en el root del proyecto junto al .sln
  
Actualizar
- `dotnet ef dbcontext scaffold "Server=localhost;Port=5432;Database=BookCart;User Id=postgres;Password=Pa55w0rd!" Npgsql.EntityFrameworkCore.PostgreSQL -o Models --force -p .\API\ -s .\MercadoLibro.sln`
