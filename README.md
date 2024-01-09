# OpenBaseNET para Oracle

![GitHub repo size](https://img.shields.io/github/repo-size/britors/OpenBaseNETOracle)
![GitHub top language](https://img.shields.io/github/languages/top/britors/OpenBaseNETOracle)
![GitHub language count](https://img.shields.io/github/languages/count/britors/OpenBaseNETOracle)
![GitHub last commit](https://img.shields.io/github/last-commit/britors/OpenBaseNETOracle)
![GitHub issues](https://img.shields.io/github/issues/britors/OpenBaseNETOracle)
![GitHub](https://img.shields.io/github/license/britors/OpenBaseNETOracle)
![GitHub forks](https://img.shields.io/github/forks/britors/OpenBaseNETOracle?style=social)
![GitHub Repo stars](https://img.shields.io/github/stars/britors/OpenBaseNETOracle?style=social)
![GitHub watchers](https://img.shields.io/github/watchers/britors/OpenBaseNETOracle?style=social)
![GitHub followers](https://img.shields.io/github/followers/britors?style=social)


![file-sNxWguRlgRD15B33cvlk9sHq](https://github.com/britors/OpenBaseNETOracle/assets/183213/449f9dbe-8a57-4be0-a010-85bc26ef36a8)


![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Oracle](https://img.shields.io/badge/Oracle-F80000?style=for-the-badge&logo=oracle&logoColor=white)



> OpenBaseNET para Oracle é um template para projetos .net 8 usando base de dados Oracle.
O template foi construído devido a necessidade de criar projetos  forma rápida e prática.

## Para criar um projeto, basta seguir os passos abaixo:

#### Crie seu projeto usando o template OpenBaseNETOracle
![image](https://github.com/britors/OpenBaseNETOracle/assets/183213/1503d4b0-d7d9-4e25-a3ae-ec93c74a421e)


#### Baixe seu projeto para sua máquina
```bash
git clone <projeto>
```
#### Agora, dentro da Pasta _01-Presentation_, acesse o arquivo _appsettings.json_ e altere a string de conexão para a sua base de dados

```json
{
  "ConnectionStrings": {
    "OpenBaseOracle": "Data Source=localhost:1521/FREE; User Id=OPENBASENET;Password=OPENBASENET;"
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
#### No projeto OpenBaseNET.Application acesse a pasta Entities e crie suas classes para representar as suas entidades (existe um modelo chamado Customer, use como exemplo)
   É extemamente importante que a classe implemente a interface _IEntityOrQueryResult_ </p>
```csharp
namespace OpenBaseNET.Domain.Entities;

public sealed class Customer : IEntityOrQueryResult
{
    public int Id { get; set; }
    public Name Name { set; get; } = null!;
 
}
```
#### No Projeto OpenBaseNET.Infra.Data.Context acesse a pasta Configurations e crie a classe de mapeamento da sua entidade (existe um modelo chamado CustomerMapping, use como exemplo)
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
