using System;
using System.Collections.Generic;

namespace group9_GroupProject.Models;

public partial class BookCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
