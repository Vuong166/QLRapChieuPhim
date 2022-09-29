using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using grpcQLRapChieuPhim;
using QLRapChieuPhim.Models;

namespace QLRapChieuPhim.Pages
{
    public class TimKiemModel : PageModel
    {
        public List<PhimModel> DanhSachPhim { get; set; } = new();
        public List<TheLoaiPhimModel> DanhSachTheLoai { get; private set; } = new();
        public List<XepHangPhimModel> DanhSachXepHangPhim { get; private set; } = new();

        [BindProperty(SupportsGet = true)]
        public string Keyword { get; set; }
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }

        public void OnGet()
        {
            //Gọi hàm TimPhim từ grpc
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new RapChieuPhim.RapChieuPhimClient(channel);

            if (string.IsNullOrEmpty(Keyword)) Keyword = "";

            var input = new TimPhimInput { Keyword = Keyword, CurrentPage = CurrentPage, PageSize = 20 };
            var response = client.TimPhim(input);

            DanhSachPhim = response.DanhSachPhim.Select(phim => new PhimModel
            {
                Id = phim.Id,
                TenPhim = phim.TenPhim,
                TenGoc = phim.TenGoc,
                DanhSachTheLoaiId = phim.DanhSachTheLoaiId,
                DaoDien = phim.DaoDien,
                DienVien = phim.DienVien,
                NgayKhoiChieu = phim.NgayKhoiChieu.ToDateTime(),
                NgonNgu = phim.NgonNgu,
                NhaSanXuat = phim.NhaSanXuat,
                NoiDung = phim.NoiDung,
                NuocSanXuat = phim.NuocSanXuat,
                Poster = phim.Poster,
                ThoiLuong = phim.ThoiLuong,
                Trailer = phim.Trailer,
                XepHangPhimId = phim.XepHangPhimId
            }).ToList();

            CurrentPage = response.CurrentPage;
            if (CurrentPage < 1) CurrentPage = 1;
            PageCount = response.PageCount;

        }
    }
}
