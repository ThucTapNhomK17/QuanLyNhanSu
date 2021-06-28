CREATE PROC [dbo].[PROC_DELETE_ACCOUNT]
( @UserName nvarchar(100))
AS
BEGIN
	DELETE FROM ACC
	WHERE USERNAME = @UserName
END

CREATE PROC [dbo].[PROC_DELETE_EMPLOYEE]
( @ID INT)
AS
BEGIN
	DELETE FROM Employee
	WHERE id = @ID
END

CREATE PROC [dbo].[PROC_INSERT_ACCOUNT]
(  @DisplayName NVARCHAR(100), @UserName NVARCHAR(100), @Pass NVARCHAR(1000), @Type INT)
AS
BEGIN
	INSERT ACC(DISPLAYNAME,USERNAME,PASS,TYPE)
	VALUES (@DisplayName,@UserName,@Pass,@Type)
END

CREATE PROC [dbo].[PROC_INSERT_EMPLOYEE]
( @ID INT, @HOTEN NVARCHAR(100), @NGAYSINH DATE, @QUEQUAN NVARCHAR(200), @LUONG INT)
AS
BEGIN
	INSERT Employee(id,HoTen,NgaySinh,QueQuan,Luong)
	VALUES (@ID,@HOTEN,@NGAYSINH,@QUEQUAN,@LUONG)
END

CREATE PROC [dbo].[PROC_UPDATE_EMPLOYEE]
( @ID INT, @HOTEN NVARCHAR(100), @NGAYSINH DATE, @QUEQUAN NVARCHAR(200), @LUONG INT)
AS
BEGIN 
	UPDATE Employee
	SET HoTen = @HOTEN, NgaySinh = @NGAYSINH, QueQuan = @QUEQUAN, Luong = @LUONG
	WHERE id = @ID
END

CREATE proc [dbo].[USP_UpdateAccount]
	@userName NVARCHAR(100),@displayName nvarchar(100), @password nvarchar(100), @newPassword nvarchar(100)
AS
BEGIN
	declare @isRightPass int=0
	select @isRightPass=COUNT(*) from ACC where USERNAME=@userName and PASS=@password
	if(@isRightPass=1)
	begin
		if(@newPassword=NULL or @newPassword='')
		begin
			update ACC set DISPLAYNAME=@displayName where USERNAME=@userName
		end
		else
			update ACC set DISPLAYNAME=@displayName, PASS=@newPassword where USERNAME=@userName
	end
END