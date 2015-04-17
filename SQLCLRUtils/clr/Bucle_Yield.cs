using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Threading;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void Bucle_Yield ()
    {
        // Dividimos el bucle en "n" partes y en cada una de ellas forzamos nosotros la liberacion del thread para ser "cooperativos"        
        //
        for (long i = 0; i <= 8620000000; i++)
        {
            if (i % 500 == 0)
                Thread.Sleep(0);
        }
    }
}
