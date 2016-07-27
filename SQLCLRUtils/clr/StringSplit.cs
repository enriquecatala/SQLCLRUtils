using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections;

/// <summary>
/// Based on http://sqlblog.com/blogs/adam_machanic/archive/2009/04/28/sqlclr-string-splitting-part-2-even-faster-even-more-scalable.aspx 
/// </summary>
public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction(
        FillRowMethodName = "FillRowNumber",
        TableDefinition = "item bigint",
        DataAccess = DataAccessKind.None
        )
     ]
    public static IEnumerator StringSplit_Number([SqlFacet(MaxSize = -1)]
      SqlChars Input,
      [SqlFacet(MaxSize = 255)]
      SqlChars Delimiter)
    {
        return (
            (Input.IsNull || Delimiter.IsNull) ?
            new StringSplit_Class(new char[0], new char[0]) :
            new StringSplit_Class(Input.Value, Delimiter.Value));
    }

    [Microsoft.SqlServer.Server.SqlFunction(
    FillRowMethodName = "FillRow",
    TableDefinition = "item nvarchar(4000)",
    DataAccess = DataAccessKind.None
    )
 ]
    public static IEnumerator StringSplit_Text(
        [SqlFacet(MaxSize = -1)]
        SqlChars Input,
        [SqlFacet(MaxSize = 255)]
        SqlChars Delimiter)
    {
        return (
            (Input.IsNull || Delimiter.IsNull) ?
            new StringSplit_Class(new char[0], new char[0]) :
            new StringSplit_Class(Input.Value, Delimiter.Value));
    }

    public static void FillRow(object obj, out SqlString item)
    {
        item = new SqlString((string)obj);
    }

    public static void FillRowNumber(object obj, out SqlInt64 item)
    {
        item = new SqlInt64(long.Parse((string)obj));
    }

    public class StringSplit_Class : IEnumerator
    {
        public StringSplit_Class(char[] TheString, char[] Delimiter)
        {
            theString = TheString;
            stringLen = TheString.Length;
            delimiter = Delimiter;
            delimiterLen = (byte)(Delimiter.Length);
            isSingleCharDelim = (delimiterLen == 1);

            lastPos = 0;
            nextPos = delimiterLen * -1;
        }

        #region IEnumerator Members

        public object Current
        {
            get
            {
                return new string(theString, lastPos, nextPos - lastPos);
            }
        }

        public bool MoveNext()
        {
            if (nextPos >= stringLen)
                return false;
            else
            {
                lastPos = nextPos + delimiterLen;

                for (int i = lastPos; i < stringLen; i++)
                {
                    bool matches = true;

                    //Optimize for single-character delimiters
                    if (isSingleCharDelim)
                    {
                        if (theString[i] != delimiter[0])
                            matches = false;
                    }
                    else
                    {
                        for (byte j = 0; j < delimiterLen; j++)
                        {
                            if (((i + j) >= stringLen) || (theString[i + j] != delimiter[j]))
                            {
                                matches = false;
                                break;
                            }
                        }
                    }

                    if (matches)
                    {
                        nextPos = i;

                        //Deal with consecutive delimiters
                        if ((nextPos - lastPos) > 0)
                            return true;
                        else
                        {
                            i += (delimiterLen - 1);
                            lastPos += delimiterLen;
                        }
                    }
                }

                lastPos = nextPos + delimiterLen;
                nextPos = stringLen;

                if ((nextPos - lastPos) > 0)
                    return true;
                else
                    return false;
            }
        }

        public void Reset()
        {
            lastPos = 0;
            nextPos = delimiterLen * -1;
        }

        #endregion

        private int lastPos;
        private int nextPos;

        private readonly char[] theString;
        private readonly char[] delimiter;
        private readonly int stringLen;
        private readonly byte delimiterLen;
        private readonly bool isSingleCharDelim;
    }
}