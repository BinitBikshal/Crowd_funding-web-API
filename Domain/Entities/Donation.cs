using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Donation : BaseEntity
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string DonatedById { get; set; }
        public ApplicationUser? DonatedBy { get; set; }
        public Guid CampaignId { get; set; }
        public virtual Campaign? Campaign { get; set; }
    }
}
