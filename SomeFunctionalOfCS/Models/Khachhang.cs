using System;
using System.Collections.Generic;

namespace SomeFunctionalOfCS.Models;

public partial class Khachhang
{
    public int KhachhangId { get; set; }

    public string? HoTen { get; set; }

    public string? TenLienLac { get; set; }

    public string? Diachi { get; set; }

    public string? Thanhpho { get; set; }

    public string? MaBuudien { get; set; }

    public string? QuocGia { get; set; }
}
