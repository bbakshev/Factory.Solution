namespace Factory.Models
{
    public class EngineerMachine
    {
        public int EngineerMachineId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Please select an engineer.")]
        public int EngineerId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a machine.")]
        public int MachineId { get; set; }
        public Engineer Engineer { get; set; }
        public Machine Machine { get; set; }
    }
}