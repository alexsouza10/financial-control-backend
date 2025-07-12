// FinancialControl.Infrastructure/Data/AppDbContext.cs

using FinancialControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql; // Necessário para AppContext.SetSwitch
using System; // Necessário para AppContext
using System.Linq; // Necessário para o Linq no SaveChanges

namespace FinancialControl.Infrastructure.Data;

public class AppDbContext : DbContext
{
    // Define os DbSets para suas entidades que correspondem a tabelas no banco de dados.
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Salary> Salaries { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // IMPORTANTE:
        // Esta linha configura o Npgsql globalmente para desativar o comportamento legado
        // de timestamp. Isso garante que DateTime com Kind=Unspecified não seja enviado
        // para colunas 'timestamp with time zone', resolvendo o erro '-infinity' que você teve antes.
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);

        // Se você estivesse usando o NodaTime para lidar com datas e horas,
        // você precisaria da linha abaixo, mas para DateTime padrão, a linha acima é a correta.
        // NpgsqlConnection.GlobalTypeMapper.UseNodaTime();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapeamento explícito para nomes de tabela, se eles forem diferentes dos nomes dos DbSets.
        // Se as tabelas já têm esses nomes, essas linhas são opcionais mas não prejudicam.
        modelBuilder.Entity<Expense>().ToTable("Expenses");
        modelBuilder.Entity<Salary>().ToTable("Salaries");

        // Configurações específicas para a entidade Expense
        modelBuilder.Entity<Expense>(entity =>
        {
            // Define o tipo de coluna para valores decimais no banco de dados.
            entity.Property(e => e.Value).HasColumnType("numeric(18,2)");
            // Define que a categoria é obrigatória e tem um tamanho máximo.
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            // Define que o método de pagamento é obrigatório e tem um tamanho máximo.
            entity.Property(e => e.PaymentMethod).IsRequired().HasMaxLength(50);
            // Define o tamanho máximo para o campo de cartão (não é obrigatório).
            entity.Property(e => e.Card).HasMaxLength(50);
            // As propriedades Id, CreatedAt e UpdatedAt são herdadas de BaseEntity e serão
            // automaticamente mapeadas para a tabela "Expenses" porque BaseEntity é [NotMapped].
            // Não precisamos de entity.Property(e => e.Date) aqui se ela for um DateTime padrão
            // e a coluna no DB for 'timestamp with time zone', e o AppContext.SetSwitch estiver ativo.
        });

        // Configurações específicas para a entidade Salary
        modelBuilder.Entity<Salary>(entity =>
        {
            entity.Property(s => s.Value).HasColumnType("numeric(18,2)");
            // As propriedades Id, CreatedAt e UpdatedAt são herdadas de BaseEntity e serão
            // automaticamente mapeadas para a tabela "Salaries".
        });

        // IMPORTANTE:
        // NÃO inclua modelBuilder.Entity<BaseEntity>(...) aqui.
        // A classe BaseEntity deve ser [NotMapped] e o EF Core não deve tentar criar uma tabela para ela.
        // A iteração no SaveChanges é feita em objetos C# e não causa o mapeamento de tabela.
    }

    // Sobrescreve SaveChanges para automatizar CreatedAt e UpdatedAt.
    public override int SaveChanges()
    {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    // Sobrescreve SaveChangesAsync para automatizar CreatedAt e UpdatedAt em operações assíncronas.
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }

    // Método auxiliar para definir CreatedAt e UpdatedAt antes de salvar as alterações no DB.
    private void OnBeforeSaving()
    {
        // Obtém todas as entidades que estão sendo adicionadas ou modificadas e que herdam de BaseEntity.
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added ||
                e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var baseEntity = (BaseEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                baseEntity.SetUpdatedAt(DateTime.UtcNow);
            }

            if (entry.State == EntityState.Modified)
            {
                baseEntity.SetUpdatedAt(DateTime.UtcNow);
            }
        }
    }
}