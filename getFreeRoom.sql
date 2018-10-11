USE [gh]
GO
/****** Object:  StoredProcedure [dbo].[getFreeRoom]    Script Date: 05-Sep-18 6:12:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[getFreeRoom] 
    @DateCheckIn dateTime,   
    @DateCheckOut dateTime ,
 @WhereClause nvarchar(max)  
AS   

    declare @tempRoomFree as table(RoomID nvarchar(30),RoomTypeID int,Floor int,Status varchar(10));
 declare @qry nvarchar(max)=(select concat('select * from Room ',@WhereClause));
 insert into @tempRoomFree(RoomID,RoomTypeId,Floor,Status)
 exec(@qry);

 declare @cIn as nvarchar(max);
 declare @cOut as nvarchar(max);

 
 set @cIn=nCHAR(39)+(SELECT CONVERT(VARCHAR,@DateCheckIn,121))+nCHAR(39);
 set @cOut=nCHAR(39)+(SELECT CONVERT(VARCHAR,@DateCheckOut,121))+nCHAR(39);

 declare @tempRoomFromCheckIn as table(RoomID nvarchar(30));

 declare @st as nvarchar(10);
 set @st=nCHAR(39)+'Pending'+nCHAR(39);

 set @qry=(select concat('select roomID from CheckInDetail d join checkin m 
 on m.CheckInID=d.CheckInId where ((',@cIn,'>=m.checkinDate and ',@cIn,'<d.checkoutDate) or (m.checkinDate >=',@cIn,' and m.checkinDate<',@cOut,')) and d.status=',@st));


 insert into @tempRoomFromCheckIn(RoomID)
 exec (@qry);

 set @qry=(select concat('select roomID from bookDetail where (',@cIn,' >= bookIn and ',@cIn,'<bookout) or (bookin >=',@cIn,' and bookin<',@cOut,')'));
 insert into @tempRoomFromCheckIn(RoomID)
 exec (@qry);

 select l.* from @tempRoomFree l left join @tempRoomFromCheckIn r on l.RoomID=r.RoomID where r.RoomID is null;