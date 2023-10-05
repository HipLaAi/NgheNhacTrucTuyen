use NgheNhacTrucTuyen
go

--thu tuc them tai khoan
create proc createtaikhoan
	@idloaitaikhoan int,
	@tendangnhap char(50),
	@matkhau char(50),
	@email char(100)
as
begin
	if(not exists (select * from TaiKhoan where TenDangNhap = @tendangnhap))
	begin
		insert into TaiKhoan
		values
		(@idloaitaikhoan,@tendangnhap,HASHBYTES('SHA2_256', @matkhau),@email)
	end
end
go

--thu tuc them chi tiet tai khoan
create proc createchitiettaikhoan
	@idtaikhoan int,
	@hoten nvarchar(50),
	@gioitinh nvarchar(3),
	@ngaysinh date,
	@sdt char(11),
	@diachi nvarchar(250),
	@anhdaidien nvarchar(500)
as
begin
	insert into ChiTietTaiKhoan
	values (@idtaikhoan,@hoten,@gioitinh,@ngaysinh,@sdt,@diachi,@anhdaidien)
end
go

--thu tuc dang nhap
create proc logintaikhoan
	@tendangnhap char(50),
	@matkhau char(50)
as
begin
	select * from TaiKhoan
	where TenDangNhap = @tendangnhap
	and MatKhau = HASHBYTES('SHA2_256', @matkhau)
end
go

--thu tuc xoa tai khoan
alter proc deletetaikhoan
	@tendangnhap char(40)
as
begin
	if(exists (select * from TaiKhoan where TenDangNhap = @tendangnhap))
	begin
		delete from TaiKhoan where TenDangNhap = @tendangnhap
	end
	else
	begin
		print N'Không tồn tại tài khoản'
	end
end
go

--thu tuc cap nhat tai khoan
alter proc updatetaikhoan
	@idtaikhoan int,
	@idloaitaikhoan int,
	@tendangnhap char(50),
	@matkhau char(50),
	@email char(100)
as
begin
	if(exists (select * from TaiKhoan where IDTaiKhoan = @idtaikhoan))
	begin
		update TaiKhoan
		set IDLoaiTaiKhoan = @idloaitaikhoan,
		MatKhau = HASHBYTES('SHA2_256', @matkhau),
		Email = @email
		where IDTaiKhoan = @idtaikhoan
		if(not exists (select * from TaiKhoan where TenDangNhap = @tendangnhap ))
		begin
			update TaiKhoan
			set TenDangNhap = @tendangnhap
			where IDTaiKhoan = @idtaikhoan
		end
	end
end
go

--chay thu tuc
exec logintaikhoan 'ngo','string'
set identity_insert TaiKhoan off
exec createtaikhoan 2,'ngo','592003','taolaadmin@gmail.com'
exec createchitiettaikhoan 5,N'Tao là ADMIN',N'Nam','10-12-2022','0000000000',N'Sư Phạm Kỹ Thật Hưng Yên',N'D:\Phát triển Dịch Vụ\BTL\API.NgheNhacTrucTuyen\IMG\Gấu nhăn mặt chỉ tay mặc áo khoa.png'
exec deletetaikhoan 'ngo'
exec updatetaikhoan 14,2,'nam','taolahiep','hiep@gmail.com'
go

use NgheNhacTrucTuyen


select * from LoaiTaiKhoan
select * from TaiKhoan
select * from ChiTietTaiKhoan

--thu tuc tim kiem tai khoan
create proc gettaikhoan
	@tendangnhap char(40)
as
begin
	select HoTen,GioiTinh, NgaySinh, SDT, DiaChi,LoaiTaiKhoan from TaiKhoan where TenDangNhap = @tendangnhap
end
go