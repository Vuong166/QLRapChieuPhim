using Grpc.Net.Client;
using gRPCRapChieuPhim;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Phim> dsPhim { get; private set; } = new List<Phim>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new RapChieuPhim.RapChieuPhimClient(channel);
            Phims phims = client.DanhSachPhimTheoTheLoai(new TheLoai() { Id = 5 });
            dsPhim = phims.Items.ToList();
        }
    }
}
