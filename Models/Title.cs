using System;
using System.Collections.Generic;

namespace Mandatory_IMDB_Assignment.Models;

public partial class Title
{
    public string Tconst { get; set; } = null!;

    public string TitleType { get; set; } = null!;

    public string PrimaryTitle { get; set; } = null!;

    public string OriginalTitle { get; set; } = null!;

    public bool? IsAdult { get; set; }

    public int? StartYear { get; set; }

    public int? EndYear { get; set; }

    public int? RuntimeMinutes { get; set; }

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    public static Func<string[], Title> titleMapper = values => new Title
    {
        Tconst = values[0],
        TitleType = values[1],
        PrimaryTitle = values[2],
        OriginalTitle = values[3],
        IsAdult = values[4] == "1" | values[4] == "0",
        StartYear = int.TryParse(values[5], out var startYear) ? startYear : 0,
        EndYear = int.TryParse(values[6], out var endYear) ? endYear : 0,
        RuntimeMinutes = int.TryParse(values[7], out var runtimeMinutes) ? runtimeMinutes : 0
    };
}

