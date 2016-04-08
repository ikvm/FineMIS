using System.ComponentModel;

namespace FineMIS
{
    /// <summary>
    /// type of relationship
    /// </summary>
    public enum RELATION
    {
        [Description("一对一")]
        ONE_TO_ONE = 0,

        [Description("一对多")]
        ONE_TO_MANY = 1,

        [Description("多对多")]
        MANY_TO_MANY = 2,

        [Description("表内外键")]
        WITH_FOREIGN_KEY = 3,
    }

    public enum ORDER
    {
        [Description("主")]
        MAIN = 1,
        [Description("从")]
        SUB = 2,
        [Description("三")]
        THIRD = 3,
    }

    /// <summary>
    /// entity status
    /// </summary>
    public enum STATUS
    {
        [Description("活动")]
        ACTIVE = 1,

        [Description("删除")]
        INACTIVE = 0,
    }

    /// <summary>
    /// entity verified
    /// </summary>
    public enum VERIFIED
    {
        [CssClass("fgreen")]
        [Description("已审核")]
        PERMIT = 1,

        [CssClass("fred")]
        [Description("已拒绝")]
        REFUSE = -1,

        [CssClass("fgray")]
        [Description("未审核")]
        UNAUDIT = 0,
    }

    /// <summary>
    /// type of action
    /// </summary>
    public enum ACTION
    {
        NONE = 0,
        [Description("刷新")]
        REFRESH = 1,
        [Description("新增")]
        INSERT = 2,
        [Description("删除")]
        DELETE = 3,
        [Description("审核")]
        AUDIT = 4,
        [Description("反审")]
        UNAUDIT = 5,
        [Description("导入")]
        IMPORT = 6,
        [Description("导出")]
        EXPORT = 7,
        [Description("打印")]
        PRINT = 8,
        [Description("添加")]
        APPEND = 9,
        [Description("移除")]
        REMOVE = 10,
        [Description("关闭")]
        CLOSE = 11,
        [Description("重置")]
        RESET = 12,
        [Description("保存并继续")]
        SAVEANDCONTINUE = 13,
        [Description("保存并关闭")]
        SAVEANDCLOSE = 14,
        [Description("更新")]
        UPDATE = 15,
        [Description("查看")]
        DETAIL = 16,
        [Description("全文搜索")]
        FULLTEXTSEARCH = 17,
        [Description("审核通过")]
        PERMIT = 18,
        [Description("审核拒绝")]
        REFUSE = 19,
        [Description("选择")]
        SELECT = 20,
        [Description("新增详情")]
        INSERTDETAIL = 21,
        [Description("自定义")]
        CUSTOM1 = 22,
        [Description("自定义")]
        CUSTOM2 = 23,
        [Description("自定义")]
        CUSTOM3 = 24,
        [Description("自定义")]
        CUSTOM4 = 25,
    }


    /// <summary>
    /// 模块编码
    /// </summary>
    public enum ModelCode
    {
        /// <summary>
        /// 设备维修记录
        /// </summary>
        WXJL,
        /// <summary>
        /// 设备维修验收
        /// </summary>
        WXYS,
        /// <summary>
        /// 设备信息
        /// </summary>
        SBXX,
        /// <summary>
        /// 设备申请
        /// </summary>
        SBSQ,
        /// <summary>
        /// 保养计划
        /// </summary>
        BYJH,
        /// <summary>
        /// 保养记录
        /// </summary>
        BYJL,
        /// <summary>
        /// 油料过磅单
        /// </summary>
        YLGB,
        /// <summary>
        /// 设备借入
        /// </summary>
        SBJR,
        /// <summary>
        /// 设备借出
        /// </summary>
        SBJC,
        /// <summary>
        /// 设备报废
        /// </summary>
        SBBF,
        /// <summary>
        /// 原材料过磅
        /// </summary>
        CLGB,
        /// <summary>
        /// 应付结算
        /// </summary>
        YFJS,
        /// <summary>
        /// 应收结算
        /// </summary>
        YSJS,
        /// <summary>
        /// 已付账款
        /// </summary>
        YFZK,
        /// <summary>
        /// 已收账款
        /// </summary>
        YSZK,
        /// <summary>
        /// 付款发票
        /// </summary>
        FKFP,
        /// <summary>
        /// 收款发票
        /// </summary>
        SKFP,
        /// <summary>
        /// 合同结算
        /// </summary>
        HTJS
    }

    public enum WEIGHTING_STATUS
    {
        [CssClass("fred")]
        [Description("只称了皮重")]
        只称了皮重 = 1,

