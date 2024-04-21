namespace AirportManagementSystem.Models
{
    public class Gates
    {
        public Flight? Flight { get; set; } // ? means that the value can be null
        public int GateNumber { get; set; }
        public Boolean InUse { get; set; } = false;

        public Gates(int GateNumber)
        {
            this.GateNumber = GateNumber;
        }
    }  
}
