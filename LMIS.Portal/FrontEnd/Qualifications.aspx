<%@ Page Title="<%$ Resources:CommonControls,F056 %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Qualifications.aspx.cs" Inherits="LMIS.Portal.FrontEnd.Qualifications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 

<table id="grdQualification" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "F029_Category")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F056")%></th>
                    </tr>
                </thead>
            
                <tbody data-bind="foreach: QualificationsList">
                    <tr>
                        <td data-bind="text: GroupName"></td>
                     <td data-bind="text: Qualification"></td>
                     
                    </tr>
                </tbody>
            </table>
    <script src="../FrontEnd/Scripts/Qualifications.js"></script>
  
</asp:Content>
