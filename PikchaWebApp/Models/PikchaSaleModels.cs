using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    public class PikchaSaleModels
    {
    }

    [Table("ImageProducts")]
    public class ImageProduct
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        //[Column("ProdCode")]
        //public string ProdCode { get; set; }

        //[Column("ProdNum")]
        //public int ProdNum { get; set; }
        //// https://docs.microsoft.com/en-us/ef/core/modeling/relational/sequences

        [Column("IsSale")]
        public bool IsSale { get; set; } = false;

        [Column("SetPrice", TypeName = "DECIMAL(13,2)")]
        public decimal SetPrice { get; set; } = 0;

        [Column("FinPrice", TypeName = "DECIMAL(13,2)")]
        public decimal FinPrice { get; set; } = 0;

        [Column("Type")]
        public string Type { get; set; } = string.Empty; // primary - artist is selling, secondary - resellers selling

        [Column("Material")]
        public string Material { get; set; } = string.Empty; // primary - artist is selling, secondary - resellers selling

        [Column("Frame")]
        public string Frame { get; set; } = string.Empty; // primary - artist is selling, secondary - resellers selling

        [Column("Border")]
        public string Border { get; set; } = string.Empty; // primary - artist is selling, secondary - resellers selling

        [Column("Finish")]
        public string Finish { get; set; } = string.Empty; // primary - artist is selling, secondary - resellers selling
        

        [Column("Image")]
        public PikchaImage Image { get; set; }

        [Column("Seller")]
        public PikchaUser Seller { get; set; }

        [Column("Printer")]
        public Printer Printer { get; set; }

        public IEnumerable<InvoiceDetail> InvoiceItems { get; set; }
    }


    [Table("Printers")]
    public class Printer
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
    }


    [Table("Invoices")]
    public class Invoice
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        


        [Column("PrintRefId")]
        public string PrintRefId { get; set; } = string.Empty;

        // delivery info
        [Column("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [Column("LastName")]
        public string LastName { get; set; } = string.Empty;

        [Column("Company")]
        public string Company { get; set; } = string.Empty;

        [Column("Addr1")]
        public string Addr1 { get; set; } = string.Empty;

        [Column("Addr2")]
        public string Addr2 { get; set; } = string.Empty;

        [Column("City")]
        public string City { get; set; } = string.Empty;

        [Column("Postal")]
        public string Postal { get; set; } = string.Empty;

        [Column("State")]
        public string State { get; set; } = string.Empty;

        [Column("Country")]
        public string Country { get; set; } = string.Empty;

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;

       

        // shipping info
        [Column("ShipCost", TypeName = "DECIMAL(13,2)")]
        public decimal ShipCost { get; set; } = 0;

        [Column("ShipType")]
        public string ShipType { get; set; } = string.Empty;

        [Column("ShipCarrier")]
        public string ShipCarrier { get; set; } = string.Empty;

        [Column("EstDelivOn")]
        public DateTime EstDelivOn { get; set; } = DateTime.Now;

        [Column("ShipStatus")]
        public string ShipStatus { get; set; } = string.Empty;


        // invoice info
        [Column("DBNum")]
        public int DBNum { get; set; }

        [NotMapped]
        public string InvId
        {
            get
            {
                return DBNum.ToString(); // do conversion if required
            }
        }
        [Column("Discount", TypeName = "DECIMAL(13,2)")]
        public decimal Discount { get; set; } = 0;

        [Column("PaidAmount", TypeName = "DECIMAL(13,2)")]
        public decimal PaidAmount { get; set; } = 0;
        
        [Column("PaidAt")]
        public DateTime PaidAt { get; set; }

        [Column("InvDate")]
        public DateTime InvDate { get; set; } = DateTime.Now;

        [Column("InvStatus")]
        public string InvStatus { get; set; } // open, paid



        [Column("CreatedAt")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset CreatedAt { get; set; }

        [Column("ModifiedAt")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset ModifiedAt { get; set; }

        
        [Column("Customer")]
        public PikchaUser Customer { get; set; }
                

        public List<InvoiceDetail> InvDetails { get; set; }
    }

    [Table("InvoiceDetails")]
    public class InvoiceDetail
    {

        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Column("UnitPrice", TypeName = "DECIMAL(13,2)")]
        public decimal UnitPrice { get; set; } = 0;

        [Column("Qty")]
        public int Qty { get; set; } = 1;

        [Column("Discount", TypeName = "DECIMAL(13,2)")]
        public decimal Discount { get; set; } = 0;

        [Column("Invoice")]
        public Invoice Invoice { get; set; }

        [Column("ImageProduct")]
        public ImageProduct ImageProduct { get; set; }
    }

}
