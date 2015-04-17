using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlBoolean SqlContains(SqlString querystring, SqlString texto_a_buscar)
    {
        if (querystring.IsNull)
            return SqlBoolean.Null;
        return (querystring.Value.Contains(texto_a_buscar.Value));

    }

    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlBoolean SqlContainsCI(SqlString querystring, SqlString texto_a_buscar)
    {
        if (querystring.IsNull)
            return SqlBoolean.Null;
        else
            return (querystring.Value.IndexOf(texto_a_buscar.Value, 0, StringComparison.CurrentCultureIgnoreCase) >= 0);

    }


    /// <summary>
    /// Esto es porque para evitar multiples llamadas, si sabemos que vamos a pedir comparar con muchas cosas, me interesa crear una llamada que
    /// encapsule varias expresiones a comparar.
    ///     la idea es que en texto a buscar se pase texto | texto2 | texto3,...  y se indique como separador = '|' de forma que se esté indicando indirectamente
    ///     como un conjunto de cadenas 
    /// A dia de hoy, en SQL Server 2014 no se permite TVP como parámetros de entrada
    /// </summary>
    /// <param name="querystring"></param>
    /// <param name="texto_a_buscar"></param>
    /// <param name="separador"></param>
    /// <returns></returns>
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlBoolean SqlContainsMultiple(SqlString querystring,
                                                 SqlString texto_a_buscar,
                                                 [SqlFacet(IsFixedLength = true, IsNullable = false, MaxSize = 1)]SqlChars separador)
    {
        if (querystring.IsNull)
            return SqlBoolean.Null;

        /// Si no encuentro el texto, sigo buscando...
        foreach (string tmp_texto_a_buscar in texto_a_buscar.Value.Split(separador[0]))
        {
            if (querystring.Value.Contains(tmp_texto_a_buscar))
                return (SqlBoolean.True);
        }

        /// Si despues de estar buscando por todo lo que le he pasado no encuentro nada...no hay nada
        return (SqlBoolean.False);
    }
}
