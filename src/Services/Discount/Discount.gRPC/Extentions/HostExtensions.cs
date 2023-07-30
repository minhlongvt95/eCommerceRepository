using Npgsql;

namespace Discount.gRPC.Extentions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0) where TContext : class
        {
            var retryForAvailability = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var configuration = services.GetRequiredService<IConfiguration>();

                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

                    using var command = new NpgsqlCommand() { Connection = connection };

                    logger.LogInformation("Migrating postresql database.");
                    connection.Open();

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon (   Id SERIAL PRIMARY KEY NOT NULL,
                                                                    ProductName VARCHAR(100) NOT NULL,
                                                                    Description TEXT,
                                                                    Amount INT )";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon ( ProductName, Description, Amount ) VALUES('IPhone X', 'IPhone X Discount', 155)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO Coupon ( ProductName, Description, Amount ) VALUES('Samsung', 'Samsung Discount', 195)";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrate postgresql process");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "an error occurred while migarate postgresql database");


                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                }

            }

            return host;
        }

    }
}
