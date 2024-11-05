using System;
using System.Collections.Generic;

namespace WebPlantApi.Models;

public partial class Usercontact
{
    public int Id { get; set; }

    public string Contacttype { get; set; } = null!;

    public string Contactvalue { get; set; } = null!;

    public bool? Isprimary { get; set; }

    public DateTime? Createdat { get; set; }
}
