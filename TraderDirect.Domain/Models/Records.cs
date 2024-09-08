using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderDirect.Domain.Models;

public record ExecuteTradesServiceRequest(List<ExecuteTradeServiceRequest> Trades);
public record ExecuteTradeServiceRequest
{
    public int UserId { get; set; }
    public required string Asset { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
}