        [CssClass("fred")]
        [Description("只称了毛重")]
        只称了毛重 = 2,

        [CssClass("fred")]
        [Description("未入库")]
        未入库 = 3,

        [CssClass("fgreen")]
        [Description("已入库")]
        已入库 = 4,
    }

    public enum INPUT_MODE
    {
        [Description("手工录入")]
        MANUAL = 1,
        [Description("系统录入")]
        SYSTEM = 2,
    }

    public enum FLT_PAYTHOD
    {
        [Description("方量")]
        Volume = 1,
        [Description("小时")]
        Hour = 2,
    }

    public enum POURING_MODE
    {
        [Description("泵送")]
        PUMPER = 0,
    }

    public enum FINISH_STATUS
    {
        [Description("未开始")]
        UNSTART = 1,
        [Description("进行中")]
        ING = 2,
        [Description("已完成")]
        DONE = 3,
    }

    public enum RENT_TYPE
    {
        [Description("外租设备")]
        RENT = 1,
        [Description("外租设备归还")]
        RENTRETURN = 2,
        [Description("租出设备")]
        LEASE = 3,
        [Description("租出设备回收")]
        LEASERETURN = 4,
    }

    public enum REPORT_STATUS
    {
        [Description("申请处理中")]
        HANDLING = 1,
        [Description("处理完成")]
        HANDLED = 2,
        [Description("已验收")]
        Check = 3
    }

    /// <summary>
    /// 设备状态
    /// </summary>
    public enum EQU_STATUS
    {
        [Description("正常")]
        InUsed = 0,
        [Description("未使用")]
        UnUse = 1,
        [Description("维修中")]
        Repair = 2,
        [Description("已报废")]
        Obsolete = 3,
        [CssClass("fred")]
        [Description("已借入")]
        Borrow = 4,
        [CssClass("fgreen")]
        [Description("已归还")]
        Return = 5,
        [CssClass("fred")]
        [Description("已借出")]
        Lend = 6
    }

    public enum HANDLE_STATUS
    {
        [Description("处理中")]
        HANDLING = 1,
        [Description("打回申请")]
        RETURN = 2,
        [Description("处理完成")]
        HANDLED = 3,
        [Description("申报中")]
        CREATE = 4,
        [Description("已验收")]
        Check = 5
    }

    public enum HANDLE_TYPE
    {
        [Description("外部维修")]
        OUTER = 1,
        [Description("内部维修")]
        INNER = 2,
    }

    public enum FIN_PAYABLE_TYPE
    {
        [Description("应收")]
        Receivable = 1,
        [Description("应收票据")]
        ReceivableTicket = 2,
        [Description("付款")]
        Payment = 3,
        [Description("原材料付款")]
        MaterialPayment = 4,
        [Description("配件付款")]
        PartsPayment = 5,
        [Description("应付账款")]
        Payable = 6,
        [Description("应收账款")]
        ReceivAcount = 7,
    }

    public enum WEIGHING_SUBSTRACTTYPE
    {
        [Description("扣重管理")]
        Substract = 1,
        [Description("扣重率管理")]
        SubstractRate = 2,
    }

    public enum ENTERPRISE_TYPE
    {
        [Description("政府单位")]
        Government = 1,
        [Description("监理单位")]
        Supervising = 2,
        [Description("施工单位")]
        Building = 3,
        [Description("生产单位")]
        Production = 4
    }


    /// <summary>
    /// 调度单状态 
    /// </summary>
    public enum DeliveryStatus
    {
        [Description("待生产")]
        Active = 1,
        [Description("已完成")]
        Finish = 2,
        [Description("已核单")]
        Audit = 3,
        [Description("已作废")]
        Cancel = 4,
        [Description("生产中")]
        Producting = 5,
        [Description("配送中")]
        Sending = 6,
        [Description("已结算")]
        Settle = 7
    }

    /// <summary>
    /// 调度单类型 
    /// </summary>
    public enum DeliveryType
    {
        /// <summary>
        /// 手动生产 
        /// </summary>
        [Description("手动生产")]
        Manual = 1,
        /// <summary>
        /// 自动生产
        /// </summary>
        [Description("自动生产")]
        Auto = 2
    }

    /// <summary>
    /// 处理方式(退转料)
    /// </summary>
    public enum Delivery_Method
    {
        /// <summary>
        /// 转发
        /// </summary> 
        [Description("转发")]
        Transpond = 1,
        /// <summary>
        /// 自用
        /// </summary>
        [Description("自用")]
        Self = 2,
        /// <summary>
        /// 回收
        /// </summary>
        [Description("回收")]
        Recycle = 3,
        /// <summary>
        /// 废弃
        /// </summary>
        [Description("废弃")]
        Dispose = 4
    }

