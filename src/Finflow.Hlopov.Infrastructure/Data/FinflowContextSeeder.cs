using Finflow.Hlopov.Core.Entities;
using Finflow.Hlopov.Core.Entities.Enums;
using Finflow.Hlopov.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using RemittanceStatuses = Finflow.Hlopov.Core.Entities.Enums.RemittanceStatuses;

namespace Finflow.Hlopov.Infrastructure.Data
{
    public class FinflowContextSeeder
    {
        public static async Task SeedAsync(FinflowContext finflowContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                // TODO: Only run this if using a real database
                await finflowContext.Database.MigrateAsync();
                await finflowContext.Database.EnsureCreatedAsync();

                if (!finflowContext.Clients.Any())
                {
                    finflowContext.Clients.AddRange(GetPreconfiguredClients());
                    await finflowContext.SaveChangesAsync();
                }

                if (!finflowContext.Currencies.Any())
                {
                    finflowContext.Currencies.AddRange(GetPreconfiguredCurrencies());
                    await finflowContext.SaveChangesAsync();
                }

                if (!finflowContext.Statuses.Any())
                {
                    finflowContext.Statuses.AddRange(GetPreconfiguredStatuses());
                    await finflowContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<FinflowContextSeeder>();
                    log.LogError(exception.Message);
                    await SeedAsync(finflowContext, loggerFactory, retryForAvailability);
                }

                throw;
            }
        }

        private static IEnumerable<Client> GetPreconfiguredClients()
        {
            return new List<Client>()
            {
                new Client { Name = "Robert", Surname = "Martin", DateOfBirth = DateTime.ParseExact("12/05/1952", "MM/dd/yyyy", CultureInfo.InvariantCulture) },
                new Client { Name = "Martin", Surname = "Fowler", DateOfBirth = DateTime.ParseExact("01/01/1963", "MM/dd/yyyy", CultureInfo.InvariantCulture) }
            };
        }

        private static IEnumerable<Currency> GetPreconfiguredCurrencies()
        {
            var list = new List<Currency>();
            foreach (CurrenciesISO4217 currency in Enum.GetValues(typeof(CurrenciesISO4217)))
                list.Add(Currency.Create((int)currency, currency.GetDescription()));

            return list;
        }

        private static IEnumerable<Status> GetPreconfiguredStatuses()
        {
            var list = new List<Status>();
            foreach (RemittanceStatuses status in Enum.GetValues(typeof(RemittanceStatuses)))
                list.Add(Status.Create((int)status, status.GetDescription()));

            return list;
        }
    }
}