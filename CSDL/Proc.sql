use NgheNhacTrucTuyen
go

--thu tuc tao tai khoan
alter proc createtaikhoan
(
	@idloaitaikhoan int,
	@tendangnhap char(50),
	@matkhau char(50),
	@email char(100),
	@hoten nvarchar(50),
	@gioitinh nvarchar(3),
	@ngaysinh date,
	@sdt char(11),
	@diachi nvarchar(250),
	@anhdaidien nvarchar(500)
)
as
begin
	declare @idtaikhoan int;
	if(not exists (select * from TaiKhoan where TenDangNhap = @tendangnhap))
	begin
		insert into TaiKhoan
		values
		(@idloaitaikhoan,@tendangnhap,HASHBYTES('SHA2_256', @matkhau),@email);
		set @idtaikhoan = (select scope_identity());
		if(@idtaikhoan is not null)
			begin
				insert into ChiTietTaiKhoan
				values  
				(@idtaikhoan, @hoten, @gioitinh, @ngaysinh, @sdt, @diachi, @anhdaidien)
			end;
	end;
end;
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
create proc deletetaikhoan
	@tendangnhap char(40)
as
begin
	if(exists (select * from TaiKhoan where TenDangNhap = @tendangnhap))
	begin
		delete from TaiKhoan where TenDangNhap = @tendangnhap
	end;
end;
go

--thu tuc cap nhat tai khoan
alter proc updatetaikhoan
	@idtaikhoan int,
	@tendangnhap char(50),
	@matkhau char(50),
	@email char(100),
	@hoten nvarchar(50),
	@gioitinh nvarchar(3),
	@ngaysinh date,
	@sdt char(11),
	@diachi nvarchar(250),
	@anhdaidien nvarchar(500)
as
begin
	if(exists (select * from TaiKhoan where IDTaiKhoan = @idtaikhoan))
	begin
		update TaiKhoan
		set MatKhau = HASHBYTES('SHA2_256', @matkhau),
		Email = @email
		where IDTaiKhoan = @idtaikhoan
		if(not exists (select * from TaiKhoan where TenDangNhap = @tendangnhap ))
		begin
			update TaiKhoan
			set TenDangNhap = @tendangnhap
			where IDTaiKhoan = @idtaikhoan
		end
		update ChiTietTaiKhoan
		set HoTen = @hoten,
		GioiTinh = @gioitinh,
		NgaySinh = @ngaysinh,
		SDT = @sdt,
		DiaChi = @diachi,
		AnhDaiDien = @anhdaidien
		where IDTaiKhoan = @idtaikhoan
	end;
end;
go

--thu tuc tim kiem tai khoan
alter proc searchchitiettaikhoan
	(@pageindex int, 
	@pagesize int,
	@hoten nvarchar(50),
	@diachi nvarchar(250),
	@gioitinh nvarchar(3))
as
    begin
        declare @RecordCount bigint;
        if(@pagesize <> 0)
            begin
				set nocount on;
                select(row_number() over(
                        order by HoTen ASC)) AS RowNumber, 
                        ct.IDChiTietTaiKhoan,
						ct.IDTaiKhoan,
						ct.HoTen,
						ct.DiaChi,
						ct.GioiTinh,
						ct.NgaySinh,
						ct.AnhDaiDien,
						ct.SDT
                into #Results1
                from ChiTietTaiKhoan as ct
				WHERE  (@hoten = '' Or ct.HoTen like N'%'+@hoten+'%') and						
				(@diachi = '' Or ct.DiaChi like N'%'+@diachi+'%') and (@gioitinh = '' or ct.GioiTinh like N'%'+@gioitinh + '%');                  
                select @RecordCount = count(*)
                from #Results1;
                select *, 
                        @RecordCount as RecordCount
                from #Results1
                where ROWNUMBER BETWEEN(@pageindex - 1) * @pagesize + 1 AND(((@pageindex - 1) * @pagesize + 1) + @pagesize) - 1
                        OR @pageindex = -1;
                drop table #Results1; 
            end;
            else
            begin
				set nocount on;
                select(row_number() over(
                        order by HoTen ASC)) AS RowNumber, 
                        ct.IDChiTietTaiKhoan,
						ct.IDTaiKhoan,
						ct.HoTen,
						ct.DiaChi,
						ct.GioiTinh,
						ct.NgaySinh,
						ct.AnhDaiDien,
						ct.SDT
                into #Results2
                from ChiTietTaiKhoan AS ct
				WHERE  (@hoten = '' Or ct.HoTen like N'%'+@hoten+'%') and						
				(@diachi = '' Or ct.DiaChi like N'%'+@diachi+'%') and (@gioitinh = '' or ct.GioiTinh like N'%'+@gioitinh + '%');               
                select @RecordCount = count(*)
                from #Results2;
                select *, 
                        @RecordCount AS RecordCount
                from #Results2;                        
                drop table #Results2; 
        end;
    end;