    public enum VehicleStatus
    {
        /// <summary>
        /// 可调度
        /// </summary>
        ///         
        [CssClass("fgreen")]
        [Description("可调度")]
        Enable = 1,
        /// <summary>
        /// 已调度
        /// </summary>
        /// 
        [CssClass("fred")]
        [Description("已调度")]
        UnEnable = 2,
        /// <summary>
        /// 维修
        /// </summary>
        /// 
        [CssClass("fgray")]
        [Description("维修")]
        Fix = 3,
    }

    /// <summary>
    /// 工控机
    /// </summary>
    public enum IPCReadWrite
    {
        /// <summary>
        /// 读
        /// </summary>
        Read = 0,
        /// <summary>
        /// 写
        /// </summary>
        Write = 1
    }

    /// <summary>
    /// 工控机
    /// </summary>
    public enum IPCReset
    {
        /// <summary>
        /// 不重写
        /// </summary>
        No = 0,
        /// <summary>
        /// 重写
        /// </summary>
        Yes = 1
    }

    /// <summary>
    /// 员工类型
    /// </summary>
    public enum STAFF_TYPE
    {
        [Description("在职")]
        Simple = 1,
        [Description("异动")]
        Unusual = 2,
        [Description("离职")]
        Dimission = 3
    }

    /// <summary>
    /// 员工性别
    /// </summary>
    public enum STAFF_SEX
    {
        [Description("男")]
        男 = 1,
        [Description("女")]
        女 = 0
    }

    /// <summary>
    /// 员工学历
    /// </summary>
    public enum STAFF_EDU
    {
        [Description("初中")]
        初中 = 1,
        [Description("高中")]
        高中 = 2,
        [Description("中技")]
        中技 = 3,
        [Description("中专")]
        中专 = 4,
        [Description("大专")]
        大专 = 5,
        [Description("本科")]
        本科 = 6,
        [Description("硕士")]
        硕士 = 7,
        [Description("博士")]
        博士 = 8,
        [Description("其他")]
        其他 = 9
    }

    /// <summary>
    /// 员工婚姻
    /// </summary>
    public enum STAFF_MARRY
    {
        [Description("未婚")]
        未婚 = 1,
        [Description("已婚未育")]
        已婚未育 = 2,
        [Description("已婚已育")]
        已婚已育 = 3,
        [Description("离异")]
        离异 = 4,
    }

    /// <summary>
    /// 外语水平
    /// </summary>
    public enum STAFF_LEVEL
    {
        [Description("三级")]
        三级 = 1,
        [Description("四级")]
        四级 = 2,
        [Description("六级")]
        六级 = 3,
        [Description("专四")]
        专四 = 4,
        [Description("专八")]
        专八 = 5
    }

    /// <summary>
    /// 招聘状态
    /// </summary>
    public enum RECRUIT_STATUS
    {
        [CssClass("fgreen")]
        [Description("进行中")]
        Doing = 1,
        [CssClass("fred")]
        [Description("已关闭")]
        Done = 2
    }

    /// <summary>
    /// 异动类型
    /// </summary>
    public enum UNUSUAL_TYPE
    {
        [Description("调职")]
        调职 = 1,
        [Description("升职")]
        升职 = 2,
        [Description("降职")]
        降职 = 3,
        [Description("借调")]
        借调 = 4,
        [Description("退休")]
        退休 = 5,
        [Description("其他")]
        其他 = 6
    }


    /// <summary>
    /// 福利类型
    /// </summary>
    public enum WELFARE_TYPE
    {
        [Description("节日福利")]
        节日福利 = 1,
        [Description("补贴")]
        补贴 = 2,
        [Description("全勤奖")]
        全勤奖 = 3,
        [Description("年假")]
        年假 = 4,
        [Description("旅游")]
        旅游 = 5,
        [Description("分红")]
        分红 = 6,
        [Description("股票")]
        股票 = 7,
        [Description("其他")]
        其他 = 8
    }

    /// <summary>
    /// 奖惩类型
    /// </summary>
    public enum REWARDPUNISH_TYPE
    {
        [Description("奖金")]
        奖金 = 1,
        [Description("优秀员工")]
        优秀员工 = 2,
        [Description("升职")]
        升职 = 3,
        [Description("警告")]
        警告 = 4,
        [Description("记过")]
        记过 = 5,
        [Description("停职")]
        停职 = 6,
        [Description("留用查看")]
        留用查看 = 7,
        [Description("降职")]
        降职 = 8,
        [Description("其他")]
        其他 = 9
    }

