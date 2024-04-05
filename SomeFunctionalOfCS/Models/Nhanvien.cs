using System;
using System.Collections.Generic;

namespace SomeFunctionalOfCS.Models;

public partial class Nhanvien
{
    public int NhanVienId { get; set; }

    public string? Ten { get; set; }

    public string? Ho { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public string? Anh { get; set; }

    public string? Ghichu { get; set; }
}
