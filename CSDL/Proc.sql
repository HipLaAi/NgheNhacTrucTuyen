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

-- thu tuc lay thong tin tai khoan
create proc getbyidtaikhoan
	@idtaikhoan int
as
begin
	select * from TaiKhoan
	where IDTaiKhoan = @idtaikhoan
	end;
go

---------thu tuc lien quan den album-------------------
-- thu tuc them album
alter proc createalbum
	@idnghesi int,
	@tenalbum nvarchar(250),
	@anhdaidien nvarchar(500),
	@mota nvarchar(250),
	@list_json_chitietalbum nvarchar(max)
as
    begin
		declare @idalbum int;
        insert into Album
		(IDNgheSi,TenAlbum,AnhDaiDien,MoTa)
        values
		(@idnghesi,@tenalbum,@anhdaidien,@mota)
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
						ct.IDAlbum,
                        ct.TenAlbum,
						ct.IDNgheSi,
						ct.AnhDaiDien,
						ct.MoTa,
						ct.SoLuongBaiHat
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
						ct.IDAlbum,
                        ct.TenAlbum,
						ct.IDNgheSi,
						ct.AnhDaiDien,
						ct.MoTa,
						ct.SoLuongBaiHat
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
	@idnghesi int,
	@tenalbum nvarchar(250),
	@anhdaidien nvarchar(500),
	@mota nvarchar(250),
	@list_json_chitietalbum nvarchar(max)
as
begin
	if(exists (select * from Album where IDAlbum = @idalbum))
	begin
		update Album
		set IDNgheSi = @idnghesi,
		TenAlbum = @tenalbum,
		AnhDaiDien = @anhdaidien,
		MoTa = @mota
		where IDAlbum = @idalbum;

		if(@list_json_chitietalbum is not null)
		begin
			select
				  json_value(a.value, '$.idChiTietAlbum') as idChiTietAlbum,
				  @idalbum as idAlbum,
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
			
			-- Update data to table with STATUS = 3
			--update ct
			--set ct.IDAlbum = r.idAlbum,
			--	ct.IDNhac = r.idNhac
			--from ChiTietAlbum ct
			--join #Results r on ct.IDChiTietAlbum = r.idChiTietAlbum
			--where r.status = '3';
			
			-- Delete data to table with STATUS = 2
			delete C
			from ChiTietAlbum c
			inner join #Results r
			on c.IDAlbum=r.idAlbum and c.IDNhac = r.idNhac
			where r.status = '2';
			drop table #Results;
		end;
	end;
end;
go

-- thu tuc lay cac album moi gan day
alter proc newalbum
	@top int
as
begin
	select top(@top) * from Album
	order by IDAlbum desc
	end;
go

-- thu tuc chi tiet album
alter proc detailalbum
	@idalbum int
as
begin
	declare @idnghesi int
	set @idnghesi = (select IDNgheSi from Album where IDAlbum = @idalbum)
	select *,
	(select top 5 n.* from Nhac n join ChiTietAlbum ct on n.IDNhac = ct.IDNhac where ct.IDAlbum = @idalbum order by newid() for json path) 
	as list_jsonnhactrongalbum,
	(select n.* from Nhac n left join ChiTietAlbum ct on n.IDNhac = ct.IDNhac and ct.IDAlbum = @idalbum where ct.IDNhac is null and n.IDNgheSi = @idnghesi  order by newid() for json path) 
	as list_jsonnhactheonghesikhongcotrongalbum
	from Album
	where IDAlbum = @idalbum
	end;
go

-- thu tuc tim kiem theo ten album
alter proc getbytenalbum
	@tenalbum nvarchar(100)
as
begin
	select * from Album 
	where TenAlbum like (N'%'+ @tenalbum +'%')
	end;
go

-- thu tuc cap nhat luot nghe
create proc updateluotnghealbum
	@idalbum int
as
begin
	update Album
	set LuotNghe = isnull(LuotNghe, 0) + 1
	where IDAlbum = @idalbum
end;

-- thu tuc lay album thinh hanh
create proc topalbumthinhhanh
	@top int
as
begin
	select top(@top) * from Album
	where LuotNghe > 0
	order by LuotNghe desc
end;
go

