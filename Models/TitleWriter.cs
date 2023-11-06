using System;
using System.Collections.Generic;

namespace Mandatory_IMDB_Assignment.Models;

public partial class TitleWriter
{
    public string Tconst { get; set; } = null!;

    public string? WriterNconst { get; set; }

    public int WriterId { get; set; }

    public virtual TitleCrew TconstNavigation { get; set; } = null!;


    public static Func<string[], TitleWriter> writerMapper = values => new TitleWriter
    {
        Tconst = values[0],
        WriterNconst = TruncateIfTooLong(values[1], 1000),

    };


    private static string TruncateIfTooLong(string input, int maxLength)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }
        if (input.Length > maxLength)
        {
            return input.Substring(0, maxLength);
        }

        return input;
    }
}
