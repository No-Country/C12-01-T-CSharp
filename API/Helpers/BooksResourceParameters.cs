namespace API.Helpers
{
    public class BooksResourceParameters
    {
        
        /// <summary>
        /// Filter by Category
        /// </summary>
        public string? Category { get; set; }


        /// <summary>
        /// Search by Title and Author
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        /// OrderBy Price, type ASC or DES, ASC by default
        /// </summary>
        public string Price { get; set; } = "ASC";
    }
}
