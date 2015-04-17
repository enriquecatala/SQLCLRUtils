using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;

public partial class UserDefinedFunctions
{
    /// <summary>
    /// Returns true if the regex pattern is match
    /// </summary>
    /// <param name="input"></param>
    /// <param name="pattern"></param>
    /// <param name="options">
    ///     Values for OPTIONS
    ///     =================
    ///     scREIgnoreCase	        1	  Case insensitive match
    ///     scREReplaceNoCopy	    128	  Do not copy to the output portions of the input string that did not participate in the match
    ///     scREDotNotMatchNull	    16	  Dot should not match null character '
    ///     scRECollate	            2	  Locale sensitive
    ///     scRESyntaxExtensions	256	  Enable syntax extensions
    ///     scREMatchSingleLine	    32	  Prevent ^ and $ from matching next to a newline
    ///     scREDefaultOptions	    382	  Default options
    ///     scREDontMatchEmpty	    4	  Expression should not match an empty string
    ///     scREReplaceFirstOnly	64	  Replace only the first occurrence of match
    ///     scREDotNotMatchNewline	8	  Dot should not match newline
    /// </param>
    /// <returns></returns>
    [Microsoft.SqlServer.Server.SqlFunction(IsDeterministic=true,IsPrecise=true,DataAccess=DataAccessKind.None)]
    public static  SqlBoolean RegExIsMatch(SqlString input,SqlString pattern)
    {
        // Put your code here
        if (input.IsNull || pattern.IsNull || String.IsNullOrWhiteSpace(pattern.Value))
            return SqlBoolean.False;

        
        Regex regex= new Regex(pattern.Value);

        return(regex.IsMatch(input.Value));
    }

    /// <summary>
    /// Replaces all occurrences of the specified pattern in the expression with the "replace" value 
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="pattern"></param>
    /// <param name="replace"></param>
    /// <returns></returns>
    [Microsoft.SqlServer.Server.SqlFunction(IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None)]
    public static SqlString RegExReplace(SqlString expression, SqlString pattern, SqlString replace)
    {
        if (expression.IsNull || pattern.IsNull || replace.IsNull)
            return SqlString.Null;

        Regex r = new Regex(pattern.Value);

        return new SqlString(r.Replace(expression.Value, replace.Value));
    }
}