---------thu tuc lien quan den nhac-------------------
-- thu tuc them nhac
alter proc createnhac
	@tennhac nvarchar(50),
	@idtheloai int,
	@idnghesi int,
	@audio nvarchar(max),
	@img nvarchar(max),
	@lyrics nvarchar(max)
as
begin
	if(not exists (select * from Nhac where TenNhac = @tennhac))
	begin
		insert into Nhac
		(
		TenNhac,
		IDTheLoai,
		IDNgheSi,
		Audio,
		IMG,
		Lyrics
		)
		values
			(@tennhac,@idtheloai,@idnghesi,@audio,@img,@lyrics)
		end;
	else
		select * from Nhac where TenNhac = @tennhac
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

-- thu lay danh sach nhac theo nghe si
create proc getnhacbyidnghesi
	@idnghesi int
as
begin
	select * from Nhac where IDNgheSi = @idnghesi
	end;
go

-- thu tuc cap nhat nhac
alter proc updatenhac
	@idnhac int,
	@tennhac nvarchar(50),
	@idtheloai int,
	@idnghesi int,
	@audio nvarchar(500),
	@img nvarchar(500),
	@lyrics nvarchar(500)
as
begin
	update Nhac
	set TenNhac = @tennhac,
	IDTheLoai = @idtheloai,
	IDNgheSi = @idnghesi,
	Audio = @audio,
	IMG = @img,
	Lyrics = @lyrics
	where IDNhac = @idnhac
	end;
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
						n.Lyrics
                into #Results1
                from Nhac as n
				where  (@tennhac = '' Or n.TenNhac like N'%'+@tennhac+'%') and						
				(@idtheloai = 0 Or n.IDTheLoai = @idtheloai) and (@idnghesi = 0 or n.IDNgheSi = @idnghesi);                  
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
						n.Lyrics
                into #Results2
                from Nhac as n
				where  (@tennhac = '' Or n.TenNhac like N'%'+@tennhac+'%') and						
				(@idtheloai = 0 Or n.IDTheLoai = @idtheloai) and (@idnghesi = 0 or n.IDNgheSi = @idnghesi);              
                select @RecordCount = count(*)
                from #Results2;
                select *, 
                        @RecordCount AS RecordCount
                from #Results2;                        
                drop table #Results2; 
        end;
    end;
go	

-- thu tuc chi tiet nhac
alter proc detailnhac
	@idnhac int
as
begin
	declare @idnghesi int
	set @idnghesi = (select IDNgheSi from Nhac where IDNhac = @idnhac)
	declare @idtheloai int
	set @idtheloai = (select IDTheLoai from Nhac where IDNhac = @idnhac)
	select *,
	(select top 5 * from Nhac where IDNgheSi = @idnghesi and IDNhac <> @idnhac order by newid() for json path) as list_jsonchitietnhactheonghesi,
	(select top 5 * from Nhac where IDTheLoai = @idtheloai and IDNhac <> @idnhac order by newid() for json path) as list_jsonchitietnhactheotheloai
	from Nhac
	where IDNhac = @idnhac
	end;
go

-- thu tuc tim kiem theo ten nhac hoac ten nghe si
alter proc seachnhacvanghesi
	@ten nvarchar(100)
as
begin
	select
		n.TenNhac,
		ns.TenNgheSi,
		case
			when n.TenNhac like (N'%'+@ten+'%') then 1
			else 0
		end as MatchType
	from Nhac n join NgheSi ns 
	on n.IDNgheSi = ns.IDNgheSi
	where
        n.TenNhac like (N'%'+@ten+'%') or
        ns.TenNgheSi like (N'%'+@ten+'%')
	order by MatchType desc
	end;
go

-- thu tuc cap nhat luot nghe
create proc updateluotnghe
	@idnhac int
as
begin
	update Nhac
	set LuotNghe = isnull(LuotNghe, 0) + 1
	where IDNhac = @idnhac
end;

-- thu tuc luot nghe
create proc topnhacthinhhanh
	@top int
as
begin
	select top(@top) * from Nhac
	where LuotNghe > 0
	order by LuotNghe desc
end;
go


---------thu tuc lien quan den nghe si-------------------
-- thu tuc lay tat ca nghe si
alter proc getallnghesi
as
begin
	select * from NgheSi
	order by TenNgheSi asc
	end
go

