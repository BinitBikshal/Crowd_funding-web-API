using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CommentRequestDto
    {
        public string Content { get; set; }
        public Guid CampaignId { get; set; }
    }
}
