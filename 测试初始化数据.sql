--测试初始化数据
INSERT INTO dbo.SYS_USER
        ( Name ,
          UserName ,
          Password ,
          DepartmentId ,
          Phone ,
          Email ,
          Enabled ,
          CreateBy ,
          CreateDate ,
          LastUpdateBy ,
          LastUpdateDate ,
          Active ,
          UserBelongTo ,
          CmpyBelongTo
        )
VALUES  ( N'admin' , -- Name - nvarchar(50)
          N'admin' , -- UserName - nvarchar(50)
          N'admin' , -- Password - nvarchar(50)
          0 , -- DepartmentId - bigint
          N'110' , -- Phone - nvarchar(50)
          N'' , -- Email - nvarchar(50)
          1 , -- Enabled - bit
          N'admin' , -- CreateBy - nvarchar(50)
          '2016-04-05 02:19:39' , -- CreateDate - datetime
          N'admin' , -- LastUpdateBy - nvarchar(50)
          '2016-04-05 02:19:39' , -- LastUpdateDate - datetime
          1 , -- Active - bit
          0 , -- UserBelongTo - bigint
          0  -- CmpyBelongTo - bigint
        )
        
 INSERT INTO dbo.SYS_MENU
        ( ParentId ,
          Name ,
          ImageUrl ,
          NavigateUrl ,
          SortIndex ,
          CreateBy ,
          CreateDate ,
          LastUpdateBy ,
          LastUpdateDate ,
          Active ,
          UserBelongTo ,
          CmpyBelongTo
        )
VALUES  ( 0 , -- ParentId - bigint
          N'系统管理' , -- Name - nvarchar(50)
          N'' , -- ImageUrl - nvarchar(50)
          N'' , -- NavigateUrl - nvarchar(50)
          0 , -- SortIndex - int
          N'' , -- CreateBy - nvarchar(50)
          '2016-04-05 02:20:44' , -- CreateDate - datetime
          N'' , -- LastUpdateBy - nvarchar(50)
          '2016-04-05 02:20:44' , -- LastUpdateDate - datetime
          1 , -- Active - bit
          0 , -- UserBelongTo - bigint
          0  -- CmpyBelongTo - bigint
        )          
        
INSERT INTO dbo.SYS_MENU
        ( ParentId ,
          Name ,
          ImageUrl ,
          NavigateUrl ,
          SortIndex ,
          CreateBy ,
          CreateDate ,
          LastUpdateBy ,
          LastUpdateDate ,
          Active ,
          UserBelongTo ,
          CmpyBelongTo
        )
VALUES  ( 2 , -- ParentId - bigint
          N'菜单管理' , -- Name - nvarchar(50)
          N'' , -- ImageUrl - nvarchar(50)
          N'~/Modules/SYS/Menu/Menu.aspx' , -- NavigateUrl - nvarchar(50)
          0 , -- SortIndex - int
          N'' , -- CreateBy - nvarchar(50)
          '2016-04-05 02:20:44' , -- CreateDate - datetime
          N'' , -- LastUpdateBy - nvarchar(50)
          '2016-04-05 02:20:44' , -- LastUpdateDate - datetime
          1 , -- Active - bit
          0 , -- UserBelongTo - bigint
          0  -- CmpyBelongTo - bigint
        )   
        
        
        
        
        
 INSERT INTO dbo.SYS_ACTION
         ( Name ,
           MenuId ,
           MenuName ,
           ControlId ,
           ControlType ,
           CreateBy ,
           CreateDate ,
           LastUpdateBy ,
           LastUpdateDate ,
           Active ,
           UserBelongTo ,
           CmpyBelongTo
         )
 VALUES  ( N'浏览' , -- Name - nvarchar(50)
           2 , -- MenuId - bigint
           N'菜单管理' , -- MenuName - nvarchar(50)
           N'' , -- ControlId - nvarchar(50)
           N'' , -- ControlType - nvarchar(50)
           N'' , -- CreateBy - nvarchar(50)
           '2016-04-05 02:24:27' , -- CreateDate - datetime
           N'' , -- LastUpdateBy - nvarchar(50)
           '2016-04-05 02:24:27' , -- LastUpdateDate - datetime
           1 , -- Active - bit
           0 , -- UserBelongTo - bigint
           0  -- CmpyBelongTo - bigint
         )       
         
   
  INSERT INTO dbo.SYS_ROLE
          ( Name ,
            CreateBy ,
            CreateDate ,
            LastUpdateBy ,
            LastUpdateDate ,
            Active ,
            UserBelongTo ,
            CmpyBelongTo
          )
  VALUES  ( N'系统管理员' , -- Name - nvarchar(50)
            N'' , -- CreateBy - nvarchar(50)
            '2016-04-05 02:25:33' , -- CreateDate - datetime
            N'' , -- LastUpdateBy - nvarchar(50)
            '2016-04-05 02:25:33' , -- LastUpdateDate - datetime
            1 , -- Active - bit
            0 , -- UserBelongTo - bigint
            0  -- CmpyBelongTo - bigint
          ) 
          
          
 INSERT INTO dbo.SYS_USER_ROLE
         ( UserId ,
           RoleId ,
           CreateBy ,
           CreateDate ,
           LastUpdateBy ,
           LastUpdateDate ,
           Active ,
           UserBelongTo ,
           CmpyBelongTo
         )
 VALUES  ( 1 , -- UserId - bigint
           1 , -- RoleId - bigint
           N'' , -- CreateBy - nvarchar(50)
           '2016-04-05 02:33:55' , -- CreateDate - datetime
           N'' , -- LastUpdateBy - nvarchar(50)
           '2016-04-05 02:33:55' , -- LastUpdateDate - datetime
           1 , -- Active - bit
           0 , -- UserBelongTo - bigint
           0  -- CmpyBelongTo - bigint
         )         
         
         
 INSERT INTO dbo.SYS_ROLE_MENU_ACTION
         ( RoleId ,
           RoleName ,
           MenuId ,
           MenuName ,
           ActionId ,
           ActionName ,
           ControlId ,
           ControlType ,
           CreateBy ,
           CreateDate ,
           LastUpdateBy ,
           LastUpdateDate ,
           Active ,
           UserBelongTo ,
           CmpyBelongTo
         )
 VALUES  ( 1 , -- RoleId - bigint
           N'系统管理员' , -- RoleName - nvarchar(50)
           3 , -- MenuId - bigint
           N'菜单管理' , -- MenuName - nvarchar(50)
           1 , -- ActionId - bigint
           N'浏览' , -- ActionName - nvarchar(50)
           N'' , -- ControlId - nvarchar(50)
           N'' , -- ControlType - nvarchar(50)
           N'' , -- CreateBy - nvarchar(50)
           '2016-04-05 02:25:24' , -- CreateDate - datetime
           N'' , -- LastUpdateBy - nvarchar(50)
           '2016-04-05 02:25:24' , -- LastUpdateDate - datetime
           1 , -- Active - bit
           0 , -- UserBelongTo - bigint
           0  -- CmpyBelongTo - bigint
         )        
        
        
        
  
        
        
             