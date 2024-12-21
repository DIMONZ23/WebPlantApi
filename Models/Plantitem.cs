using System;
using System.Collections.Generic;

namespace WebPlantApi.Models;

public partial class Plantitem
{
    public int Id { get; set; }

    public string Nameplant { get; set; } = null!;

    public string Descriptionplant { get; set; } = null!;

    public string Imageurl { get; set; } = null!;

    public int Priceplant { get; set; }

    public bool Istrendy { get; set; }
}
