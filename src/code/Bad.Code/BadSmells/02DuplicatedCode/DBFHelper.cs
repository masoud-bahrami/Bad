using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace Bad.Code.BadSmells._02DuplicatedCode;

public class DBFHelper
{
    DBFWriter Writer;
    Stream fileStream;

    public void Write(DBFField[] fields, Object[] values)
    {
        DBFWriter r = new DBFWriter();

        Writer.Fields = fields;

        Writer.AddRecord(values);
        Writer.Write(fileStream);
    }

    public void Write(DBFField[] fields, List<Object[]> valuesList)
    {
        DBFWriter r = new DBFWriter();

        var writer = new DBFWriter();
        Writer.Fields = fields;
        foreach (var values in valuesList)
        {
            Writer.AddRecord(values);
        }
        Writer.Write(fileStream);
    }

}

public class DBFWriter
{
    public DBFField[] Fields { get; set; }
    public void AddRecord(params Object[] values)
    {
    }
    public void Write(Stream tOut)
    {
    }
}


public class DBFField
{
}