using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;
using System.Reflection;

namespace Infrastructure.Context
{
    public class QRCodeDbContextFactory : IDesignTimeDbContextFactory<QRCodeDbContext>
    {
        public QRCodeDbContext CreateDbContext(string[] args)
        {
            String databasePath =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), 
                    "qrcodemaui.db"
                );
            var optionsBuilder = new DbContextOptionsBuilder<QRCodeDbContext>();
            optionsBuilder.UseSqlite(databasePath,
                            sqliteOptionsAction: sqlOptions =>
                            {
                                sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(2).TotalSeconds);
                                sqlOptions.MigrationsAssembly(typeof(QRCodeDbContext).GetTypeInfo().Assembly.GetName().Name);
                            })
                        .EnableSensitiveDataLogging();
            return new QRCodeDbContext(optionsBuilder.Options);
        }
    }
}