namespace SPZCapstoneVar2.Models
{
    public class Connection
    {
        public int FromId { get; set; }

        public int ToId { get; set; }

        public int FromPinIndex { get; set; }

        public int ToPinIndex { get; set; }

        public bool IsConnectedTo(int targetId) => FromId == targetId || ToId == targetId;
    }
}
