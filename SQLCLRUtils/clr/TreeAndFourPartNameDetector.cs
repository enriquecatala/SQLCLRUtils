using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Enrique Catala Bañuls
/// Esta función CLR TVF detecta y devuelve aquellas referencias a nombres de tres y cuatro partes de un tsql
///  - select * from server.bbdd.esquema.objeto
///  - select * from bbdd.esquema.objeto
///  
/// </summary>
internal class GroupNode
{
    private int _index;
    public int Index { get { return _index; } }

    private string _name;
    public string Name { get { return _name; } }

    private string _value;
    public string Value { get { return _value; } }

    public GroupNode(int index, string group, string value)
    {
        _index = index;
        _name = group;
        _value = value;
    }
};

internal class GroupIterator : IEnumerable
{

    private string _input;

    //private static readonly Regex _regex =
    //    new Regex(@"(?<fourPart>[\[|\']?\w*[\]|\']?\.[\[|\']?\w*[\]|\']?\.[\[|\']?\w*[\]|\']?\.[\[|\']?\w*[\]|\']?)|(?<treePart>[\[|\']?\w*[\]|\']?\.[\[|\']?\w*[\]|\']?\.[\[|\']?\w*[\]|\']?)"
    //    , RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex _regex =
       new Regex(@"(?<=[from|join|exec|into]\s+)(?<fourPart>[\[|\']?\w*[\]|\']?\.[\[|\']?\w*[\]|\']?\.[\[|\']?\w*[\]|\']?\.[\[|\']?\w*[\]|\']?)|(?<=[from|join|exec|into]\s+)(?<treePart>[\[|\']?\w*[\]|\']?\.[\[|\']?\w*[\]|\']?\.[\[|\']?\w*[\]|\']?)"
       , RegexOptions.IgnoreCase | RegexOptions.Compiled);



    public GroupIterator(string input)
    {
        _input = input;
    }

    public IEnumerator GetEnumerator()
    {
        int index = 0;
        Match current = null;
        string[] names = _regex.GetGroupNames();
        do
        {
            index++;
            current = (current == null) ?
                _regex.Match(_input) : current.NextMatch();
            if (current.Success)
            {
                foreach (string name in names)
                {
                    Group group = current.Groups[name];
                    if (group.Success)
                    {
                        yield return new GroupNode(
                            index, name, group.Value);
                    }
                }
            }
        }
        while (current.Success);
    }
};


public partial class UserDefinedFunctions
{


    [SqlFunction(FillRowMethodName = "FillGroupRow", TableDefinition =
    "[Index] int,[Group] nvarchar(max),[Text] nvarchar(max)")]
    public static IEnumerable TreeAndFourPartNameDetector(SqlChars input)
    {
        return new GroupIterator(new string(input.Value));
    }

    [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters")]
    public static void FillGroupRow(object data,
        out SqlInt32 index, out SqlChars group, out SqlChars text)
    {
        GroupNode node = (GroupNode)data;
        index = new SqlInt32(node.Index);
        group = new SqlChars(node.Name.ToCharArray());
        text = new SqlChars(node.Value.ToCharArray());
    }

};


