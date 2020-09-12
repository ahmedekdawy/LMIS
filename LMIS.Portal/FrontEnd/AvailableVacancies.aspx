<%@ Page Title="<%$ Resources:CommonControls,F045%>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="AvailableVacancies.aspx.cs" Inherits="LMIS.Portal.FrontEnd.AvailableVacancies" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table id="grdAvailableVacancies" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr >
                         <th><%=GetGlobalResourceObject("CommonControls", "F045_Qualification")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F045_Count")%></th>
              
                         
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th></th>
                        <th></th>
                    </tr>
                </tfoot>
                <tbody data-bind="foreach: AvailableVacanciesList">
                    <tr >
                        <td >
                              <span data-bind="text: Title" class="show"></span>
                        </td>
                        <td >
                              <span data-bind="text: Count" class="show"></span>
                        </td>
                    
                        
                    </tr>
                </tbody>
            </table>
    <script src="../FrontEnd/Scripts/AvailableVacancies.js"></script>
</asp:Content>
