<%@ Page Title="<%$ Resources:CommonControls,F017_Title %>" Language="C#" MasterPageFile="~/MasterPages/BackEnd.Master" AutoEventWireup="true" CodeBehind="ReportsList.aspx.cs" Inherits="LMIS.Portal.Statistics.ReportsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <form id="form1" runat="server">
    
    <table class="display" style="border-spacing: 0; width: 100%;">
        <tr>
            <td><label> <%=GetGlobalResourceObject("CommonControls", "F010_Themes")%> </label></td>
             <td>
            
                 
                <asp:DropDownList ID="ddlThemeType" runat="server" AppendDataBoundItems="True" CssClass="form-control ddlThemeType selectpicker" AutoPostBack="True" OnSelectedIndexChanged="ddlThemeType_SelectedIndexChanged" ></asp:DropDownList>
           
                </td>
        </tr>
            <tr>
         <td> <label> <%=GetGlobalResourceObject("CommonControls", "F010_Title")%> </label></td>
             <td>
            
                 
                <asp:DropDownList ID="ddlThemes" runat="server" CssClass="form-control" AppendDataBoundItems="True" ></asp:DropDownList>
                </td>
        </tr>
            <tr>
         <td> &nbsp;</td>
             <td>
            
                 
                 <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:CommonControls,X_Search %>" OnClick="btnSearch_Click" CssClass="search-icon-block" />
                </td>
        </tr>
            <tr>
         <td> &nbsp;</td>
             <td>
                 <asp:LinkButton ID="CreateReportBtn" runat="server" PostBackUrl="../Statistics/CreateReport.aspx">Create Report</asp:LinkButton>
                 
               
                </td>
        </tr>
    </table>
    
        <asp:GridView ID="gvReports" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvReports_SelectedIndexChanged" DataKeyNames="ReportID" OnRowDeleting="gvReports_RowDeleting">
            <Columns>
                <asp:BoundField DataField="ReportEnName" HeaderText="<%$ Resources:CommonControls,F017_NameEn %>" SortExpression="ReportEnName" />
                <asp:BoundField DataField="ReportArName" HeaderText="<%$ Resources:CommonControls,F017_NameAr %>" SortExpression="ReportArName" />
                <asp:BoundField DataField="ReportFrName" HeaderText="<%$ Resources:CommonControls,F017_NameFr %>" SortExpression="ReportFrName" />
                <asp:BoundField DataField="RunningVariableName" HeaderText="<%$ Resources:CommonControls,F017_RunVariable %>" SortExpression="RunningVariableName" />
                <asp:BoundField DataField="changingVariableName" HeaderText="<%$ Resources:CommonControls,F017_ChangeVariable %>" SortExpression="changingVariableName" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" ><i class="fa fa-edit"></i></asp:LinkButton>
                    </ItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('Are You Sure');" ><i class="fa fa-trash-o"></i></asp:LinkButton>
                    </ItemTemplate>
                 
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
    
</asp:Content>
