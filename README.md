# MercadoLibro
MercadoLibro es un e-commerce especializado en la venta de libros

## API
https://mercadolibro-api.app.csharpjourney.com
## Client
https://mercado-libro-alpha.vercel.app

## Stack 
### ASP.NET Core
- Web API
- Identity
- JWT
- Entity Framework Core

### Angular
- 
## Integrantes
**Backend** [Jhonatan](https://github.com/Jhonatan1599)

**Frontend** [Oleydi](https://github.com/oleydi1234)

## Mapeo de tablas de Postgres a DbContext(Ef Core)
Crear 
- `dotnet ef dbcontext scaffold "Server=localhost;Port=5432;Database=BookCart;User Id=postgres;Password=Pa55w0rd!" Npgsql.EntityFrameworkCore.PostgreSQL -o Models -p .\API\ -s .\MercadoLibro.sln`
  >Nota: Ejecutar el commando en el root del proyecto junto al .sln
  
Actualizar
- `dotnet ef dbcontext scaffold "Server=localhost;Port=5432;Database=BookCart;User Id=postgres;Password=Pa55w0rd!" Npgsql.EntityFrameworkCore.PostgreSQL -o Models --context BookCartContext --context-dir models -f -p .\API\ -s .\MercadoLibro.sln`
