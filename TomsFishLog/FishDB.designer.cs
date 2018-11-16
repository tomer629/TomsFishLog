﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TomsFishLog
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="DB_A38EB7_thomasgmiranda")]
	public partial class FishDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public FishDBDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["DB_A38EB7_thomasgmirandaConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public FishDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FishDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FishDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FishDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.spGetFishByUser")]
		public ISingleResult<spGetFishByUserResult> spGetFishByUser([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(40)")] string username)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), username);
			return ((ISingleResult<spGetFishByUserResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.spGetUserID")]
		public ISingleResult<spGetUserIDResult> spGetUserID([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(256)")] string strUserName)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), strUserName);
			return ((ISingleResult<spGetUserIDResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.spUpdateUserOptions")]
		public int spUpdateUserOptions([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserID", DbType="Int")] System.Nullable<int> userID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MarkerSize", DbType="Int")] System.Nullable<int> markerSize, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="MarkerOpacity", DbType="Decimal(6,2)")] System.Nullable<decimal> markerOpacity)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID, markerSize, markerOpacity);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.spGetUserOptions")]
		public ISingleResult<spGetUserOptionsResult> spGetUserOptions([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserID", DbType="Int")] System.Nullable<int> userID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID);
			return ((ISingleResult<spGetUserOptionsResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.getFishFromSpeciesCSV")]
		public ISingleResult<getFishFromSpeciesCSVResult> getFishFromSpeciesCSV([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserID", DbType="Int")] System.Nullable<int> userID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="VarChar(1000)")] string speciesString)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID, speciesString);
			return ((ISingleResult<getFishFromSpeciesCSVResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.spCreateAnglerID")]
		public int spCreateAnglerID([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="NVarChar(128)")] string id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.spGetAnglerID")]
		public ISingleResult<spGetAnglerIDResult> spGetAnglerID([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="NVarChar(128)")] string id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id);
			return ((ISingleResult<spGetAnglerIDResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.spGetLast5FishById")]
		public ISingleResult<spGetLast5FishByIdResult> spGetLast5FishById([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="NVarChar(128)")] string id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id);
			return ((ISingleResult<spGetLast5FishByIdResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.spGetSpeciesList")]
		public ISingleResult<spGetSpeciesListResult> spGetSpeciesList([global::System.Data.Linq.Mapping.ParameterAttribute(Name="AnglerId", DbType="Int")] System.Nullable<int> anglerId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), anglerId);
			return ((ISingleResult<spGetSpeciesListResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.spEnterFish")]
		public int spEnterFish([global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserID", DbType="NVarChar(128)")] string userID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="FishID", DbType="NVarChar(26)")] string fishID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Species", DbType="VarChar(50)")] string species, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="DTCaught", DbType="DateTime")] System.Nullable<System.DateTime> dTCaught, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="DTEntered", DbType="DateTime")] System.Nullable<System.DateTime> dTEntered, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LengthInches", DbType="Decimal(5,2)")] System.Nullable<decimal> lengthInches, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="WeightLbs", DbType="Decimal(5,2)")] System.Nullable<decimal> weightLbs)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userID, fishID, species, dTCaught, dTEntered, lengthInches, weightLbs);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.spGetImagesByFishID")]
		public ISingleResult<spGetImagesByFishIDResult> spGetImagesByFishID([global::System.Data.Linq.Mapping.ParameterAttribute(Name="FishID", DbType="NVarChar(26)")] string fishID)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), fishID);
			return ((ISingleResult<spGetImagesByFishIDResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.spInsertImage")]
		public int spInsertImage([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Id", DbType="NVarChar(128)")] string id, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(26)")] string fishID, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(100)")] string thumbObjectKey, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(1024)")] string thumbUrl, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> thumbExpires, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(100)")] string fullSizeObjectKey, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="NVarChar(1024)")] string fullSizeUrl, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="DateTime")] System.Nullable<System.DateTime> fullSizeExpires)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, fishID, thumbObjectKey, thumbUrl, thumbExpires, fullSizeObjectKey, fullSizeUrl, fullSizeExpires);
			return ((int)(result.ReturnValue));
		}
	}
	
	public partial class spGetFishByUserResult
	{
		
		private int _FishId;
		
		private System.Nullable<int> _UserID;
		
		private string _strUsername;
		
		private string _strSpecies;
		
		private System.Nullable<decimal> _decWeightLbs;
		
		private System.Nullable<decimal> _decLengthInches;
		
		private string _strLocationName;
		
		private System.Nullable<decimal> _decWaterLevel;
		
		private System.Nullable<System.DateTime> _dteTimeEntered;
		
		private System.Nullable<decimal> _decLatitude;
		
		private System.Nullable<decimal> _decLongitude;
		
		public spGetFishByUserResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FishId", DbType="Int NOT NULL")]
		public int FishId
		{
			get
			{
				return this._FishId;
			}
			set
			{
				if ((this._FishId != value))
				{
					this._FishId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="Int")]
		public System.Nullable<int> UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this._UserID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_strUsername", DbType="VarChar(60)")]
		public string strUsername
		{
			get
			{
				return this._strUsername;
			}
			set
			{
				if ((this._strUsername != value))
				{
					this._strUsername = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_strSpecies", DbType="VarChar(100)")]
		public string strSpecies
		{
			get
			{
				return this._strSpecies;
			}
			set
			{
				if ((this._strSpecies != value))
				{
					this._strSpecies = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_decWeightLbs", DbType="Decimal(7,2)")]
		public System.Nullable<decimal> decWeightLbs
		{
			get
			{
				return this._decWeightLbs;
			}
			set
			{
				if ((this._decWeightLbs != value))
				{
					this._decWeightLbs = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_decLengthInches", DbType="Decimal(6,2)")]
		public System.Nullable<decimal> decLengthInches
		{
			get
			{
				return this._decLengthInches;
			}
			set
			{
				if ((this._decLengthInches != value))
				{
					this._decLengthInches = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_strLocationName", DbType="VarChar(60)")]
		public string strLocationName
		{
			get
			{
				return this._strLocationName;
			}
			set
			{
				if ((this._strLocationName != value))
				{
					this._strLocationName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_decWaterLevel", DbType="Decimal(6,2)")]
		public System.Nullable<decimal> decWaterLevel
		{
			get
			{
				return this._decWaterLevel;
			}
			set
			{
				if ((this._decWaterLevel != value))
				{
					this._decWaterLevel = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dteTimeEntered", DbType="DateTime")]
		public System.Nullable<System.DateTime> dteTimeEntered
		{
			get
			{
				return this._dteTimeEntered;
			}
			set
			{
				if ((this._dteTimeEntered != value))
				{
					this._dteTimeEntered = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_decLatitude", DbType="Decimal(9,6)")]
		public System.Nullable<decimal> decLatitude
		{
			get
			{
				return this._decLatitude;
			}
			set
			{
				if ((this._decLatitude != value))
				{
					this._decLatitude = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_decLongitude", DbType="Decimal(9,6)")]
		public System.Nullable<decimal> decLongitude
		{
			get
			{
				return this._decLongitude;
			}
			set
			{
				if ((this._decLongitude != value))
				{
					this._decLongitude = value;
				}
			}
		}
	}
	
	public partial class spGetUserIDResult
	{
		
		private int _UserID;
		
		public spGetUserIDResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="Int NOT NULL")]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this._UserID = value;
				}
			}
		}
	}
	
	public partial class spGetUserOptionsResult
	{
		
		private int _UserID;
		
		private System.Nullable<int> _MarkerSize;
		
		private System.Nullable<decimal> _MarkerOpacity;
		
		public spGetUserOptionsResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="Int NOT NULL")]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this._UserID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MarkerSize", DbType="Int")]
		public System.Nullable<int> MarkerSize
		{
			get
			{
				return this._MarkerSize;
			}
			set
			{
				if ((this._MarkerSize != value))
				{
					this._MarkerSize = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MarkerOpacity", DbType="Decimal(6,2)")]
		public System.Nullable<decimal> MarkerOpacity
		{
			get
			{
				return this._MarkerOpacity;
			}
			set
			{
				if ((this._MarkerOpacity != value))
				{
					this._MarkerOpacity = value;
				}
			}
		}
	}
	
	public partial class getFishFromSpeciesCSVResult
	{
		
		private int _FishId;
		
		private System.Nullable<int> _UserID;
		
		private string _strUsername;
		
		private string _strSpecies;
		
		private System.Nullable<decimal> _decWeightLbs;
		
		private System.Nullable<decimal> _decLengthInches;
		
		private string _strLocationName;
		
		private System.Nullable<decimal> _decWaterLevel;
		
		private System.Nullable<System.DateTime> _dteTimeEntered;
		
		private System.Nullable<decimal> _decLatitude;
		
		private System.Nullable<decimal> _decLongitude;
		
		public getFishFromSpeciesCSVResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FishId", DbType="Int NOT NULL")]
		public int FishId
		{
			get
			{
				return this._FishId;
			}
			set
			{
				if ((this._FishId != value))
				{
					this._FishId = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="Int")]
		public System.Nullable<int> UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this._UserID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_strUsername", DbType="VarChar(60)")]
		public string strUsername
		{
			get
			{
				return this._strUsername;
			}
			set
			{
				if ((this._strUsername != value))
				{
					this._strUsername = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_strSpecies", DbType="VarChar(100)")]
		public string strSpecies
		{
			get
			{
				return this._strSpecies;
			}
			set
			{
				if ((this._strSpecies != value))
				{
					this._strSpecies = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_decWeightLbs", DbType="Decimal(7,2)")]
		public System.Nullable<decimal> decWeightLbs
		{
			get
			{
				return this._decWeightLbs;
			}
			set
			{
				if ((this._decWeightLbs != value))
				{
					this._decWeightLbs = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_decLengthInches", DbType="Decimal(6,2)")]
		public System.Nullable<decimal> decLengthInches
		{
			get
			{
				return this._decLengthInches;
			}
			set
			{
				if ((this._decLengthInches != value))
				{
					this._decLengthInches = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_strLocationName", DbType="VarChar(60)")]
		public string strLocationName
		{
			get
			{
				return this._strLocationName;
			}
			set
			{
				if ((this._strLocationName != value))
				{
					this._strLocationName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_decWaterLevel", DbType="Decimal(6,2)")]
		public System.Nullable<decimal> decWaterLevel
		{
			get
			{
				return this._decWaterLevel;
			}
			set
			{
				if ((this._decWaterLevel != value))
				{
					this._decWaterLevel = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_dteTimeEntered", DbType="DateTime")]
		public System.Nullable<System.DateTime> dteTimeEntered
		{
			get
			{
				return this._dteTimeEntered;
			}
			set
			{
				if ((this._dteTimeEntered != value))
				{
					this._dteTimeEntered = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_decLatitude", DbType="Decimal(9,6)")]
		public System.Nullable<decimal> decLatitude
		{
			get
			{
				return this._decLatitude;
			}
			set
			{
				if ((this._decLatitude != value))
				{
					this._decLatitude = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_decLongitude", DbType="Decimal(9,6)")]
		public System.Nullable<decimal> decLongitude
		{
			get
			{
				return this._decLongitude;
			}
			set
			{
				if ((this._decLongitude != value))
				{
					this._decLongitude = value;
				}
			}
		}
	}
	
	public partial class spGetAnglerIDResult
	{
		
		private int _AnglerID;
		
		public spGetAnglerIDResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AnglerID", DbType="Int NOT NULL")]
		public int AnglerID
		{
			get
			{
				return this._AnglerID;
			}
			set
			{
				if ((this._AnglerID != value))
				{
					this._AnglerID = value;
				}
			}
		}
	}
	
	public partial class spGetLast5FishByIdResult
	{
		
		private System.Nullable<short> _SpeciesID;
		
		private string _SpeciesName;
		
		private System.Nullable<System.DateTime> _DTCaught;
		
		public spGetLast5FishByIdResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SpeciesID", DbType="SmallInt")]
		public System.Nullable<short> SpeciesID
		{
			get
			{
				return this._SpeciesID;
			}
			set
			{
				if ((this._SpeciesID != value))
				{
					this._SpeciesID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SpeciesName", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string SpeciesName
		{
			get
			{
				return this._SpeciesName;
			}
			set
			{
				if ((this._SpeciesName != value))
				{
					this._SpeciesName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DTCaught", DbType="DateTime")]
		public System.Nullable<System.DateTime> DTCaught
		{
			get
			{
				return this._DTCaught;
			}
			set
			{
				if ((this._DTCaught != value))
				{
					this._DTCaught = value;
				}
			}
		}
	}
	
	public partial class spGetSpeciesListResult
	{
		
		private short _speciesID;
		
		private string _SpeciesName;
		
		public spGetSpeciesListResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_speciesID", DbType="SmallInt NOT NULL")]
		public short speciesID
		{
			get
			{
				return this._speciesID;
			}
			set
			{
				if ((this._speciesID != value))
				{
					this._speciesID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SpeciesName", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string SpeciesName
		{
			get
			{
				return this._SpeciesName;
			}
			set
			{
				if ((this._SpeciesName != value))
				{
					this._SpeciesName = value;
				}
			}
		}
	}
	
	public partial class spGetImagesByFishIDResult
	{
		
		private int _AnglerID;
		
		private string _FishID;
		
		private string _thumbObjectKey;
		
		private string _thumbUrl;
		
		private System.Nullable<System.DateTime> _thumbExpires;
		
		private string _fullSizeObjectKey;
		
		private string _fullSizeUrl;
		
		private System.Nullable<System.DateTime> _fullSizeExpires;
		
		public spGetImagesByFishIDResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AnglerID", DbType="Int NOT NULL")]
		public int AnglerID
		{
			get
			{
				return this._AnglerID;
			}
			set
			{
				if ((this._AnglerID != value))
				{
					this._AnglerID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FishID", DbType="NVarChar(26)")]
		public string FishID
		{
			get
			{
				return this._FishID;
			}
			set
			{
				if ((this._FishID != value))
				{
					this._FishID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_thumbObjectKey", DbType="NVarChar(100)")]
		public string thumbObjectKey
		{
			get
			{
				return this._thumbObjectKey;
			}
			set
			{
				if ((this._thumbObjectKey != value))
				{
					this._thumbObjectKey = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_thumbUrl", DbType="NVarChar(1024)")]
		public string thumbUrl
		{
			get
			{
				return this._thumbUrl;
			}
			set
			{
				if ((this._thumbUrl != value))
				{
					this._thumbUrl = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_thumbExpires", DbType="DateTime")]
		public System.Nullable<System.DateTime> thumbExpires
		{
			get
			{
				return this._thumbExpires;
			}
			set
			{
				if ((this._thumbExpires != value))
				{
					this._thumbExpires = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fullSizeObjectKey", DbType="NVarChar(1024)")]
		public string fullSizeObjectKey
		{
			get
			{
				return this._fullSizeObjectKey;
			}
			set
			{
				if ((this._fullSizeObjectKey != value))
				{
					this._fullSizeObjectKey = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fullSizeUrl", DbType="NVarChar(1024)")]
		public string fullSizeUrl
		{
			get
			{
				return this._fullSizeUrl;
			}
			set
			{
				if ((this._fullSizeUrl != value))
				{
					this._fullSizeUrl = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_fullSizeExpires", DbType="DateTime")]
		public System.Nullable<System.DateTime> fullSizeExpires
		{
			get
			{
				return this._fullSizeExpires;
			}
			set
			{
				if ((this._fullSizeExpires != value))
				{
					this._fullSizeExpires = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
