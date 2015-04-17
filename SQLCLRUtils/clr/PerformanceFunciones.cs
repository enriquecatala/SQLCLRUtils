using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{

    /// <summary>
    /// Por defecto DataAccess = NO
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    [Microsoft.SqlServer.Server.SqlFunction(IsPrecise=true,IsDeterministic=true,DataAccess=DataAccessKind.None)]
    public static SqlBoolean EsNumeroParClr(SqlInt32 num)
    {
        return new SqlBoolean(num % 2==0 ? true : false);
    }

    [Microsoft.SqlServer.Server.SqlFunction(IsPrecise = true, IsDeterministic = true, DataAccess = DataAccessKind.Read)]
    public static SqlBoolean EsNumeroParClrDataAccess(SqlInt32 num)
    {
        return new SqlBoolean(num % 2 == 0 ? true : false);
    }


}
