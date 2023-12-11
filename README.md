# OpenBaseNET para Oracle
![file-sNxWguRlgRD15B33cvlk9sHq](https://github.com/britors/OpenBaseNETOracle/assets/183213/449f9dbe-8a57-4be0-a010-85bc26ef36a8)

> OpenBaseNET para Oracle é um template para projetos .net 8 usando base de dados Oracle
O template foi construído devido a necessidade de criar projetos  forma rápida e prática.

<p>Para criar um projeto, basta seguir os passos abaixo: </p>

### Crie seu projeto usando o template OpenBaseNETOracle
![image](https://github.com/britors/OpenBaseNETOracle/assets/183213/1503d4b0-d7d9-4e25-a3ae-ec93c74a421e)


### Baixe seu projeto para sua máquina <br/>
#### Exemplo
```bash
git clone <projeto>
```
### Agora, dentro da Pasta _01-Presentation_, acesse o arquivo _appsettings.json_ e altere a string de conexão para a sua base de dados e para o banco mongodb para logs
#### Exemplo de appsettings
```json
{
  "ConnectionStrings": {
    "OpenBaseOracle": "Data Source=localhost:1521/FREE; User Id=OPENBASENET;Password=OPENBASENET;",
    "OpenBaseMongoDb": "mongodb://localhost:27017/logs-oracle"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
### No projeto OpenBaseNET.Application acesse a pasta Entities e crie suas classes para representar as suas entidades (existe um modelo chamado Customer, use como exemplo)
   É extemamente importante que a classe implemente a interface _IEntityOrQueryResult_ </p>
#### Exemplo de classe para representar uma entidade
```csharp
namespace OpenBaseNET.Domain.Entities;

public sealed class Customer : IEntityOrQueryResult
{
    public int Id { get; set; }
    public Name Name { set; get; } = null!;
 
}
```
### No Projeto OpenBaseNET.Infra.Data.Context acesse a pasta Configurations e crie a classe de mapeamento da sua entidade (existe um modelo chamado CustomerMapping, use como exemplo)
#### Exemplo de classe para mapear uma entidade
```csharp
namespace OpenBaseNET.Infra.Data.Context.Configurations;

internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("CLITAB", "OPENBASENET");

        builder.HasKey(c => c.Id)
            .HasName("PK_CLITAB");

        builder
            .Property(c => c.Id)
            .HasColumnName("CLIID");
        
        builder
            .OwnsOne(
                c => c.Name, 
                name =>
            {
                    name.Property(n => n.Value)
                    .HasColumnName("CLINM")
                    .HasMaxLength(255)
                    .IsRequired();
            });
    }
}
```
## Pronto!
> A partir de agora você pode criar suas classes de serviço, repositório e controlador para sua entidade <br/>
Caso você siga o padrão de nomenclatura do template não precisará fazer nenhuma configuração adicional <br/>
