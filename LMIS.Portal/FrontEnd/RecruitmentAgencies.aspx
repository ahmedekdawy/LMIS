<%@ Page Title="<%$ Resources:CommonControls,F062 %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="RecruitmentAgencies.aspx.cs" Inherits="LMIS.Portal.FrontEnd.RecruitmentAgencies" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
             <table id="grdConcept" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "F062_Titles")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "F062_Description")%></th>
                    </tr>
                </thead>
            
                <tbody data-bind="foreach: RecruitmentAgenciesList">
                    <tr>
     <td><span data-bind="    text: Name"></span>
         <img class="img-responsive project-image" style="width: 150px;height:100px"  data-bind=" visible: LogoPathVisible,   attr: {   src: 'uploads/' + LogoPath() }" /></td>
                  
                 
                        <td data-bind="html: Background"></td>
                    </tr>
                </tbody>
            </table>

    <script src="../FrontEnd/Scripts/RecruitmentAgencies.js"></script>
</asp:Content>
