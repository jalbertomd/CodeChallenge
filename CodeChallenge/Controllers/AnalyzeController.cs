using CodeChallenge.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}

                // Remove white spaces
                var onlyText = data.Text.Replace(" ", "").ToLower();

                // Count words
                var wordCount = data.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;
                                
                // Get only letters
                var onlyCharacters = Regex.Replace(onlyText, @"[^A-Za-z]+", string.Empty);

                // Count characters
                var characterCount = onlyCharacters.OrderBy(c => c).GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

                dynamic output = new List<dynamic>();

                foreach (var item in characterCount)
                {
                    var row = new ExpandoObject() as IDictionary<string, object>;
                    row.Add(item.Key.ToString(), item.Value);
                    output.Add(row);
                }

                var result = new {
                    textLength = new { withSpaces = data.Text.Length, withoutSpaces = onlyText.Length},
                    wordCount,
                    characterCount = output
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
