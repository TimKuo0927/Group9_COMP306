using System;
using System.Collections.Generic;

namespace group9_GroupProject.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Isbn { get; set; } = null!;

    public string BookTitle { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int CategoryId { get; set; }

    public int PublisherId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    public virtual BookCategory Category { get; set; } = null!;

    public virtual Publisher Publisher { get; set; } = null!;
}
