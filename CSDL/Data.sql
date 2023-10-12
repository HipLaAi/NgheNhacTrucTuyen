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

create table Album
(
IDAlbum int identity(1,1) primary key,
TenAlbum nvarchar(250)
)
alter table Album 
add MoTa nvarchar(250)
alter table Album 
add AnhDaiDien nvarchar(500)
go

create table NgheSi
(
IDNgheSi int identity(1,1) primary key,
TenNgheSi nvarchar(250)
)
alter table NgheSi 
add AnhDaiDien nvarchar(500)
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
IDAlbum int foreign key references Album(IDAlbum) on delete cascade on update cascade,
IDNgheSi int foreign key references NgheSi(IDNgheSi) on delete cascade on update cascade,
Audio nvarchar(500),
IMG nvarchar(500),
ThoiLuong nvarchar(50)
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

insert into LoaiTaiKhoan
values
(N'ADMIN',Null),
(N'USER',Null)

insert into Album
values
(N'Vũ Phụng Tiên',''),
(N'Sơn Tùng MTP',''),
(N'DT Tập Rap',''),
(N'Namcocain',''),
(N'Đánh đổi',''),
(N'Phan Mạnh Quỳnh',''),
(N'Đen Vâu',''),
(N'Moi Song','')