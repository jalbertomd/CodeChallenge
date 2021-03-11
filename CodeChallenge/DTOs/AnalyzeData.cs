using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.DTOs
{
    public class AnalyzeData
    {
        [Required]
        public string Text { get; set; }
    }
}
