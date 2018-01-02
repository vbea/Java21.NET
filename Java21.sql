create database sqlvbes
go
use sqlvbes
go
create table Video
(
	id int primary key identity(1,1),
	name varchar(100) not null,
	url varchar(150) not null,
	url2 varchar(150) not null
)

create table Keys
(
    id int primary key identity(1,1),
	keys varchar(50) not null,
	maxc int not null,
	curr int not null,
	ver char(1) not null,
	cdate datetime not null,
	mark varchar(200)
)

create table Users
(
	id int primary key identity(1,1),
	name varchar(20) not null,
	psd varchar(100) not null,
	roles int not null,
	nickname varchar(20) not null,
	gender bit not null,
	email varchar(100) not null,
	qq varchar(14) null,
	mobile varchar(20) null,
	birthday datetime not null,
	address varchar(100) null,
	valid bit not null,
	cdate datetime not null,
	mark varchar(1000),
	head varchar(200)
)

create table Quotations
(
	id int primary key identity(1,1),
	sentence varchar(500) not null,
	cdate datetime,
	tips bit not null
)

create table Knowledge
(
	id int primary key identity(1,1),
	title varchar(100) not null,
	artical text,
	cdate datetime,
	cuser varchar(50),
	edate datetime,
	euser varchar(50),
	comment datetime,
	cread int not null,
	valid bit not null,
)

create table Article
(
	id int primary key identity(1000,1),
	title varchar(100) not null,
	category int not null,
	artical text,
	cdate datetime,
	cuser varchar(50),
	edate datetime,
	euser varchar(50),
	comment datetime,
	cread int not null,
	valid bit not null,
)

create table Category
(
	id int primary key identity(1,1),
	catename varchar(20) not null,
	remark varchar(200) null
)

create table Comment
(
	id int primary key identity(1,1),
	aid int not null,
	uid varchar(20) not null,
	star int not null, 
	comment nvarchar(MAX),
	cdate datetime not null,
	device varchar(50)
)

create table Feedback
(
	id int primary key identity(1,1),
	suggest varchar(1000),
	device varchar(50),
	contact varchar(100),
	cdate datetime not null
)

--历史版本下载列表
create table Download
(
	id int primary key identity(1,1),
	ver varchar(10) not null,
	url varchar(100) not null,
	cdate datetime not null
)

--资料下载
create table Material
(
	id int primary key identity(1,1),
	title varchar(500) not null,
	url varchar(100) not null,
	psd varchar(20) null,
	cdate datetime not null,
	remark varchar(500) null,
	download int null
)

create proc prPager
	@PageSize INT, ----每页记录数
	@CurrentCount INT, ----当前记录数量（页码*每页记录数）
	@TableName NVARCHAR(200), ----表名称
	@Where NVARCHAR(800), ----查询条件
	@Order NVARCHAR(800),----排序条件
	@TotalCount INT OUTPUT ----记录总数
AS
	DECLARE @sqlSelect    NVARCHAR(2000) ----局部变量（sql语句），查询记录集
	DECLARE @sqlGetCount  NVARCHAR(2000) ----局部变量（sql语句），取出记录集总数
	
	
	SET @sqlSelect = 'SELECT * FROM  ' 
	    + '    (SELECT ROW_NUMBER()  OVER( ORDER BY ' + @Order +
	    ' ) AS RowNumber,* '
	    + '        FROM ' + @TableName 
	    + '        WHERE ' + @Where 
	    + '     ) as  T2 ' 
	    + ' WHERE T2.RowNumber<= ' + STR(@CurrentCount + @PageSize) +
	    ' AND T2.RowNumber>' + STR(@CurrentCount) 
	
	SET @sqlGetCount = 'SELECT @TotalCount = COUNT(*) FROM ' + @TableName 
	    + ' WHERE ' + @Where
	
	
	EXEC (@sqlSelect) 
	EXEC SP_EXECUTESQL @sqlGetCount,
	     N'@TotalCount INT OUTPUT',
	     @TotalCount OUTPUT


