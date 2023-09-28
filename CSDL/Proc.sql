use NgheNhacTrucTuyen
go

--thu tuc tim kiem tai khoan
create proc gettaikhoan
	@tendangnhap char(40)
as
begin
	select HoTen,GioiTinh, NgaySinh, SDT, DiaChi,LoaiTaiKhoan from TaiKhoan where TenDangNhap = @tendangnhap
end

--thu tuc tao tai khoan
create proc createtaikhoan
	@tendangnhap char(40),
	@matkhau char(40),
	@hoten nvarchar(40),
	@gioitinh nvarchar(3),
	@ngaysinh date,
	@sdt char(11),
	@diachi nvarchar(40),
	@loaitaikhoan char(10)
as
begin
	if(not exists (select * from TaiKhoan where TenDangNhap = @tendangnhap))
	begin
		insert into TaiKhoan
		values
		(@tendangnhap,HASHBYTES('SHA2_256', @matkhau),@hoten,@gioitinh,@ngaysinh,@sdt,@diachi,@loaitaikhoan)
	end
end
go

	select * from TaiKhoan

exec createtaikhoan 'Hiep',''
