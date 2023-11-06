using System;
using System.Collections.Generic;

namespace Mandatory_IMDB_Assignment.Models;

public partial class PrimaryProfession
{
    public string Nconst { get; set; } = null!;

    public string? Profession { get; set; }

    public int ProfId { get; set; }

    public virtual NameBasic NconstNavigation { get; set; } = null!;

    public static Func<string[], PrimaryProfession> profMapper = values => new PrimaryProfession
    {
        Nconst = values[0],
        Profession = values[1],

    };
}
