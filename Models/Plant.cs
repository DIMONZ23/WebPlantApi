using System;
using System.Collections.Generic;

namespace WebPlantApi.Models;

public partial class Plant
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Imageurl { get; set; }

    public string? Shortdescription { get; set; }

    public string? Detaileddescription { get; set; }

    public decimal Price { get; set; }

    public DateTime? Createdat { get; set; }
}