insert into Video (name ,url, url2)
values
('java_1','http://player.youku.com/player.php/sid/XNTk2NDkzNTI4/v.swf','http://player.youku.com/embed/XNTk2NDkzNTI4'),
('java_2','http://player.youku.com/player.php/sid/XNTk2NDk2OTg4/v.swf','http://player.youku.com/embed/XNTk2NDk2OTg4'),
('java_3','http://player.youku.com/player.php/sid/XNTk2NDk5ODE2/v.swf','http://player.youku.com/embed/XNTk2NDk5ODE2'),
('java_4','http://player.youku.com/player.php/sid/XNTk2NTAzMTU2/v.swf','http://player.youku.com/embed/XNTk2NTAzMTU2'),
('java_5','http://player.youku.com/player.php/sid/XNTk2NTA3ODcy/v.swf','http://player.youku.com/embed/XNTk2NTA3ODcy'),
('java_6','http://player.youku.com/player.php/sid/XNTk2NTIxNjEy/v.swf','http://player.youku.com/embed/XNTk2NTIxNjEy'),
('java_7','http://player.youku.com/player.php/sid/XNTk2NTQzMzYw/v.swf','http://player.youku.com/embed/XNTk2NTQzMzYw'),
('java_8','http://player.youku.com/player.php/sid/XNTk2OTE5OTg0/v.swf','http://player.youku.com/embed/XNTk2OTE5OTg0'),
('java_9','http://player.youku.com/player.php/sid/XNTk2OTIzNTA0/v.swf','http://player.youku.com/embed/XNTk2OTIzNTA0'),
('java_10','http://player.youku.com/player.php/sid/XNTk2OTc3ODA4/v.swf','http://player.youku.com/embed/XNTk2OTc3ODA4'),
('java_11','http://player.youku.com/player.php/sid/XNTk4NzIxMjEy/v.swf','http://player.youku.com/embed/XNTk4NzIxMjEy'),
('java_12','http://player.youku.com/player.php/sid/XNjAxMzM0OTQ0/v.swf','http://player.youku.com/embed/XNjAxMzM0OTQ0'),
('java_13','http://player.youku.com/player.php/sid/XNjA3NjAwNTcy/v.swf','http://player.youku.com/embed/XNjA3NjAwNTcy'),
('java_14','http://player.youku.com/player.php/sid/XNjA5NDQ1OTUy/v.swf','http://player.youku.com/embed/XNjA5NDQ1OTUy'),
('java_15','http://player.youku.com/player.php/sid/XNjEwNzQ4NjIw/v.swf','http://player.youku.com/embed/XNjEwNzQ4NjIw'),
('java_16','http://player.youku.com/player.php/sid/XNjEwNzU1NzA4/v.swf','http://player.youku.com/embed/XNjEwNzU1NzA4'),
('java_17','http://player.youku.com/player.php/sid/XNjExNDQwOTMy/v.swf','http://player.youku.com/embed/XNjExNDQwOTMy'),
('java_18','http://player.youku.com/player.php/sid/XNjEzMTk3ODYw/v.swf','http://player.youku.com/embed/XNjEzMTk3ODYw'),
('java_19','http://player.youku.com/player.php/sid/XNjE0MDcyNjM2/v.swf','http://player.youku.com/embed/XNjE0MDcyNjM2'),
('java_20','http://player.youku.com/player.php/sid/XNjIxNDg1MTky/v.swf','http://player.youku.com/embed/XNjIxNDg1MTky'),
('java_21','http://player.youku.com/player.php/sid/XNjIxNDkxNDIw/v.swf','http://player.youku.com/embed/XNjIxNDkxNDIw'),
('java_22','http://player.youku.com/player.php/sid/XNjIxNTIxNjA0/v.swf','http://player.youku.com/embed/XNjIxNTIxNjA0'),
('java_23','http://player.youku.com/player.php/sid/XNjM0MDIzMjk2/v.swf','http://player.youku.com/embed/XNjM0MDIzMjk2'),
('java_24','http://player.youku.com/player.php/sid/XNjM4NjMxMjky/v.swf','http://player.youku.com/embed/XNjM4NjMxMjky'),
('java_25','http://player.youku.com/player.php/sid/XNjM4NjMzNDI0/v.swf','http://player.youku.com/embed/XNjM4NjMzNDI0'),
('java_26','http://player.youku.com/player.php/sid/XNjM4NjQ4NTI0/v.swf','http://player.youku.com/embed/XNjM4NjQ4NTI0'),
('java_27','http://player.youku.com/player.php/sid/XNjgyNTU5Njc2/v.swf','http://player.youku.com/embed/XNjgyNTU5Njc2'),
('java_28','http://player.youku.com/player.php/sid/XNjgyNTYyODQ4/v.swf','http://player.youku.com/embed/XNjgyNTYyODQ4'),
('java_29','http://player.youku.com/player.php/sid/XNjgyNTY0MDAw/v.swf','http://player.youku.com/embed/XNjgyNTY0MDAw'),
('java_30','http://player.youku.com/player.php/sid/XNjgzNjI4MzEy/v.swf','http://player.youku.com/embed/XNjgzNjI4MzEy'),
('java_31','http://player.youku.com/player.php/sid/XNjgzNjM4NDQw/v.swf','http://player.youku.com/embed/XNjgzNjM4NDQw'),
('java_32','http://player.youku.com/player.php/sid/XNjgzODQ5MDk2/v.swf','http://player.youku.com/embed/XNjgzODQ5MDk2'),
('java_33','http://player.youku.com/player.php/sid/XNjgzODUwMDAw/v.swf','http://player.youku.com/embed/XNjgzODUwMDAw'),
('java_34','http://player.youku.com/player.php/sid/XNjgzODUwNzYw/v.swf','http://player.youku.com/embed/XNjgzODUwNzYw'),
('java_35','http://player.youku.com/player.php/sid/XNjgzODUxNTI0/v.swf','http://player.youku.com/embed/XNjgzODUxNTI0')
go

--name	psd	roles	nickname	gender	email	valid	cdate	mark
--admin	3BE54FC5CCBBA30ACAEBF6F25C2CF5A1	1	Admin	0	vbea@foxmail.com	1	2016-01-07 12:00:00.000	管理员邠心工作室

insert into Article
(title,category,artical,cdate,cuser,edate,euser,comment,cread,valid)
select title,1,artical,cdate,cuser,edate,euser,comment,cread,valid from Knowledge

--升级用户表
--备份数据
select * into Users1 from Users
drop table Users
--创建新表
--恢复数据
insert into Users (name,psd,roles,nickname,gender,email,qq,mobile,birthday,address,valid,cdate,mark,head)
select name,psd,roles,nickname,gender,email,'','','1990-01-01','',valid,cdate,mark,head from Users1


--修改字段
alter table Feedback alter column suggest varchar(1000) null

--添加字段
alter table Material add download int null