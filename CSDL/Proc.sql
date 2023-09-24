use NgheNhacTrucTuyen
go
create proc gettaikhoan
	@tendangnhap char(40)
as
begin
	select HoTen,GioiTinh, NgaySinh, SDT, DiaChi,LoaiTaiKhoan from TaiKhoan where TenDangNhap = @tendangnhap
end