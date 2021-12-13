using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace BlazorTest.Shared;

public class MultivalueConverter : TypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (text == "") return Array.Empty<string>();
        string[] allElements = text.Split(';');
        return allElements;
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        return string.Join(";", (string[]) value);
    }
}