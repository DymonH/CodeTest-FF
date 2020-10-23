using Microsoft.EntityFrameworkCore.Migrations;

namespace Finflow.Hlopov.Infrastructure.Migrations
{
    public partial class CreateStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE PROCEDURE [dbo].[sp_CreateRemmittance] 
	-- Add the parameters for the stored procedure here
	@Code varchar(max), 
	@SenderId int,
	@ReceiverId int,
	@SendAmount decimal(18, 2),
	@ReceiveAmount decimal(18, 2),
	@Rate decimal(18, 2),
	@SendCurrencyId int,
	@ReceiveCurrencyId int
AS
BEGIN
	declare @RemId uniqueidentifier;
	declare @RemStatusId int;
	set @RemId = NEWID()
	BEGIN TRANSACTION;  
	BEGIN TRY  
		INSERT INTO [dbo].[Remittance]
				   ([Id]
				   ,[Code]
				   ,[SendAmount]
				   ,[ReceiveAmount]
				   ,[Rate]
				   ,[SenderId]
				   ,[ReceiverId]
				   ,[SendCurrencyId]
				   ,[ReceiveCurrencyId])
			 VALUES
				   (@RemId
				   , @Code
				   , @SendAmount
				   , @ReceiveAmount
				   , @Rate
				   , @SenderId
				   , @ReceiverId
				   , @SendCurrencyId
				   , @ReceiveCurrencyId)

		INSERT INTO [dbo].[RemittanceStatus] ([StatusDate], [StatusId]) VALUES(getdate(), 0)
		
		SET @RemStatusId = SCOPE_IDENTITY()

		INSERT INTO [dbo].[RemittanceStatuses] ([RemittanceId], [RemittanceStatusId]) VALUES (@RemId, @RemStatusId)

		SELECT @RemId
	END TRY  
	BEGIN CATCH  
		SELECT   
			ERROR_NUMBER() AS ErrorNumber  
			,ERROR_SEVERITY() AS ErrorSeverity  
			,ERROR_STATE() AS ErrorState  
			,ERROR_PROCEDURE() AS ErrorProcedure  
			,ERROR_LINE() AS ErrorLine  
			,ERROR_MESSAGE() AS ErrorMessage;  
  
		IF @@TRANCOUNT > 0  
			ROLLBACK TRANSACTION;  
	END CATCH;  
  
	IF @@TRANCOUNT > 0  
		COMMIT TRANSACTION;  
END
			");

			migrationBuilder.Sql(@"
CREATE PROCEDURE [dbo].[sp_UpdateRemmittanceStatus] 
	@Id uniqueidentifier, 
	@StatusId int
AS
BEGIN
	declare @currentStatusId int;
	declare @RemStatusId int;
	BEGIN TRANSACTION;  
	BEGIN TRY
		select @currentStatusId=StatusId from RemittanceStatus R where R.Id = (select MAX(RemittanceStatusId) from RemittanceStatuses RS where RS.RemittanceId = @Id)
		IF NOT ((@currentStatusId = 0 AND @StatusId IN (1, 4)) OR (@currentStatusId = 1 AND @StatusId IN (2, 3)))
			THROW 51000, 'Operation not allowed', 1;
			
		INSERT INTO [dbo].[RemittanceStatus] ([StatusDate], [StatusId]) VALUES(getdate(), @StatusId)
		
		SET @RemStatusId = SCOPE_IDENTITY()

		INSERT INTO [dbo].[RemittanceStatuses] ([RemittanceId], [RemittanceStatusId]) VALUES (@Id, @RemStatusId)

	END TRY  
	BEGIN CATCH  
		SELECT   
			ERROR_NUMBER() AS ErrorNumber  
			,ERROR_SEVERITY() AS ErrorSeverity  
			,ERROR_STATE() AS ErrorState  
			,ERROR_PROCEDURE() AS ErrorProcedure  
			,ERROR_LINE() AS ErrorLine  
			,ERROR_MESSAGE() AS ErrorMessage;  
  
		IF @@TRANCOUNT > 0  
			ROLLBACK TRANSACTION;  
	END CATCH;  
  
	IF @@TRANCOUNT > 0  
		COMMIT TRANSACTION;  
END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
