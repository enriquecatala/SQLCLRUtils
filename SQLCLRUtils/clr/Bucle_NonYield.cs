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
}