go

use NgheNhacTrucTuyen
select * from LoaiTaiKhoan
select * from TaiKhoan
select * from ChiTietTaiKhoan
select * from TaiKhoan, ChiTietTaiKhoan where TaiKhoan.IDTaiKhoan = ChiTietTaiKhoan.IDTaiKhoan
go

exec searchchitiettaikhoan 1,1,'','',''
exec updatetaikhoan 19,'Hiep','123','vuvanhiep@gmail.com',N'Vũ Văn Hiệp',N'Nam','2003-09-05','0901519038',N'Kim Động - Hưng Yên',''


select * from Album

-- thu tuc them album
alter proc craetealbum
	@tenalbum nvarchar(250),
	@anhdaidien nvarchar(250)
as
begin
	if(not exists (select * from Album where TenAlbum = @tenalbum))
	begin
		insert into Album
		values
		(@tenalbum,@anhdaidien)
	end;
end;
go

-- thu tuc cap nhat album
create proc updatealbum
	@idalbum int,
	@tenalbum nvarchar(250),
	@mota nvarchar(250),
	@anhdaidien nvarchar(250)
as
begin
	if(exists (select * from Album where IDAlbum = @idalbum))
	begin
		update Album
		set TenAlbum = @tenalbum,
		AnhDaiDien = @anhdaidien,
		MoTa = @mota
		where IDAlbum = @idalbum
	end;
end;
go

-- thu tuc xoa album
create proc deletealbum
	@idalbum int
as
begin
	delete from Album where IDAlbum = @idalbum
end;
go

-- thu tuc tim kiem album
create proc searchalbum
	(@pageindex int, 
	@pagesize int,
	@tenalbum nvarchar(250))
as
    begin
        declare @RecordCount bigint;
        if(@pagesize <> 0)
            begin
				set nocount on;
                select(row_number() over(
                        order by TenAlbum ASC)) AS RowNumber, 
                        ct.TenAlbum,
						ct.AnhDaiDien
                into #Results1
                from Album as ct
				WHERE  (@tenalbum = '' Or ct.TenAlbum like N'%'+@tenalbum+'%');                  
                select @RecordCount = count(*)
                from #Results1;
                select *, 
                        @RecordCount as RecordCount
                from #Results1
                where ROWNUMBER BETWEEN(@pageindex - 1) * @pagesize + 1 AND(((@pageindex - 1) * @pagesize + 1) + @pagesize) - 1
                        OR @pageindex = -1;
                drop table #Results1; 
            end;
            else
            begin
				set nocount on;
                select(row_number() over(
                        order by TenAlbum ASC)) AS RowNumber, 
                        ct.TenAlbum,
						ct.AnhDaiDien
                into #Results2
                from Album as ct
				WHERE  (@tenalbum = '' Or ct.TenAlbum like N'%'+@tenalbum+'%');               
                select @RecordCount = count(*)
                from #Results2;
                select *, 
                        @RecordCount AS RecordCount
                from #Results2;                        
                drop table #Results2; 
        end;
    end;
go

select * from Album




select * from Album

exec searchalbum 2,2,'',''