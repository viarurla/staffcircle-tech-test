using System;

namespace api.Models;

public class MessageQueryParameters
{
    public int PageNumber { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public DateTime AtDate { get; set; }
    
    public MessageQueryParameters()
    {
        
    }

}