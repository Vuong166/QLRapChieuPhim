using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QLRapChieuPhim.Pages
{
    public class CapNhatPhimModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();           
        }
        public void OnPost()
        {
            
        }
        
    }
}
