using Dapper;
using Microsoft.Data.SqlClient;
using System.Dynamic;
using DapperWebApiExample.Services;

namespace DapperWebApiExample.Services
{
    public class InventoryService
    {
        private readonly string _connectionString;

        public InventoryService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<dynamic>> GetAllAvailableEquipment()
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                string sql = @"exec thapp_dcommon.dbo.eqp_selectallavailableequipment";
                await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync(Helpers.DefaultParamsGet(sql));
            }
        }

        public async Task<IEnumerable<dynamic>> GetItems()
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    if object_id('tempdb..#tempdata') is not null drop table #tempdata
                    create table #tempdata (
                      Id int
                      , TypeName varchar(50)
                      , ModelName varchar(200)
                      , CreateBy varchar(200)
                      , CreatedAt datetime
                      , UpdateBy varchar(50)
                      , UpdateAt datetime
                    )
                    insert into #tempdata
                    exec thapp_dcommon.dbo.eqp_gettype

                    select *, itemtype = typename, itemmodel = modelname from #tempdata
                ";
                await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync(Helpers.DefaultParamsGet(sql));
            }
        }

        public async Task<IEnumerable<dynamic>> GetUser()
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    if object_id('tempdb..#tempdata') is not null drop table #tempdata
                    create table #tempdata (
                        Id int
                        , Department varchar(100)
                        , DisplayName varchar(100)
                        , EmployeeID varchar(100)
                        , EmailAddress varchar(100)
                        , CreatedBy varchar(100)
                        , CreatedAt datetime
                    )
                    insert into #tempdata
                    exec thapp_dcommon.dbo.eqp_select_aduser

                    select *, [value] = displayname, [label] = displayname from #tempdata
                ";
                await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync(Helpers.DefaultParamsGet(sql));
            }
        }

        public async Task<IEnumerable<dynamic>> GetSelectPO()
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                string sql = @"exec thapp_dcommon.dbo.eqp_selectpo";
                await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync(Helpers.DefaultParamsGet(sql));
            }
        }

        public async Task<IEnumerable<dynamic>> GetSelectAllAddLog()
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    if object_id('tempdb..#tempdata') is not null drop table #tempdata
                    create table #tempdata (
                      Id int
                      , PONumber varchar(100)
                      , SupplierInvoiceNo varchar(100)
                      , ResponsibleIT varchar(100)
                      , Name varchar(100)
                      , Model varchar(100)
                      , Quantity int
                      , Location  varchar(100)
                      , Remark varchar(100)
                      , Create_Date datetime
                      , Create_By  varchar(100)
                    )
                    insert into #tempdata
                    exec thapp_dcommon.dbo.eqp_selectalladdlog

                    select *, createdate = convert(varchar(20), Create_Date, 120) from #tempdata
                ";
                await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync(Helpers.DefaultParamsGet(sql));
            }
        }

        public async Task<IEnumerable<dynamic>> GetSelectAllRemoveLog()
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    if object_id('tempdb..#tempdata') is not null drop table #tempdata
                    create table #tempdata (
                      Id int
                      , ItemId int
                      , ITStaff varchar(100)
                      , UserName varchar(100)
                      , Remark varchar(100)
                      , RequestTicket varchar(100)
                      , Name varchar(100)
                      , Model varchar(100)
                      , Quantity int
                      , Location varchar(100)
                      , Create_Date datetime
                      , Create_By varchar(100)
                    )
                    insert into #tempdata
                    exec thapp_dcommon.dbo.eqp_selectallremovelog

                    select *, createdate = convert(varchar(20), Create_Date, 120) from #tempdata
                ";
                await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync(Helpers.DefaultParamsGet(sql));
            }
        }

        public async Task<IEnumerable<dynamic>> GetAddLogByID(string id)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    declare @id int set @id = [@id]
                    exec thapp_dcommon.dbo.eqp_findaddlogitemid @id
                ";
                await dbConnection.OpenAsync();
                string newsql = Helpers.DefaultParamsGet(sql, "id", id);
                return await dbConnection.QueryAsync(newsql);
            }
        }

        public async Task<IEnumerable<dynamic>> GetRemoveLogByID(string id)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    declare @id int set @id = [@id]
                    exec thapp_dcommon.dbo.eqp_findremovelogitemid @id
                ";
                await dbConnection.OpenAsync();
                string newsql = Helpers.DefaultParamsGet(sql, "id", id);
                return await dbConnection.QueryAsync(newsql);
            }
        }

        public async Task<IEnumerable<dynamic>> SetAddItemType(dynamic payload)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    declare @type varchar(50) set @type = [@type]
                    declare @model varchar(50) set @model = [@model]
                    exec thapp_dcommon.dbo.eqp_addtype @type, @model
                ";
                await dbConnection.OpenAsync();
                string newsql = Helpers.DefaultParamsPost(sql, payload);
                return await dbConnection.QueryAsync(newsql);
            }
        }

        public async Task<IEnumerable<dynamic>> SetEditItemType(dynamic payload)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    declare @id int set @id = [@id]
                    declare @type varchar(50) set @type = [@type]
                    declare @model varchar(50) set @model = [@model]
                    declare @create_date datetimeoffset(7) set @create_date = getdate()
                    declare @create_by varchar(50) set @create_by = [@createby]
                    exec thapp_dcommon.dbo.eqp_edittype @id, @type, @model, @create_date, @create_by
                ";
                await dbConnection.OpenAsync();
                string newsql = Helpers.DefaultParamsPost(sql, payload);
                return await dbConnection.QueryAsync(newsql);
            }
        }

        public async Task<IEnumerable<dynamic>> SetDeleteItemType(dynamic payload)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    declare @id int set @id = [@id]
                    exec thapp_dcommon.dbo.eqp_removetype @id
                ";
                await dbConnection.OpenAsync();
                string newsql = Helpers.DefaultParamsPost(sql, payload);
                return await dbConnection.QueryAsync(newsql);
            }
        }

        public async Task<IEnumerable<dynamic>> SetAddItem(dynamic payload)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    declare @name varchar(50) set @name = [@name]
                    declare @model varchar(50) set @model = [@model]
                    declare @ponumber varchar(10) set @ponumber = [@ponumber]
                    declare @supplierinvoiceno varchar(20) set @supplierinvoiceno = [@supplierinvoiceno]
                    declare @itstaff varchar(50) set @itstaff = [@itstaff]
                    declare @quantity int set @quantity = [@quantity]
                    declare @location varchar(5) set @location = [@location]
                    declare @remark varchar(max) set @remark = [@remark]
                    declare @create_date datetimeoffset(7) set @create_date = getdate()
                    declare @create_by varchar(50) set @create_by = [@createby]
                    exec thapp_dcommon.dbo.eqp_additem @name, @model, @ponumber, @supplierinvoiceno, @itstaff, @quantity, @location, @remark, @create_date, @create_by
                ";
                await dbConnection.OpenAsync();
                string newsql = Helpers.DefaultParamsPost(sql, payload);
                return await dbConnection.QueryAsync(newsql);
            }
        }

        public async Task<IEnumerable<dynamic>> SetRemoveItem(dynamic payload)
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                string sql = @"
                    declare @id int set @id = [@id]
                    declare @itstaff varchar(50) set @itstaff = [@itstaff]
                    declare @username varchar(50) set @username = [@username]
                    declare @requestticket varchar(15) set @requestticket = [@requestticket]
                    declare @remark varchar(max) set @remark = [@remark]
                    declare @quantity int set @quantity = [@quantity]
                    declare @location varchar(5) set @location = [@location]
                    declare @create_by varchar(50) set @create_by = [@createby]
                    declare @create_date datetimeoffset(7) set @create_date = getdate()
                    declare @update_by varchar(50) set @update_by = [@updateby]
                    declare @update_date datetimeoffset(7) set @update_date = getdate()

                    exec thapp_dcommon.dbo.eqp_removebyid @id, @quantity
                    exec thapp_dcommon.dbo.eqp_addremovelog @id, @itstaff, @username, @requestticket, @remark, @quantity, @location, @create_by, @create_date, @update_by, @update_date
                ";
                await dbConnection.OpenAsync();
                string newsql = Helpers.DefaultParamsPost(sql, payload);
                return await dbConnection.QueryAsync(newsql);
            }
        }

    }
}
