namespace DataAccess.EF.Seeder;

public interface IDbSeeder
{
    Task SeedDataAsync();
}