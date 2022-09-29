using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QLRapChieuPhim.Models;
using grpcQLRapChieuPhim;

namespace QLRapChieuPhim.Pages
{
    public class DanhSachPhimModel : PageModel
    {
        public List<PhimModel> DanhSachPhim { get; private set; }
        public List<TheLoaiPhimModel> DanhSachTheLoai { get; private set; }
        public List<XepHangPhimModel> DanhSachXepHangPhim { get; private set; }

        [BindProperty(SupportsGet = true)]
        public int TheLoaiId { get; set; }

        public void OnGet()
        {
            var channel = GrpcChannel.ForAddress(Common.ServiceLink);
            var client = new RapChieuPhim.RapChieuPhimClient(channel);

            var responseTheloai = client.DocDanhSachTheLoai(new Empty());
            var responseXephang = client.DocDanhSachXepHangPhim(new Empty());

            if (TheLoaiId <= 0) TheLoaiId = responseTheloai.DanhSachTheLoai[0].Id;

            var input = new PhimTheoTheLoaiInput { TheLoaiId = this.TheLoaiId };
            var response = client.DocPhimTheoTheLoai(input);
            DanhSachPhim = response.DanhSachPhim.Select(phim => new PhimModel
            {
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

            
            /*
            foreach(var phim in DanhSachPhim)
            {
                var xephang = responseXephang.DanhSachXepHang.FirstOrDefault(x => x.Id.Equals(phim.XepHangPhimId));
                if (xephang != null)
                    phim.XepHangPhim = new XepHangPhimModel
                    {
                        Id = xephang.Id,
                        KyHieu = xephang.KyHieu,
                        Ten = xephang.Ten
                    };

                var dsTheloais = responseTheloai.DanhSachTheLoai
                                                .Where(x => phim.DanhSachTheLoaiId.Contains("," + x.Id.ToString() + ","))
                                                .Select(x => new TheLoaiPhimModel
                                                {
                                                    Id = x.Id,
                                                    Ten = x.Ten
                                                })
                                                .ToList();
                phim.DanhSachTheLoai = dsTheloais;
            }
            */
            DanhSachTheLoai = responseTheloai.DanhSachTheLoai
                                            .Select(x => new TheLoaiPhimModel
                                            {
                                                Id = x.Id,
                                                Ten = x.Ten
                                            })
                                            .ToList();
            DanhSachXepHangPhim = responseXephang.DanhSachXepHang
                                                .Select(x => new XepHangPhimModel
                                                {
                                                    Id = x.Id,
                                                    KyHieu = x.KyHieu,
                                                    Ten = x.Ten
                                                })
                                                .ToList();
        }
    }
}
