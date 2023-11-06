using System;
using System.Collections.Generic;

namespace Mandatory_IMDB_Assignment.Models;

public partial class Genre
{
    public string Tconst { get; set; } = null!;

    public string GenreName { get; set; } = null!;

    public int GenreId { get; set; }

    public virtual Title TconstNavigation { get; set; } = null!;


    public static Func<string[], Genre> gennreMapper = values => new Genre
    {
        Tconst = values[0],
        GenreName = values[1]

    };
}