-- thu tuc them nghe si
alter proc createnghesi
	@tennghesi nvarchar(250),
	@anhdaidien nvarchar(500),
	@mota nvarchar(500)
as
begin
	if(not exists (select * from NgheSi where TenNgheSi = @tennghesi))
	begin
		insert into NgheSi
		(TenNgheSi,AnhDaiDien,MoTa)
		values
		(@tennghesi,@anhdaidien,@mota)
	end;
	else
		select * from NgheSi where TenNgheSi = @tennghesi
	end;
go

-- thu tuc xoa nghe si
create proc deletenghesi
	@idnghesi int
as
begin
	delete from NgheSi where IDNgheSi = @idnghesi
	end;
go

exec deletenghesi 19
select * from NgheSi

-- thu tuc cap nhat nghe si
alter proc updatenghesi
	@idnghesi int,
	@tennghesi nvarchar(250),
	@anhdaidien nvarchar(500),
	@mota nvarchar(500),
	@soluongbaihat int
as
begin
	update NgheSi
	set TenNgheSi = @tennghesi,
	AnhDaiDien = @anhdaidien,
	MoTa = @mota,
	SoLuongBaiHat = @soluongbaihat
	where IDNgheSi = @idnghesi
	end;
go

-- thu tuc tim kiem nghe si
alter proc searchnghesi
	(@pageindex int, 
	@pagesize int,
	@tennghesi nvarchar(250))
as
    begin
        declare @RecordCount bigint;
        if(@pagesize <> 0)
            begin
				set nocount on;
                select(row_number() over(
                        order by IDNgheSi ASC)) AS RowNumber, 
                        n.IDNgheSi,
						n.TenNgheSi,
						n.AnhDaiDien,
						n.MoTa,
						n.SoLuongBaiHat
                into #Results1
                from NgheSi as n
				where  (@tennghesi = '' Or n.TenNgheSi like N'%'+@tennghesi+'%');                  
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
                        order by IDNgheSi ASC)) AS RowNumber, 
                        n.IDNgheSi,
						n.TenNgheSi,
						n.AnhDaiDien,
						n.MoTa,
						n.SoLuongBaiHat
                into #Results2
                from NgheSi as n
				where  (@tennghesi = '' Or n.TenNgheSi like N'%'+@tennghesi+'%');            
                select @RecordCount = count(*)
                from #Results2;
                select *, 
                        @RecordCount AS RecordCount
                from #Results2;                        
                drop table #Results2; 
        end;
    end;
go	

-- thu tuc chi tiet nghe si
create proc detailnghesi
	@idnghesi int
as
begin
	select *,
	(select top 5 * from Nhac where IDNgheSi = @idnghesi order by newid() for json path) as list_jsonchitietnhactheonghesi,
	(select top 5 * from Album where IDNgheSi = 3 order by newid() for json path) as list_jsonchitietalbumtheonghesi
	from NgheSi
	where IDNgheSi = @idnghesi
	end;
go

-- thu tuc tim kiem nghe si theo ten
create proc getbytennghesi
	@tennghesi nvarchar(100)
as
begin
	select * from NgheSi where TenNgheSi like N'%'+ @tennghesi +'%'
	end;
go

---------thu tuc lien quan den the loai-------------------
-- thu tuc lay tat ca the loai
alter proc getalltheoai
as
begin
	select * from TheLoai
	order by TenTheLoai asc
	end
go

-- thu tuc them the loai
alter proc createtheloai
	@tentheloai nvarchar(250),
	@anhdaidien nvarchar(500),
	@mota nvarchar(500)
as
begin
	if(not exists (select * from TheLoai where TenTheLoai = @tentheloai))
	begin
		insert into TheLoai
		(TenTheLoai,AnhDaiDien,MoTa)
		values
		(@tentheloai,@anhdaidien,@mota)
		end;
	else
		select * from TheLoai where TenTheLoai = @tentheloai
	end;
go

-- thu tuc xoa the loai
create proc deletetheloai
	@idtheloai int
as
begin
	delete from TheLoai where IDTheLoai = @idtheloai
	end;
go

-- thu tuc cap nhat the loai
alter proc updatetheloai
	@idtheloai int,
	@tentheloai nvarchar(250),
	@anhdaidien nvarchar(500),
	@mota nvarchar(500),
	@soluongbaihat int
