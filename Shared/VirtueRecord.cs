using CsvHelper.Configuration.Attributes;

namespace BlazorTest.Shared;

public class VirtueRecord
{
    [Index(0)]
    public string? Url { get; set; }
    [Index(1)]
    public string? Grade { get; set; }
    [Index(2)]
    public string? Topic  { get; set; }
    [Index(3)]
    public string? Lesson  { get; set; }
    [Index(4)]
    public string? Activity  { get; set; }
    [Index(5)]
    public string? Name  { get; set; }
    [Index(6)]
    public string? Description  { get; set; }
    [Index(7)]
    public string? ResourceType  { get; set; }
    [Index(8)]
    public string? VideoMinutes { get; set; }
    [Index(9)]
    public string? Source { get; set; }
        
    [Index(10)]
    [TypeConverter(typeof(MultivalueConverter))]
    public string[]? School2030Values { get; set; }
    [Index(11)]
    [TypeConverter(typeof(MultivalueConverter))]
    public string[]? School2030Virtues { get; set; }
    [Index(12)]
    [TypeConverter(typeof(MultivalueConverter))]
    public string[]? EtapValues { get; set; }
    [Index(13)]
    [TypeConverter(typeof(MultivalueConverter))]
    public string[]? EtapVirtues { get; set; }

    [Index(14)]
    public string? Comments { get; set; }
}