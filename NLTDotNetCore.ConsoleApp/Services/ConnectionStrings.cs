using System.Data.SqlClient;

namespace NLTDotNetCore.ConsoleApp.Services;

internal static class ConnectionStrings
{
    public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".", // server name
        InitialCatalog = "NLTDotNetCore", // database name
        UserID = "sa", // username
        Password = "sasa@123", // password
        TrustServerCertificate = true,
    };
}