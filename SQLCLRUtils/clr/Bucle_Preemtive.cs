using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Threading;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void Bucle_Preemtive ()
    {
        // Marcamos este proceso para que se pase el hilo al OS y sea él quien maneje el quantum
        //
        Thread.BeginThreadAffinity();        

        for (long i = 0; i <= 8620000000; i++)
        {
        }

        Thread.EndThreadAffinity();
    }
}
