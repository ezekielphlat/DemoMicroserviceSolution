using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryWebApi.Models
{
    [Table("inventory", Schema = "public")]
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("inventory_id")]
        public int InventoryId { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("product_name")]
        public string ProductName { get; set; }
        [Column("total_stock")]
        public int TotalStock { get; set; }
        [Column("remaining_stock")]
        public int RemainingStock { get; set; }
        [Column("last_modified")]
        public DateTime LastModified { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
    }
}
