create database NgheNhacTrucTuyen
go
use NgheNhacTrucTuyen
go

create table TheLoai
(
IDTheLoai int identity(1,1) primary key,
TenTheLoai nvarchar(250),
)
alter table TheLoai 
add AnhDaiDien nvarchar(500)
go

--drop table DanhMucYeuThich
--go
--drop table NhacCoTrongDanhSachPhat
--go
--drop table Nhac
--go
--drop table Album
--go
--drop table ChiTietAlbum
--go


create table NgheSi
(
IDNgheSi int identity(1,1) primary key,
TenNgheSi nvarchar(250),
AnhDaiDien nvarchar(500)
)
go

create table LoaiTaiKhoan
(
IDLoaiTaiKhoan int identity(1,1) primary key,
TenLoaiTaiKhoan nvarchar (50),
MoTa nvarchar(250)
)
go

create table TaiKhoan
(
IDTaiKhoan int identity(1,1) primary key,
IDLoaiTaiKhoan int foreign key references LoaiTaiKhoan(IDLoaiTaiKhoan) on delete cascade on update cascade,
TenDangNhap char(50) unique,
MatKhau char(50),
Email char(100)
)
go


create table ChiTietTaiKhoan
(
IDChiTietTaiKhoan int identity(1,1) primary key,
IDTaiKhoan int foreign key references TaiKhoan(IDTaiKhoan) on delete cascade on update cascade,
HoTen nvarchar(50),
GioiTinh nvarchar(3) check (GioiTinh in (N'Nam',N'Nữ')),
NgaySinh date,
SDT char(11),
DiaChi nvarchar(250),
AnhDaiDien nvarchar(500)
)
go

create table Nhac
(
IDNhac int identity(1,1) primary key,
TenNhac nvarchar(50),
IDTheLoai int foreign key references TheLoai(IDTheLoai) on delete cascade on update cascade,
IDNgheSi int foreign key references NgheSi(IDNgheSi) on delete cascade on update cascade,
Audio nvarchar(500),
IMG nvarchar(500),
ThoiLuong nvarchar(50),
Lyrics nvarchar(500)
)
go

create table Album
(
IDAlbum int identity(1,1) primary key,
TenAlbum nvarchar(250),
AnhDaiDien nvarchar(500),
MoTa nvarchar(250)
)
go

create table ChiTietAlbum
(
IDChiTietAlbum int identity(1,1) primary key,
IDAlbum int foreign key references Album(IDAlbum) on delete cascade on update cascade,
IDNhac int foreign key references Nhac(IDNhac) on delete cascade on update cascade
)
go

create table DanhMucYeuThich
(
IDTaiKhoan int foreign key references TaiKhoan(IDTaiKhoan) on delete cascade on update cascade,
IDNhac int foreign key references Nhac(IDNhac) on delete cascade on update cascade,
)
go

create table DanhSachPhatCuaNguoiDung
(
IDDanhSachPhat int identity(1,1) primary key,
TenDanhSachPhat nvarchar(40),
IDTaiKhoan int foreign key references TaiKhoan(IDTaiKhoan) on delete cascade on update cascade,
)
go

create table NhacCoTrongDanhSachPhat
(
IDDanhSachPhat int foreign key references DanhSachPhatCuaNguoiDung(IDDanhSachPhat) on delete cascade on update cascade,
IDNhac int foreign key references Nhac(IDNhac) on delete cascade on update cascade
)
go


use NgheNhacTrucTuyen
go

insert into NgheSi
values
(N'Namcocain',null),
(N'Obito',null),
(N'MCK',null),
(N'Phan Mạnh Quỳnh',null),
(N'Đen Vâu',null),
(N'MR.Siro',null),
(N'Sơn Tùng MTP',null)
go

insert into TheLoai
values
(N'K-Pop',null),
(N'Nhạc vàng',null),
(N'EDM',null),
(N'US-UK',null),
(N'Bolero',null),
(N'Nhạc Trẻ Remix',null),
(N'Ballad',null),
(N'Rap',null)
go

insert into LoaiTaiKhoan
values
(N'ADMIN',Null),
(N'USER',Null)
go

insert into Nhac
values
(N'Mười năm',8,5,null,null,null,null),
(N'Kiểu như tâm tình',8,1,null,null,null,null),
(N'Truy lùng',8,1,null,null,null,null),
(N'Con kể ba nghe',8,2,null,null,null,null),
(N'Đánh đổi',8,2,null,null,null,null)
go

insert into Album
values
(N'Đánh đổi',null,null)

insert into ChiTietAlbum
values
(1,4),
(1,5)


use NgheNhacTrucTuyen
select * from NgheSi
select * from LoaiTaiKhoan
select * from LoaiTaiKhoan
select * from Nhac
select * from Album
select * from ChiTietAlbum