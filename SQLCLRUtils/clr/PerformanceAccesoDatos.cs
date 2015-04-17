using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void PerformanceAccesoDatosCLR (SqlInt32 idProductMin, SqlInt32 idProductMax)
    {
        using(SqlConnection con = new SqlConnection("context connection=true"))
        {
            con.Open();

            SqlCommand command = new SqlCommand(@"select [Product_Name] from dbo.ProductsBig where ID_Product between @p1 and @p2", con);
            command.Parameters.Add(new SqlParameter("@p1", SqlDbType.Int)).Value = idProductMin;
            command.Parameters.Add(new SqlParameter("@p2", SqlDbType.Int)).Value = idProductMax;

            SqlContext.Pipe.ExecuteAndSend(command);
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void PerformanceAccesoDatosCLR_ManualSend(SqlInt32 idProductMin, SqlInt32 idProductMax)
    {
        using (SqlConnection con = new SqlConnection("context connection=true"))
        {
            con.Open();

            SqlCommand command = new SqlCommand(@"select [Product_Name] from dbo.ProductsBig where ID_Product between @p1 and @p2", con);
            command.Parameters.Add(new SqlParameter("@p1", SqlDbType.Int)).Value = idProductMin;
            command.Parameters.Add(new SqlParameter("@p2", SqlDbType.Int)).Value = idProductMax;
            SqlDataReader dr = command.ExecuteReader();

            SqlContext.Pipe.Send(dr);
        }
    }

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void MyStoredProcedureCLR(SqlString producto)
    {
        using (SqlConnection con = new SqlConnection("context connection=true"))
        {
            con.Open();

            SqlCommand command = new SqlCommand(@"SELECT col1 FROM dbo.ProductsBig WHERE Product_Name = @p", con);
            command.Parameters.Add(new SqlParameter("@p", SqlDbType.VarChar)).Value = producto;

            SqlContext.Pipe.ExecuteAndSend(command);
        }
    }
}