as
begin
	update TheLoai
	set TenTheLoai = @tentheloai,
	AnhDaiDien = @anhdaidien,
	SoLuongBaiHat = @soluongbaihat,
	MoTa = @mota
	where IDTheLoai = @idtheloai
	end;
go

-- thu tuc tim kiem the loai
alter proc searchtheloai
	(@pageindex int, 
	@pagesize int,
	@tentheloai nvarchar(250))
as
    begin
        declare @RecordCount bigint;
        if(@pagesize <> 0)
            begin
				set nocount on;
                select(row_number() over(
                        order by IDTheLoai ASC)) AS RowNumber, 
                        t.IDTheLoai,
						t.TenTheLoai,
						t.SoLuongBaiHat,
						t.AnhDaiDien
                into #Results1
                from TheLoai as t
				where  (@tentheloai = '' Or t.TenTheLoai like N'%'+@tentheloai+'%');                  
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
                        order by IDTheLoai ASC)) AS RowNumber, 
                        t.IDTheLoai,
						t.TenTheLoai,
						t.SoLuongBaiHat,
						t.AnhDaiDien
                into #Results2
                from TheLoai as t
				where  (@tentheloai = '' Or t.TenTheLoai like N'%'+@tentheloai+'%');            
                select @RecordCount = count(*)
                from #Results2;
                select *, 
                        @RecordCount AS RecordCount
                from #Results2;                        
                drop table #Results2; 
        end;
    end;
go

-- thu tuc tim kiem theo ten the loai
create proc getbytentheloai
	@tentheloai nvarchar(100)
as
begin
	select * from TheLoai where TenTheLoai like N'%'+@tentheloai+'%'
	end;
go

-- thu tuc chi tiet the loai
create proc detailtheloai
	@idtheloai int
as
begin
	select *,
	(select top 10 * from Nhac where IDTheLoai = @idtheloai order by newid() for json path) as list_jsonchitietnhactheotheloai
	from TheLoai
	where IDTheLoai = @idtheloai
	end;
go

-- thu tuc xoa nhieu the loai
create proc deletenhieutheloai
	@list_json_idtheloai nvarchar(max)
as
    begin
       select
		json_value(js.value, '$.idTheLoai') as idTheLoai
		into #Results 
		from openjson(@list_json_idtheloai) as js;
			
		delete tl
		from TheLoai tl
		inner join #Results r
		on tl.IDTheLoai=r.idTheLoai
		drop table #Results;
end;
go

-- thu tuc cac bai hat co trong the loai
create proc toptheloai
	@top int
as
begin
	select Top(@top) * from TheLoai
	order by SoLuongBaiHat desc
end;
go

---------thu tuc lien quan den danh muc yeu thich-------------------
create proc createdanhmucyeuthich
	@idtaikhoan int,
	@anhdaidien nvarchar(500),
	@list_json_chitietdanhmucyeuthich nvarchar(max)
as
    begin
		declare @iddanhmucyeuthich int;
        insert into DanhMucYeuThich
        values
		(@idtaikhoan,@anhdaidien)
		set @iddanhmucyeuthich = (select scope_identity());
        if(@list_json_chitietdanhmucyeuthich IS NOT NULL)
            begin
                insert into ChiTietDanhMucYeuThich
				(IDDanhMucYeuThich, 
				IDNhac)
				select  @iddanhmucyeuthich, 
						json_value(a.value, '$.idNhac')
				from openjson(@list_json_chitietdanhmucyeuthich) as a;
        end;
    select '';
	end;
go

-- thu tuc nhac yeu thich
create proc toplovemusic
	@top int
as
begin
	select top(@top) * from Nhac
	end;
go






use NgheNhacTrucTuyen

select * from TaiKhoan
select * from ChiTietTaiKhoan
select * from TheLoai
select * from NgheSi
select * from TheLoai
select * from Album
select * from ChiTietAlbum
select * from Nhac
select * from DanhMucYeuThich
select * from ChiTietDanhMucYeuThich
select * from DanhSachPhat
select * from ChiTietDanhSachPhat

update Album
set SoLuongBaiHat = 3
where IDAlbum = 10

exec updateluotnghealbum 16
exec updateluotnghe 14
exec toptheloai 5