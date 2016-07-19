using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.UserDefined, 
    IsInvariantToNulls = true,
    IsInvariantToOrder = false,
    IsInvariantToDuplicates = false,
    IsNullIfEmpty = true,
    MaxByteSize = -1,
    Name = "CONCAT_AGG")]
public struct CONCAT_AGG: IBinarySerialize
{
    private StringBuilder resultado;
    public void Init()
    {
        // Put your code here
        resultado = new StringBuilder();
    }

    public void Accumulate(SqlString Value)
    {
        if (Value.IsNull)
            return;
              
        resultado.Append(Value.Value).Append(';');
    }

    public void Merge (CONCAT_AGG Group)
    {
        resultado.Append(Group.resultado);

    }

    public SqlString Terminate ()
    {
        String salida = null;

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
