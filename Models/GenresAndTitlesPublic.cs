using System;
using System.Collections.Generic;

namespace Mandatory_IMDB_Assignment.Models;

public partial class GenresAndTitlesPublic
{
    public string PrimaryTitle { get; set; } = null!;

    public string TitleType { get; set; } = null!;

    public int? RuntimeMinutes { get; set; }

    public bool? IsAdult { get; set; }

    public string GenreName { get; set; } = null!;

    public string OriginalTitle { get; set; } = null!;
}
