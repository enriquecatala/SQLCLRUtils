using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;

/// <summary>
/// Enrique Catala Bañuls: UDA optimizado para concatenar clases en SM
///     Pide como parámetro saber qué clase queremos se pinte primero
///     la clase es un varchar(10) según el esquema de BBDD
/// </summary>
[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.UserDefined, IsInvariantToNulls = false,
    IsInvariantToOrder = false,
    IsInvariantToDuplicates = false,
    IsNullIfEmpty = true,
    MaxByteSize = -1,
    Name = "CONCAT_AGG_Optimized")]
public struct CONCAT_AGG_Optimized : IBinarySerialize
{
    private StringBuilder resultado;
    public void Init()
    {
        // Put your code here
        resultado = new StringBuilder();
    }
    /// <summary>
    /// Agregamos valores separados por "/" de forma que si primerValor == true, el valor que entra se debe imprimir el primero en el resultado final
    /// </summary>
    /// <param name="Value"></param>
    /// <param name="primerValor"></param>
    public void Accumulate([SqlFacet(MaxSize = 10)]SqlChars Value, SqlBoolean primerValor)
    {

        if (Value.IsNull)
            return;

        if (primerValor)
        {
            resultado.Insert(0, '/');
            resultado.Insert(0, Value.Value);
        }
        else
            resultado.Append(Value.Value).Append('/');

    }

    public void Merge(CONCAT_AGG_Optimized Group)
    {
        resultado.Append(Group.resultado);
    }

    public SqlString Terminate()
    {
        String salida = String.Empty;

        if (resultado != null && resultado.Length > 0)
            salida = resultado.ToString(0, resultado.Length - 1);
        return new SqlString(salida);
    }


    public void Read(System.IO.BinaryReader r)
    {
        resultado = new StringBuilder(r.ReadString());
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(resultado.ToString());
    }
}
