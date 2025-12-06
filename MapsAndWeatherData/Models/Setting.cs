using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapsAndWeatherData.Models
{
    public class Setting
    {
        
        public Guid Id { get;set; }        
        public string? Key { get; set; }
        public string? Value { get; set; }   
    }
}
