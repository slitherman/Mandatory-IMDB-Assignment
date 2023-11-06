using System;
using System.Collections.Generic;

namespace Mandatory_IMDB_Assignment.Models;

public partial class NameBasic
{
    public string Nconst { get; set; } = null!;

    public string? PrimaryName { get; set; }

    public int? BirthYear { get; set; }

    public int? DeathYear { get; set; }

    public virtual ICollection<KnownForTitle> KnownForTitles { get; set; } = new List<KnownForTitle>();

    public virtual ICollection<PrimaryProfession> PrimaryProfessions { get; set; } = new List<PrimaryProfession>();

    public static Func<string[], NameBasic> nameMapper = values => new NameBasic
    {
        Nconst = values[0],
        PrimaryName = values[1],
        BirthYear = int.TryParse(values[2], out int birthYear) ? birthYear : (int?)null,
        DeathYear = values[3] == "\\N" ? (int?)null : int.TryParse(values[3], out int deathYear) ? deathYear : (int?)null


    };
}
