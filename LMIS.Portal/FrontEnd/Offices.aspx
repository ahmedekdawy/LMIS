<%@ Page Title="<%$ Resources:CommonControls, B001 %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Offices.aspx.cs" Inherits="LMIS.Portal.FrontEnd.Offices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../FrontEnd/Scripts/Offices.js" async="async"></script>

  

            <table id="grdOffices" class="display" style="border-spacing: 0; width: 100%; display: none;">
                <thead>
                    <tr>
                         <th class="col-lg-4"><%=GetGlobalResourceObject("CommonControls", "B001_Titles")%> </th>
                         <th class="col-lg-4"><%=GetGlobalResourceObject("CommonControls", "B001_Address")%> </th>
                         <th class="col-lg-1"><%=GetGlobalResourceObject("CommonControls", "B001_District")%> </th>
                         <th class="col-lg-1"><%=GetGlobalResourceObject("CommonControls", "B001_Telephone")%> </th>
                         <th class="col-lg-1"><%=GetGlobalResourceObject("CommonControls", "B001_Mobile")%> </th>
                         <th class="col-lg-1"><%=GetGlobalResourceObject("CommonControls", "B001_Fax")%> </th>
                         <th class="col-lg-1"><%=GetGlobalResourceObject("CommonControls", "B001_Hotline")%> </th>
                    </tr>
                </thead>
          
                <tbody data-bind="foreach: OfficeList">
                    <tr data-bind="attr: { id: RowId }">
                        <td data-bind="text: Title.toLocal"></td>
                        <td data-bind="text: Address.toLocal"></td>
                        <td data-bind="text: District.toLocal"></td>
                        <td data-bind="text: Telephone"></td>
                        <td data-bind="text: Mobile"></td>
                        <td data-bind="text: Fax"></td>
                        <td data-bind="text: Hotline"></td>
                    </tr>
                </tbody>
            </table>

      

</asp:Content>