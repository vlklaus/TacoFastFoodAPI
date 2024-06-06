using System;
using System.Collections.Generic;

namespace _060524_Taco_Fast_Food_API.Models;

public partial class Taco
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public float? Cost { get; set; }

    public bool? SoftShell { get; set; }

    public bool? Chips { get; set; }
}
