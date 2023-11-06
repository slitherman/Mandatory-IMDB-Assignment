using System;
using System.Collections.Generic;

namespace Mandatory_IMDB_Assignment.Models;

public partial class TitleDirector
{
    public string Tconst { get; set; } = null!;

    public string? DirectorNconst { get; set; }

    public int DirectorId { get; set; }

    public virtual TitleCrew TconstNavigation { get; set; } = null!;

    public static Func<string[], TitleDirector> directorMapper = values => new TitleDirector
    {
        Tconst = values[0],
        DirectorNconst = TruncateIfTooLong(values[1], 1000),

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
