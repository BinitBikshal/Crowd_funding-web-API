using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public Guid CampaignId { get; set; }
        public virtual Campaign? Campaign { get; set; }
    }
}
