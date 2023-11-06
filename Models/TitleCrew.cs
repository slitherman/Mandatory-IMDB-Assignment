using System;
using System.Collections.Generic;

namespace Mandatory_IMDB_Assignment.Models;

public partial class TitleCrew
{
    public string Tconst { get; set; } = null!;

    public virtual ICollection<TitleDirector> TitleDirectors { get; set; } = new List<TitleDirector>();

    public virtual ICollection<TitleWriter> TitleWriters { get; set; } = new List<TitleWriter>();

    public static Func<string[], TitleCrew> TitleMapper = values => new TitleCrew
    {
        Tconst = values[0],

    };
}
