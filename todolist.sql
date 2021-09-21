create database todolist
use todolist
go
---Trân
create table chucvu
(
	chucvu_id int IDENTITY(1,1) PRIMARY KEY,
	chucvu_ten nvarchar(100) not null
)

create table nhanvien
(
	nhanvien_id int IDENTITY(1,1) PRIMARY KEY,
	nhanvien_ten nvarchar(100) not null,
	nhanvien_gioitinh nvarchar(10) not null,
	nhanvien_ngaysinh date not null,
	nhanvien_cmnd int not null,
	nhanvien_email varchar(100) not null,
	nhanvien_matkhau varchar(50) not null,
	chucvu_id int,
	CONSTRAINT fk_htk_id_cv
	FOREIGN KEY (chucvu_id)
	REFERENCES chucvu (chucvu_id)
)

create table phamvi
(
	phamvi_id int IDENTITY(1,1) PRIMARY KEY,
	phamvi_ten nvarchar(30) not null
)

create table congviec
(
	congviec_id int IDENTITY(1,1) PRIMARY KEY,
	congviec_ten nvarchar(200) not null,
	congviec_ngaybatdau date not null,
	congviec_ngayketthuc date not null,
	congviec_ngayhoanthanh date ,
	phamvi_id int not null,
	FOREIGN KEY (phamvi_id)
	REFERENCES phamvi (phamvi_id)
)

create table binhluan
(
	id int IDENTITY(1,1) PRIMARY KEY,
	congviec_id int not null,
	nhanvien_id int not null,
	binhluan_noidung nvarchar(300) not null,
	binhluan_thoigianbinhluan datetime not null,
	FOREIGN KEY (congviec_id)
	REFERENCES congviec (congviec_id),
	FOREIGN KEY (nhanvien_id)
	REFERENCES nhanvien (nhanvien_id)
)

create table quyen
(
	quyen_id int IDENTITY(1,1) PRIMARY KEY,
	quyen_ten nvarchar(30) not null
)

create table nhanvien_congviec
(
	id int IDENTITY(1,1) PRIMARY KEY,
	nhanvien_id int not null,
	congviec_id int not null,
	quyen_id int not null,
	FOREIGN KEY (nhanvien_id)
	REFERENCES nhanvien (nhanvien_id),
	FOREIGN KEY (congviec_id)
	REFERENCES congviec (congviec_id),
	FOREIGN KEY (quyen_id)
	REFERENCES quyen (quyen_id)
)
create table filechiase
(
	id int IDENTITY(1,1) PRIMARY KEY,
	nhanvien_id int not null,
	congviec_id int not null,
	tenfile nvarchar(100) not null,
	FOREIGN KEY (nhanvien_id)
	REFERENCES nhanvien (nhanvien_id),
	FOREIGN KEY (congviec_id)
	REFERENCES congviec (congviec_id)
)
select * from filechiase

INSERT INTO chucvu (chucvu_ten) VALUES (N'Quản Lý');
INSERT INTO chucvu (chucvu_ten) VALUES (N'Nhân viên');
select * from chucvu

INSERT INTO nhanvien (nhanvien_ten, nhanvien_gioitinh, nhanvien_ngaysinh, nhanvien_cmnd, nhanvien_email, nhanvien_matkhau, chucvu_id) VALUES (N'Lê Ngọc Vũ', N'Nam', '2000-02-22', '123456789', 'levu2000201516@gmail.com', '12345678', '1');
INSERT INTO nhanvien (nhanvien_ten, nhanvien_gioitinh, nhanvien_ngaysinh, nhanvien_cmnd, nhanvien_email, nhanvien_matkhau, chucvu_id) VALUES (N'Huỳnh Nhật Quế Trân', N'Nữ', '2000-12-02', '987654321', 'huynhquetran5239@gmail.com', '87654321', '2');
INSERT INTO nhanvien (nhanvien_ten, nhanvien_gioitinh, nhanvien_ngaysinh, nhanvien_cmnd, nhanvien_email, nhanvien_matkhau, chucvu_id) VALUES (N'Lê Minh', N'Nữ', '1999-12-02', '123454321', 'minh123@gmail.com', 'minh123', '2');
INSERT INTO nhanvien (nhanvien_ten, nhanvien_gioitinh, nhanvien_ngaysinh, nhanvien_cmnd, nhanvien_email, nhanvien_matkhau, chucvu_id) VALUES (N'Võ Lâm Tùng', N'Nam', '1978-03-14', '124253821', 'tunglam@gmail.com', 'lam456', '2');
select * from nhanvien

INSERT INTO phamvi (phamvi_ten) VALUES (N'Công cộng');
INSERT INTO phamvi (phamvi_ten) VALUES (N'Riêng tư');
select * from phamvi

INSERT INTO congviec (congviec_ten, congviec_ngaybatdau, congviec_ngayketthuc, phamvi_id) VALUES (N'Xây dựng Web Todo List', '2021-04-05', '2021-04-20', '1');
INSERT INTO congviec (congviec_ten, congviec_ngaybatdau, congviec_ngayketthuc, phamvi_id) VALUES (N'Dự án 1', '2021-04-06', '2021-04-10', '2');
INSERT INTO congviec (congviec_ten, congviec_ngaybatdau, congviec_ngayketthuc, phamvi_id) VALUES (N'CV1', '2021-04-08', '2021-04-20', '1');
INSERT INTO congviec (congviec_ten, congviec_ngaybatdau, congviec_ngayketthuc, phamvi_id) VALUES (N'CV2', '2021-04-08', '2021-04-10', '2');
INSERT INTO congviec (congviec_ten, congviec_ngaybatdau, congviec_ngayketthuc, phamvi_id) VALUES (N'CV3', '2021-04-11', '2021-04-15', '2');
select * from congviec

INSERT INTO quyen (quyen_ten) VALUES (N'Người tạo công việc');
INSERT INTO quyen (quyen_ten) VALUES (N'Người tham gia công việc');
select * from quyen

INSERT INTO nhanvien_congviec(nhanvien_id, congviec_id, quyen_id) VALUES ('2','1','1');
INSERT INTO nhanvien_congviec(nhanvien_id, congviec_id, quyen_id) VALUES ('3','2','2');
INSERT INTO nhanvien_congviec(nhanvien_id, congviec_id, quyen_id) VALUES ('2','4','2');
select * from nhanvien_congviec

--delete from nhanvien_congviec where nhanvien_id=2 and quyen_id=2
INSERT INTO binhluan (congviec_id, nhanvien_id, binhluan_noidung, binhluan_thoigianbinhluan) VALUES ('4','2','hi',GETDATE());
select * from binhluan
