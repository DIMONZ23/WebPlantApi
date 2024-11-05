using System;
using System.Collections.Generic;

namespace WebPlantApi.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;
}
