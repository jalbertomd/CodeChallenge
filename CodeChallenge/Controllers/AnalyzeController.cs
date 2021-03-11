using CodeChallenge.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyzeController : ControllerBase
    {
        [HttpPost]        
        public IActionResult Get([FromBody] AnalyzeData data)
        {
            try
            {
                // Validate input
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Remove white spaces
                var onlyText = data.Text.Replace(" ", "");

                // Count words
                var wordCount = data.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;
                                
                // Remove numbers
                var onlyCharacters = Regex.Replace(onlyText, @"[\d-]", string.Empty);

                // Count characters
                var characterCount = onlyCharacters.OrderBy(c => c).GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
                
                var result = new {
                    textLength = new { withSpaces = data.Text.Length, withoutSpaces = onlyText.Length},
                    wordCount,
                    characterCount = JsonConvert.SerializeObject(characterCount)
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
    }
}
