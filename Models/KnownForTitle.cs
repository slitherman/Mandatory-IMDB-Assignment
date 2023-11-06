using System;
using System.Collections.Generic;

namespace Mandatory_IMDB_Assignment.Models;

public partial class KnownForTitle
{
    public string Nconst { get; set; } = null!;

    public string? TitleId { get; set; }

    public int KnownId { get; set; }

    public virtual NameBasic NconstNavigation { get; set; } = null!;

    public static Func<string[], KnownForTitle> knownForMapper = values => new KnownForTitle
    {
        Nconst = values[0],
        TitleId = values[1],

    };

}
