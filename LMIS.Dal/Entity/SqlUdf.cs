using System;
using System.Data.Entity;

namespace LMIS.Dal.Entity
{
    public static class SqlUdf
    {
        //<Function Name="SubCodeName" ReturnType="nvarchar" IsComposable="true" Schema ="dbo" Aggregate="false" BuiltIn="false" StoreFunctionName="SubCodeName">
        //  <Parameter Name="SubId" Mode="In" Type="char" Scale="8" />
        //  <Parameter Name="LangId" Mode="In" Type="int" />
        //</Function>
        //<Function Name="SubCodeParent" ReturnType="char" IsComposable="true" Schema="dbo" Aggregate="false" BuiltIn="false" StoreFunctionName="SubCodeParent">
        //  <Parameter Name="SubId" Mode="In" Type="char" Scale="8" />
        //</Function>
        //<Function Name="SubCodeSearchString" Aggregate="false" BuiltIn="false" NiladicFunction="false" IsComposable="true" ParameterTypeSemantics="AllowImplicitConversion" Schema="dbo" ReturnType="nvarchar">
        //  <Parameter Name="SubId" Type="char" Mode="In" />
        //  <Parameter Name="LangId" Type="int" Mode="In" />
        //</Function>
        //<Function Name="PortalUserName" Aggregate="false" BuiltIn="false" NiladicFunction="false" IsComposable="true" ParameterTypeSemantics="AllowImplicitConversion" Schema="dbo" ReturnType="nvarchar">
        //  <Parameter Name="PortalUserId" Type="decimal" Mode="In" />
        //  <Parameter Name="LangId" Type="int" Mode="In" />
        //</Function>
        //<Function Name="RequestTitle" Aggregate="false" BuiltIn="false" NiladicFunction="false" IsComposable="true" ParameterTypeSemantics="AllowImplicitConversion" Schema="dbo" ReturnType="nvarchar">
        //  <Parameter Name="RequestType" Type="char" Mode="In" />
        //  <Parameter Name="RequestId" Type="decimal" Mode="In" />
        //  <Parameter Name="LangId" Type="int" Mode="In" />
        //</Function>
        //<Function Name="RequestDeleteDate" Aggregate="false" BuiltIn="false" NiladicFunction="false" IsComposable="true" ParameterTypeSemantics="AllowImplicitConversion" Schema="dbo" ReturnType="date">
        //  <Parameter Name="RequestType" Type="char" Mode="In" />
        //  <Parameter Name="RequestId" Type="decimal" Mode="In" />
        //</Function>
        [DbFunction("LMISEntities.Store", "SubCodeName")]
        public static string SubCodeName(string subCodeId, int langId)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("LMISEntities.Store", "SubCodeParent")]
        public static string SubCodeParent(string subCodeId)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("LMISEntities.Store", "SubCodeSearchString")]
        public static string SubCodeSearchString(string subCodeId, int langId = 1)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("LMISEntities.Store", "PortalUserName")]
        public static string PortalUserName(decimal portalUserId, int langId)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("LMISEntities.Store", "RequestTitle")]
        public static string RequestTitle(string requestType, decimal requestId, int langId)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("LMISEntities.Store", "RequestDeleteDate")]
        public static DateTime? RequestDeleteDate(string requestType, decimal requestId)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
    }
}