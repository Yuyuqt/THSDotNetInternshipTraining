using System;
using System.Collections.Generic;

namespace THSDotNetTraining.EFCoreSample.Database.AppDbContextModels;

public partial class Blog
{
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public string BlogAuthor { get; set; } = null!;

    public string BlogContent { get; set; } = null!;
}
