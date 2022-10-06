using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using gRPCRapChieuPhim;
using QLRapChieuPhim.Models;
using Grpc.Net.Client;

namespace QLRapChieuPhim.Pages
{
    public class PhimsapchieuModel : PageModel
    {

        public List<PhimModel> DanhSachPhim { get; private set; } = new();
        public List<TheLoaiPhimModel> DanhSachTheLoai { get; private set; } = new();
        public List<XepHangPhimModel> DanhSachXepHangPhim { get; private set; } = new();

        [BindProperty(SupportsGet = true)]
        public int TheLoaiId { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public void OnGet()
        {

            var channel = GrpcChannel.ForAddress(Common.ServiceLink);
            var client = new RapChieuPhim.RapChieuPhimClient(channel);



            var response = client.DanhSachPhimSapChieu(new Input.Types.Empty());

            DanhSachPhim = response.Items.Select(phim => new PhimModel
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

        }
    }
}
