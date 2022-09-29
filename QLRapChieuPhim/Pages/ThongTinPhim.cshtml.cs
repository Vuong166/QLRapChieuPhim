using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QLRapChieuPhim.Pages
{
    public class ThongTinPhimModel : PageModel
    {
        
        public IActionResult OnGet()
        {            
            return Page();            
        }
    }
}
