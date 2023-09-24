create database NgheNhacTrucTuyen
use NgheNhacTrucTuyen

create table TheLoai
(
IDTheLoai int identity(1,1) primary key,
TenTheLoai nvarchar(40)
)

create table Album
(
IDAlbum int identity(1,1) primary key,
TenAlbum nvarchar(40)
)

create table NgheSi
(
IDNgheSi int identity(1,1) primary key,
TenNgheSi nvarchar(40)
)

create table TaiKhoan
(
TenDangNhap char(40) primary key,
MatKhau char(40),
HoTen nvarchar(40),
GioiTinh nvarchar(3) check (GioiTinh in (N'Nam',N'Nữ')),
NgaySinh date,
SDT char(11),
DiaChi nvarchar(40),
LoaiTaiKhoan char(10) check (LoaiTaiKhoan in (N'ADMIN',N'USER'))
)

create table Nhac
(
IDNhac int identity(1,1) primary key,
TenNhac nvarchar(50),
IDTheLoai int foreign key references TheLoai(IDTheLoai) on delete cascade on update cascade,
IDAlbum int foreign key references Album(IDAlbum) on delete cascade on update cascade,
IDNgheSi int foreign key references NgheSi(IDNgheSi) on delete cascade on update cascade
)

create table DanhMucYeuThich
(
TenDangNhap char(40) foreign key references TaiKhoan(TenDangNhap) on delete cascade on update cascade,
IDNhac int foreign key references Nhac(IDNhac) on delete cascade on update cascade,
)

create table DanhSachPhatCuaNguoiDung
(
IDDanhSachPhat int identity(1,1) primary key,
TenDanhSachPhat nvarchar(40),
TenDangNhap char(40) foreign key references TaiKhoan(TenDangNhap) on delete cascade on update cascade,
)

create table NhacCoTrongDanhSachPhat
(
IDDanhSachPhat int foreign key references DanhSachPhatCuaNguoiDung(IDDanhSachPhat) on delete cascade on update cascade,
IDNhac int foreign key references Nhac(IDNhac) on delete cascade on update cascade
)

use NgheNhacTrucTuyen
select * from TaiKhoan
insert into TaiKhoan
values
('Hiep','1',N'Vũ Văn Hiệp','Nam','9-5-2003','0901519038',N'Kim Động - Hưng Yên','USER')

exec gettaikhoan 'Hiep'
