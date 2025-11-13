using System;
using System.Collections.Generic;

namespace group9_GroupProject.Models;

public partial class Publisher
{
    public int PublisherId { get; set; }

    public string PublisherName { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
