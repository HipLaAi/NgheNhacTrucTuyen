use NgheNhacTrucTuyen
go

----------------thu tuc lien quan den tai khoan----------------
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

---------thu tuc lien quan den album-------------------
-- thu tuc them album
create proc createalbum
	@tenalbum nvarchar(250),
	@anhdaidien nvarchar(500),
	@mota nvarchar(250),
	@list_json_chitietalbum nvarchar(max)
as
    begin
		declare @idalbum int;
        insert into Album
        values
		(@tenalbum,@anhdaidien,@mota)
		set @idalbum = (select scope_identity());
        if(@list_json_chitietalbum IS NOT NULL)
            begin
                insert into ChiTietAlbum
				(IDAlbum, 
				IDNhac)
            select  @idalbum, 
					json_value(a.value, '$.idNhac')
            from openjson(@list_json_chitietalbum) as a;
        end;
    select '';
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
alter proc searchalbum
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
                        order by IDAlbum ASC)) AS RowNumber, 
                        ct.TenAlbum,
						ct.AnhDaiDien,
						ct.MoTa
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
                        order by IDAlbum ASC)) AS RowNumber, 
                        ct.TenAlbum,
						ct.AnhDaiDien,
						ct.MoTa
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

-- thu tuc cap nhat album
alter proc updatealbum
	@idalbum int,
	@tenalbum nvarchar(250),
	@anhdaidien nvarchar(500),
	@mota nvarchar(250),
	@list_json_chitietalbum nvarchar(max)
as
begin
	if(exists (select * from Album where IDAlbum = @idalbum))
	begin
		update Album
		set TenAlbum = @tenalbum,
		AnhDaiDien = @anhdaidien,
		MoTa = @mota
		where IDAlbum = @idalbum;

		if(@list_json_chitietalbum is not null)
		begin
			select
				  json_value(a.value, '$.idChiTietAlbum') as idChiTietAlbum,
				  json_value(a.value, '$.idAlbum') as idAlbum,
				  json_value(a.value, '$.idNhac') as idNhac,
				  json_value(a.value, '$.status') as status 
				  into #Results 
			from openjson(@list_json_chitietalbum) as a;

			-- Insert data to table with STATUS = 1;
			insert into ChiTietAlbum
						(IDAlbum, 
						IDNhac) 
					select
						@idalbum,
						#Results.idNhac			 
			from  #Results 
			where #Results.status = '1' 
			
			-- Update data to table with STATUS = 2
			--update ChiTietAlbum 
			--set
			--from #Results 
			--WHERE  ChiTietAlbum.IDChiTietAlbum = #Results.idChiTietAlbum AND #Results.status = '2';
			
			-- Delete data to table with STATUS = 3

			delete C
			from ChiTietAlbum C
			inner join #Results R
			on C.IDChiTietAlbum=R.idChiTietAlbum
			where R.status = '3';
			drop table #Results;
		end;
	end;
end;
go

---------thu tuc lien quan den nhac-------------------
-- thu tuc them nhac
create proc createnhac
	@tennhac nvarchar(50),
	@idtheloai int,
	@idnghesi int,
	@audio nvarchar(500),
	@img nvarchar(500),
	@thoiluong nvarchar(50),
	@lyrics nvarchar(500)
as
begin
	insert into Nhac
	values
	(@tennhac,@idtheloai,@idnghesi,@audio,@img,@thoiluong,@lyrics)
	end;
go

-- thu tuc xoa nhac
create proc deletenhac
	@idnhac int
as
begin
	delete from Nhac where IDNhac = @idnhac
	end;
go

-- thu tuc cap nhat nhac
create proc updatenhac
	@idnhac int,
	@tennhac nvarchar(50),
	@idtheloai int,
	@idnghesi int,
	@audio nvarchar(500),
	@img nvarchar(500),
	@thoiluong nvarchar(50),
	@lyrics nvarchar(500)
as
begin
	update Nhac
	set TenNhac = @tennhac,
	IDTheLoai = @idtheloai,
	IDNgheSi = @idnghesi,
	Audio = @audio,
	IMG = @img,
	ThoiLuong = @thoiluong,
	Lyrics = @lyrics
	where IDNhac = @idnhac
end;


select * from Nhac

use NgheNhacTrucTuyen
select * from LoaiTaiKhoan
select * from TaiKhoan
select * from ChiTietTaiKhoan
select * from TaiKhoan, ChiTietTaiKhoan where TaiKhoan.IDTaiKhoan = ChiTietTaiKhoan.IDTaiKhoan
go

-- thu tuc tim kiem nhac
alter proc searchnhac
	(@pageindex int, 
	@pagesize int,
	@tennhac nvarchar(50),
	@idtheloai int,
	@idnghesi int)
as
    begin
        declare @RecordCount bigint;
        if(@pagesize <> 0)
            begin
				set nocount on;
                select(row_number() over(
                        order by IDNhac ASC)) AS RowNumber, 
                        n.IDNhac,
						n.TenNhac,
						n.IDTheLoai,
						n.IDNgheSi,
						n.Audio,
						n.IMG,
						n.ThoiLuong,
						n.Lyrics
                into #Results1
                from Nhac as n
				WHERE  (@tennhac = '' Or n.TenNhac like N'%'+@tennhac+'%') and						
				(@idtheloai = '' Or n.IDTheLoai like N'%'+@idtheloai+'%') and (@idnghesi = '' or n.IDNgheSi like N'%'+@idnghesi + '%');                  
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
                order by IDNhac ASC)) AS RowNumber, 
                        n.IDNhac,
						n.TenNhac,
						n.IDTheLoai,
						n.IDNgheSi,
						n.Audio,
						n.IMG,
						n.ThoiLuong,
						n.Lyrics
                into #Results2
                from Nhac as n
				WHERE  (@tennhac = '' Or n.TenNhac like N'%'+@tennhac+'%') and						
				(@idtheloai = '' Or n.IDTheLoai like N'%'+@idtheloai+'%') and (@idnghesi = '' or n.IDNgheSi like N'%'+@idnghesi + '%');            
                select @RecordCount = count(*)
                from #Results2;
                select *, 
                        @RecordCount AS RecordCount
                from #Results2;                        
                drop table #Results2; 
        end;
    end;
go	

exec searchchitiettaikhoan 1,1,'','',''
exec updatetaikhoan 19,'Hiep','123','vuvanhiep@gmail.com',N'Vũ Văn Hiệp',N'Nam','2003-09-05','0901519038',N'Kim Động - Hưng Yên',''

use NgheNhacTrucTuyen
select * from TheLoai
select * from NgheSi
select * from TaiKhoan
select * from ChiTietTaiKhoan
select * from ChiTietAlbum

exec searchalbum 2,2,''

