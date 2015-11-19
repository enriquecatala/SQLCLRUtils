using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void Bucle_NonYield ()
    {
		// Saturando CPU, cada 10s se hará un "yield forzado" por sql (en lugar de cada 4ms cooperativamente)
		//
        for (long i = 0; i <= 8620000000; i++)
        { 
        }
    }

    /// <summary>
    /// Este va bien para la demo de CPU ussage
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [Microsoft.SqlServer.Server.SqlFunction(SystemDataAccess = SystemDataAccessKind.None, IsDeterministic = true, IsPrecise = true)]
    public static SqlInt64 Bucle_NonYield_function(SqlInt64 value)
    {
        // Saturando CPU, cada 10s se hará un "yield forzado" por sql (en lugar de cada 4ms cooperativamente)
        //
        for (long i = 0; i <= 100 * value; i++)
        {
        }
        return (value);
    }
}
