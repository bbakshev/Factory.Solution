using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Machine
    {
      public int MachineId { get; set; }
      [Required (ErrorMessage = "Please enter name of the machine.")]
      public string Name { get; set; }
      public DateTime InspectionDate { get; set; }
      public List<EngineerMachine> JoinEntities { get; set; }
    }
}