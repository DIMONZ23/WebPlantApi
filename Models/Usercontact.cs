using System;
using System.Collections.Generic;

namespace WebPlantApi.Models;

public partial class Usercontact
{
    public int Id { get; set; }

    public string Contactvalue { get; set; } = null!;

    public bool? Isprimary { get; set; }

    public DateTime? Createdat { get; set; }

    public string? Username { get; set; }

    public string? Usercomment { get; set; }
}