    /// <summary>
    /// 培训类型
    /// </summary>
    public enum TRAIN_TYPE
    {
        [Description("入职培训")]
        入职培训 = 1,
        [Description("技术培训")]
        技术培训 = 2,
        [Description("企业文化培训")]
        企业文化培训 = 3,
        [Description("外部培训")]
        外部培训 = 4,
        [Description("拓展培训")]
        拓展培训 = 5,
        [Description("其他培训")]
        其他培训 = 6
    }


    #region 车辆调度使用
    public enum GPS_STATUS
    {
        [Description("正转")]
        Forward = 1,
        [Description("反转")]
        Reverse = 2
    }

    public enum GOBACK_STATUS
    {
        [Description("去程")]
        GO = 1,
        [Description("返程")]
        Back = 2
    }
    #endregion

    /// <summary>
    /// 原料需求状态
    /// </summary>
    public enum MATERIALREQUIRE_STATUS
    {
        [CssClass("fred")]
        [Description("未加入合同单")]
        UnActivate = 1,
        [CssClass("fgreen")]
        [Description("已加入合同单")]
        Activate = 2
    }

    public enum MATERIALCONTRACT_STATUS
    {
        [Description("供应中")]
        Supplying = 1,
        [Description("停止供应")]
        StopSupply = 2,
        [Description("暂停供应")]
        PauseSupply = 3
    }

    public enum PURCHASEREQUIRE_STATUS
    {
        [CssClass("fred")]
        [Description("需求未处理")]
        UnHandle = 1,
        [CssClass("fgreen")]
        [Description("需求已处理")]
        Handle = 2
    }

    /// <summary>
    /// 保养等级
    /// </summary>
    public enum EQU_MAINT_LEVEL
    {
        [Description("日常保养")]
        Level = 0,
        [Description("一级保养")]
        Level1 = 1,
        [Description("二级保养")]
        Level2 = 2,
        [Description("三级保养")]
        Level3 = 3,
    }

    public enum OIL_STORAGEOUT_TYPE
    {
        [Description("报废")]
        Scrap = 0,
        [Description("设备维护")]
        EquipmentRepair = 1,
        [Description("生活使用")]
        LifeUsed = 2,
        [Description("其他")]
        Others = 3,
    }

    public enum SUPPLIER_TYPE
    {
        [Description("原料供应商")]
        Material = 1,
        [Description("设备供应商")]
        Equipment = 2,
        [Description("油料供应商")]
        Oil = 3,
    }

    /// <summary>
    /// 过磅单状态
    /// </summary>
    public enum OilWeighing_Status
    {
        [CssClass("fred")]
        [Description("未入库")]
        UnstorageIn = 1,
        [CssClass("fgreen")]
        [Description("已入库")]
        StorageIned = 2,
    }

    public enum SPVEH_LevelType
    {
        [Description("临时车辆")]
        Temporary = 1,
        [Description("固定车辆")]
        Stationary = 2,
    }

    /// <summary>
    /// 设备维修
    /// </summary>
    public enum Repair_Type
    {
        故障维修 = 0,
        普通维修 = 1
    }

    public enum Silo_Type
    {
        /// <summary>
        /// 房屋仓
        /// </summary>
        [Description("房屋仓")]
        House = 0,
        /// <summary>
        /// 粉料仓
        /// </summary>
        [Description("粉料仓")]
        Powder = 1,
        /// <summary>
        ///  骨料仓
        /// </summary>
        [Description("骨料仓")]
        Aggregate = 2,
        /// <summary>
        ///  液剂仓
        /// </summary>
        [Description("液剂仓")]
        Liquid = 3,
    }

    public enum QuickTest_Type
    {
        /// <summary>
        /// 按天数检测
        /// </summary>
        [Description("按天数检测")]
        DateTest = 0,
        /// <summary>
        /// 按吨数检测
        /// </summary>
        [Description("按吨数检测")]
        TunnageTest = 1,
        /// <summary>
        ///  按小时检测
        /// </summary>
        [Description("按小时检测")]
        HourTest = 2,
        /// <summary>
        ///  按车数检测
        /// </summary>
        [Description("按车数检测")]
        VehicleTest = 3,
    }

    #region 检测管理

