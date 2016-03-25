using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FineMIS.Menus
{
    public class Menu
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public string Name { get; set; }
        public string NavigateUrl { get; set; }
        public string ImageUrl { get; set; }
        public int SortIndex { get; set; }
        public string Roles { get; set; }

        public object Clone()
        {
            return new Menu
            {
                Id = Id,
                Name = Name,
                NavigateUrl = NavigateUrl,
                ImageUrl = ImageUrl,
                SortIndex = SortIndex,
            };
        }

        public static List<Menu> Menus
        {
            get
            {
                // todo load from json/xml
                List<Menu> menus = new List<Menu>
                {
                    new Menu
                    {
                        Id = 100,
                        Name = "站点",
                        NavigateUrl = "",
                        ImageUrl = "",
                        SortIndex = 100,
                        ParentId = 0,
                    },
                    new Menu
                    {
                        Id = 101,
                        Name = "商砼站点",
                        NavigateUrl = "http://localhost:64357/Index.aspx",
                        ImageUrl = "~/res/icon/tag_blue.png",
                        SortIndex = 101,
                        ParentId = 100,
                    },
                    new Menu
                    {
                        Id = 102,
                        Name = "砂浆站点",
                        NavigateUrl = "~/Pages/Company/MortarPlant",
                        ImageUrl = "~/res/icon/tag_blue.png",
                        SortIndex = 102,
                        ParentId = 100,
                    },
                    new Menu
                    {
                        Id = 103,
                        Name = "车辆管理",
                        NavigateUrl = "~/Pages/Dispatch/Vehicle",
                        ImageUrl = "~/res/icon/tag_blue.png",
                        SortIndex = 103,
                        ParentId = 100,
                    },
                    new Menu
                    {
                        Id = 200,
                        Name = "工地",
                        NavigateUrl = "",
                        ImageUrl = "",
                        SortIndex = 1,
                        ParentId = 0,
                    },
                    new Menu
                    {
                        Id = 201,
                        Name = "建筑工程",
                        NavigateUrl = "Pages/Project/Project.aspx",
                        ImageUrl = "~/res/icon/tag_blue.png",
                        SortIndex = 1,
                        ParentId = 200,
                    },
                    new Menu
                    {
                        Id = 202,
                        Name = "生产统计",
                        NavigateUrl = "Modules/Dispatch.aspx",
                        ImageUrl = "~/res/icon/tag_blue.png",
                        SortIndex = 1,
                        ParentId = 200,
                    },
                    new Menu
                    {
                        Id = 203,
                        Name = "销售统计",
                        NavigateUrl = "Modules/Dispatch.aspx",
                        ImageUrl = "~/res/icon/tag_blue.png",
                        SortIndex = 1,
                        ParentId = 200,
                    },
                    new Menu
                    {
                        Id = 204,
                        Name = "车辆统计",
                        NavigateUrl = "Modules/Dispatch.aspx",
                        ImageUrl = "~/res/icon/tag_blue.png",
                        SortIndex = 1,
                        ParentId = 200,
                    },
                    new Menu
                    {
                        Id = 300,
                        Name = "统计",
                        NavigateUrl = "",
                        ImageUrl = "",
                        SortIndex = 1,
                        ParentId = 0,
                    },
                    new Menu
                    {
                        Id = 301,
                        Name = "车辆管理",
                        NavigateUrl = "Pages/Console/Vehicle.aspx",
                        ImageUrl = "~/res/icon/tag_blue.png",
                        SortIndex = 1,
                        ParentId = 300,
                    },
                    new Menu
                    {
                        Id = 302,
                        Name = "司机管理",
                        NavigateUrl = "Pages/Console/Driver.aspx",
                        ImageUrl = "~/res/icon/tag_blue.png",
                        SortIndex = 1,
                        ParentId = 300,
                    },
                    new Menu
                    {
                        Id = 303,
                        Name = "任务管理",
                        NavigateUrl = "Pages/Console/Task.aspx",
                        ImageUrl = "~/res/icon/tag_blue.png",
                        SortIndex = 1,
                        ParentId = 300,
                    },
                    new Menu
                    {
                        Id = 303,
                        Name = "系统设置",
                        NavigateUrl = "Modules/Console/Task.aspx",
                        ImageUrl = "~/res/icon/tag_blue.png",
                        SortIndex = 1,
                        ParentId = 300,
                    }
                };


                return menus;
            }
        }
    }
}