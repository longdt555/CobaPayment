using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models.Payment
{
    public class ProcessPaymentModel
    {
        public Guid OrderGUID { get; set; }
        public DateTime? OrderGuidGeneratedOnUtc { get; set; }
    }
}