    public enum Standard_Type
    {
        /// <summary>
        /// 标准类型
        /// </summary>
        [Description("国家标准")]
        国家标准 = 1
    }

    #endregion

    //搅拌方法
    public enum Eunm_MixingMethod
    {
        [Description("机械")]
        机械,
        [Description("手工")]
        手工
    }

    //施工季节
    public enum Eunm_BuildSeason
    {
        [Description("常温施工")]
        常温施工,
        [Description("冬季施工")]
        冬季施工
    }

    //养护方法
    public enum Eunm_MaintMethod
    {
        [Description("标准养护")]
        标准养护,
        [Description("同条件")]
        同条件
    }

    //浇捣方法
    public enum Eunm_PouringMethod
    {
        [Description("机械浇捣")]
        机械浇捣,
        [Description("手工浇捣")]
        手工浇捣
    }

    //归档
    public enum Eunm_Pigeonhole
    {
        [Description("未归档")]
        未归档,
        [Description("归档")]
        归档
    }

    //砼或砂浆
    public enum Eunm_ProductType
    {
        [Description("砼")]
        砼 = 0,
        [Description("砂浆")]
        砂浆 = 1
    }

    //银行类型
    public enum Bank_Type
    {
        [Description("工商银行")]
        ICBC = 0,
        [Description("建设银行")]
        CCB = 1,
        [Description("农业银行")]
        NYYH = 2,
        [Description("招商银行")]
        CMBC = 3,
        [Description("交通银行")]
        BOCOM = 4,
        [Description("信合银行")]
        XHYH = 5,
        [Description("邮政银行")]
        YZYH = 6,
        [Description("中国银行")]
        BOC = 7,
        [Description("中信银行")]
        CITIC = 8,
        [Description("其他银行")]
        Others = 9
    }

    //付款形式
    public enum Paid_Form
    {
        [Description("汇款")]
        Remit = 0,
        [Description("现金")]
        Cash = 1,
        [Description("转账")]
        Transfer = 2,
        [Description("银行承兑汇票")]
        YHCDHP = 3,
        [Description("抵账")]
        Repay = 4,
        [Description("其他")]
        Others = 5
    }

    //付款形式
    public enum Invoice_Status
    {
        [Description("执行")]
        Execute = 0,
        [Description("抵账")]
        Repay = 1,
        [Description("销项负数")]
        XXFS = 2,
        [Description("作废")]
        Invalid = 3
    }

    //工地类型
    public enum Project_Type
    {
        /// <summary>
        ///  普通工地
        /// </summary>
        [Description("普通工地")]
        Normal = 0,
        /// <summary>
        ///  预签工地
        /// </summary>
        [Description("预签工地")]
        Reserve = 1,
        /// <summary>
        ///  零星工地
        /// </summary>
        [Description("零星工地")]
        Fragmentary = 2,
    }

    //工地运距范围
    public enum Project_DistanceRange
    {
        /// <summary>
        ///  0-10
        /// </summary>
        [Description("0-10")]
        To10 = 0,
        /// <summary>
        ///  10-20
        /// </summary>
        [Description("10-20")]
        To20 = 1,
        /// <summary>
        ///  20-30
        /// </summary>
        [Description("20-30")]
        To30 = 2,
        /// <summary>
        ///  30-40
        /// </summary>
        [Description("30-40")]
        To40 = 3,
        /// <summary>
        ///  40-50
        /// </summary>
        [Description("40-50")]
        To50 = 4,
        /// <summary>
        ///  50以上
        /// </summary>
        [Description("50以上")]
        Over50 = 5,
    }

    /// <summary>
    /// 产品标号
    /// </summary>
    public enum Product_Type
    {
        /// <summary>
        ///  水
        /// </summary>
        [Description("水")]
        Water = 0,
        /// <summary>
        ///  污水
        /// </summary>
        [Description("污水")]
        WasteWater = 1,
    }

    /// <summary>
    ///  车辆所属
    /// </summary>
    public enum Vehicle_Belongs
    {
        /// <summary>
        ///  内部
        /// </summary>
        [Description("内部")]
        Inner = 0,
        /// <summary>
        ///  外租
        /// </summary>
        [Description("外租")]
        OutRent = 1,
    }

    public enum Contract_Status
    {
        /// <summary>
        ///  进行中
        /// </summary>
        [Description("进行中")]
        Going = 0,
        /// <summary>
        ///  已结算
        /// </summary>
        [Description("已结算")]
        Settled = 1,
        /// <summary>
        ///  已作废
        /// </summary>
        [Description("已作废")]
        Invalided = 2,
    }
}
