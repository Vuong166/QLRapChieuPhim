using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using gRPCRapChieuPhim;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QLRapChieuPhim.Models;

namespace QLRapChieuPhim.Pages
{
    public class CapNhatPhimModel : PageModel
    {
        public PhimModel Phim { get; set; }
        public IActionResult OnGet(int phimid)
        {
            if (phimid <= 0)
                //Response.Redirect("/DanhSachPhim");
                return RedirectToPage("DanhSachPhim");
            else
            {
                var channel = GrpcChannel.ForAddress(Common.ServiceLink);
                var client = new RapChieuPhim.RapChieuPhimClient(channel);

                var input = new Input.Types.Phim { Id = phimid };
                var response = client.XemThongTinPhim(input);
                Phim = new PhimModel();
                PropertyCopier<Output.Types.Phim, PhimModel>.Copy(response, Phim);
            }
            return Page();
        }
        public void OnPost()
        {
            
        }
        
    }
}
