namespace Backend.Model
{
    public class Framework
    {
        public int FrameworkID { get; set; }
        public string FrameworkName { get; set; }

    }

    public class FrameworkDetails
    {
        public int FrameworkID { get; set; }   // This is both the primary key and foreign key
        public string Details { get; set; }    // Large text for details
        public string Link { get; set; }       // URL for link

        // Navigation property to represent the relationship
        public Framework Framework { get; set; }
    }
}
