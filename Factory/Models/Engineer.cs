using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Engineer
    {
      public int EngineerId { get; set; }
      [Required (ErrorMessage = "Please enter a name.")]
      public string Name { get; set; }
      public DateTime HireDate { get; set; }
      public List<EngineerMachine> JoinEntities { get; set; }
    }
}