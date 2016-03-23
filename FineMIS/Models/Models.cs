﻿
// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `connectionString`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=.\SQL90;Initial Catalog=W4V2;Integrated Security = SSPI;`
//     Schema:                 ``
//     Include Views:          `False`

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace FineMIS
{

    
	[TableName("dbo.User")]
	[PrimaryKey("Id")]
	[ExplicitColumns]
    public partial class User : BaseModel<User, long>
    {
		[Column] public override long Id { get; set; }
	
		[Column] public string Code { get; set; }
	
		[Column] public string Role { get; set; }
	
		[Column] public string Username { get; set; }
	
		[Column] public string Password { get; set; }
	
		[Column] public int? CanUpdate { get; set; }
	
		[Column] public bool? CanFreePrint { get; set; }
	
		[Column] public bool? CanDelete { get; set; }
	
		[Column] public bool? CanManage { get; set; }
	
		[Column] public bool? IsDemo { get; set; }
	
		[Column] public string Phone { get; set; }
	
		[Column] public string Email { get; set; }
	
		[Column] public int? Status { get; set; }
	
		[Column] public int? Verified { get; set; }
	
		[Column] public string Remarks { get; set; }
	
		[Column] public override string CreateBy { get; set; }
	
		[Column] public override DateTime CreateDate { get; set; }
	
		[Column] public override string LastUpdateBy { get; set; }
	
		[Column] public override DateTime LastUpdateDate { get; set; }
	
		[Column] public override long UserBelongTo { get; set; }
	
		[Column] public override long CmpyBelongTo { get; set; }
	
		[Column] public override bool Active { get; set; }
	
	}
    
	[TableName("dbo.Warehouse")]
	[PrimaryKey("Id")]
	[ExplicitColumns]
    public partial class Warehouse : BaseModel<Warehouse, long>
    {
		[Column] public override long Id { get; set; }
	
		[Column] public string Code { get; set; }
	
		[Column] public string Usage { get; set; }
	
		[Column] public string Type { get; set; }
	
		[Column] public string Name { get; set; }
	
		[Column] public string MaterialName { get; set; }
	
		[Column] public string MaterialModel { get; set; }
	
		[Column] public decimal? Volume { get; set; }
	
		[Column] public DateTime? VolumeUpdateTime { get; set; }
	
		[Column] public string Mapping { get; set; }
	
		[Column] public DateTime? LastUsedTime { get; set; }
	
		[Column] public long? Frequency { get; set; }
	
		[Column] public bool? IsTop { get; set; }
	
		[Column] public int? Status { get; set; }
	
		[Column] public int? Verified { get; set; }
	
		[Column] public string Remarks { get; set; }
	
		[Column] public override string CreateBy { get; set; }
	
		[Column] public override DateTime CreateDate { get; set; }
	
		[Column] public override string LastUpdateBy { get; set; }
	
		[Column] public override DateTime LastUpdateDate { get; set; }
	
		[Column] public override long UserBelongTo { get; set; }
	
		[Column] public override long CmpyBelongTo { get; set; }
	
		[Column] public override bool Active { get; set; }
	
	}
    
	[TableName("dbo.Weighing")]
	[PrimaryKey("Id")]
	[ExplicitColumns]
    public partial class Weighing : BaseModel<Weighing, long>
    {
		[Column] public override long Id { get; set; }
	
		[Column] public string Code { get; set; }
	
		[Column] public string Usage { get; set; }
	
		[Column] public string VehicleName { get; set; }
	
		[Column] public string MaterialName { get; set; }
	
		[Column] public string MaterialModel { get; set; }
	
		[Column] public string SupplierName { get; set; }
	
		[Column] public string WarehouseName { get; set; }
	
		[Column] public bool? UseDefaultTare { get; set; }
	
		[Column] public decimal? RealGrossWeight { get; set; }
	
		[Column] public decimal? DeductRatio { get; set; }
	
		[Column] public decimal? GrossWeight { get; set; }
	
		[Column] public decimal? RealTareWeight { get; set; }
	
		[Column] public decimal? TareWeight { get; set; }
	
		[Column] public decimal? NetWeight { get; set; }
	
		[Column] public decimal? MinusValue { get; set; }
	
		[Column] public decimal? MinusRatio { get; set; }
	
		[Column] public string MinusType { get; set; }
	
		[Column] public decimal? ActualWeight { get; set; }
	
		[Column] public bool? HasGross { get; set; }
	
		[Column] public DateTime? GrossTime { get; set; }
	
		[Column] public string GrossBy { get; set; }
	
		[Column] public bool? HasTare { get; set; }
	
		[Column] public DateTime? TareTime { get; set; }
	
		[Column] public string TareBy { get; set; }
	
		[Column] public bool? IsPrinted { get; set; }
	
		[Column] public DateTime? PrintTime { get; set; }
	
		[Column] public string PrintBy { get; set; }
	
		[Column] public int? IsStocked { get; set; }
	
		[Column] public string StockCommand { get; set; }
	
		[Column] public string StockType { get; set; }
	
		[Column] public DateTime? StockTime { get; set; }
	
		[Column] public string StockBy { get; set; }
	
		[Column] public bool? IsManual { get; set; }
	
		[Column] public int? Status { get; set; }
	
		[Column] public int? Verified { get; set; }
	
		[Column] public string Remarks { get; set; }
	
		[Column] public override string CreateBy { get; set; }
	
		[Column] public override DateTime CreateDate { get; set; }
	
		[Column] public override string LastUpdateBy { get; set; }
	
		[Column] public override DateTime LastUpdateDate { get; set; }
	
		[Column] public override long UserBelongTo { get; set; }
	
		[Column] public override long CmpyBelongTo { get; set; }
	
		[Column] public override bool Active { get; set; }
	
	}
    
	[TableName("dbo.Material")]
	[PrimaryKey("Id")]
	[ExplicitColumns]
    public partial class Material : BaseModel<Material, long>
    {
		[Column] public override long Id { get; set; }
	
		[Column] public string Code { get; set; }
	
		[Column] public string Usage { get; set; }
	
		[Column] public string Name { get; set; }
	
		[Column] public string Model { get; set; }
	
		[Column] public DateTime? LastUsedTime { get; set; }
	
		[Column] public long? Frequency { get; set; }
	
		[Column] public bool? IsTop { get; set; }
	
		[Column] public int? Status { get; set; }
	
		[Column] public int? Verified { get; set; }
	
		[Column] public string Remarks { get; set; }
	
		[Column] public override string CreateBy { get; set; }
	
		[Column] public override DateTime CreateDate { get; set; }
	
		[Column] public override string LastUpdateBy { get; set; }
	
		[Column] public override DateTime LastUpdateDate { get; set; }
	
		[Column] public override long UserBelongTo { get; set; }
	
		[Column] public override long CmpyBelongTo { get; set; }
	
		[Column] public override bool Active { get; set; }
	
	}
    
	[TableName("dbo.Supplier")]
	[PrimaryKey("Id")]
	[ExplicitColumns]
    public partial class Supplier : BaseModel<Supplier, long>
    {
		[Column] public override long Id { get; set; }
	
		[Column] public string Code { get; set; }
	
		[Column] public string Usage { get; set; }
	
		[Column] public string Name { get; set; }
	
		[Column] public DateTime? LastUsedTime { get; set; }
	
		[Column] public long? Frequency { get; set; }
	
		[Column] public bool? IsTop { get; set; }
	
		[Column] public int? Status { get; set; }
	
		[Column] public int? Verified { get; set; }
	
		[Column] public string Remarks { get; set; }
	
		[Column] public override string CreateBy { get; set; }
	
		[Column] public override DateTime CreateDate { get; set; }
	
		[Column] public override string LastUpdateBy { get; set; }
	
		[Column] public override DateTime LastUpdateDate { get; set; }
	
		[Column] public override long UserBelongTo { get; set; }
	
		[Column] public override long CmpyBelongTo { get; set; }
	
		[Column] public override bool Active { get; set; }
	
	}
    
	[TableName("dbo.Vehicle")]
	[PrimaryKey("Id")]
	[ExplicitColumns]
    public partial class Vehicle : BaseModel<Vehicle, long>
    {
		[Column] public override long Id { get; set; }
	
		[Column] public string Code { get; set; }
	
		[Column] public string Usage { get; set; }
	
		[Column] public string Name { get; set; }
	
		[Column] public decimal? Tare { get; set; }
	
		[Column] public decimal? Surplus { get; set; }
	
		[Column] public bool? UseDefaultTare { get; set; }
	
		[Column] public DateTime? LastUsedTime { get; set; }
	
		[Column] public long? Frequency { get; set; }
	
		[Column] public bool? IsTop { get; set; }
	
		[Column] public int? Status { get; set; }
	
		[Column] public int? Verified { get; set; }
	
		[Column] public string Remarks { get; set; }
	
		[Column] public override string CreateBy { get; set; }
	
		[Column] public override DateTime CreateDate { get; set; }
	
		[Column] public override string LastUpdateBy { get; set; }
	
		[Column] public override DateTime LastUpdateDate { get; set; }
	
		[Column] public override long UserBelongTo { get; set; }
	
		[Column] public override long CmpyBelongTo { get; set; }
	
		[Column] public override bool Active { get; set; }
	
	}
}


