using Finflow.Hlopov.Core.Entities;
using Finflow.Hlopov.Core.Repositories;
using Finflow.Hlopov.Core.Specifications;
using Finflow.Hlopov.Infrastructure.Data;
using Finflow.Hlopov.Infrastructure.Repository.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Finflow.Hlopov.Infrastructure.Repository
{
    public class RemittanceRepository: Repository<Remittance>, IRemmittanceRepository
    {
        public RemittanceRepository(FinflowContext dbContext) : base(dbContext)
        {
        }

        public async Task<Remittance> Create(Remittance remittance)
        {
            Guid newId = Guid.Empty;

            using (var command = ((SqlConnection)_dbContext.Database.GetDbConnection()).CreateCommand())
            {
                command.CommandText = "dbo.sp_CreateRemmittance";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Code", remittance.Code);
                command.Parameters.AddWithValue("@SenderId", remittance.Sender.Id);
                command.Parameters.AddWithValue("@ReceiverId", remittance.Receiver.Id);
                command.Parameters.AddWithValue("@SendAmount", remittance.SendAmount);
                command.Parameters.AddWithValue("@ReceiveAmount", remittance.ReceiveAmount);
                command.Parameters.AddWithValue("@Rate", remittance.Rate);
                command.Parameters.AddWithValue("@SendCurrencyId", remittance.SendCurrencyId);
                command.Parameters.AddWithValue("@ReceiveCurrencyId", remittance.ReceiveCurrencyId);
                
                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                var dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    newId = dataReader.GetGuid(0);
                }

                dataReader.Close();
            }

            return await GeRemittanceByIdAsync(newId);
        }

        public async Task<Remittance> GeRemittanceByIdAsync(Guid remmittanceId)
        {
            var spec = new RemittanceSpecification(remmittanceId);
            var remmittance = (await GetAsync(spec)).FirstOrDefault();
            return remmittance;
        }

        public async Task<IEnumerable<Remittance>> GetRemittanceListAsync()
        {
            var spec = new RemittanceSpecification();
            return await GetAsync(spec);
        }

        public async Task<Remittance> UpdateStatusAsync(Guid remmittanceId, int statusId)
        {
            using (var command = ((SqlConnection)_dbContext.Database.GetDbConnection()).CreateCommand())
            {
                command.CommandText = "dbo.sp_UpdateRemmittanceStatus";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Id", remmittanceId);
                command.Parameters.AddWithValue("@StatusId", statusId);

                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                await command.ExecuteNonQueryAsync();
            }

            return await GeRemittanceByIdAsync(remmittanceId);
        }
    }
}