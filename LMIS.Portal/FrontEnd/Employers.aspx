<%@ Page Title="<%$ Resources:CommonControls,F061 %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Employers.aspx.cs" Inherits="LMIS.Portal.FrontEnd.Employers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
             <table id="grdConcept" class="display" style="border-spacing: 0; width: 100%;">
                <thead>
                    <tr>
                        <th><%=GetGlobalResourceObject("CommonControls", "F049_Title")%></th>
                        <th><%=GetGlobalResourceObject("CommonControls", "X_Website")%></th>
                         <th><%=GetGlobalResourceObject("CommonControls", "F049_Description")%></th>
                    </tr>
                </thead>
            
                <tbody data-bind="foreach: ConceptList">
                    <tr>
     <td><span data-bind="    text: Name"></span><img class="img-responsive project-image" style="width: 150px;height:100px"  data-bind="    visible: LogoPath !=null, attr: { src: 'uploads/' + LogoPath() }" alt="Orgnization Logo" /></td>
                  
                        <td>
                                 <p><i class="fa fa-globe"></i><a class="more-link"  data-bind="attr: { href: 'http://' + Website().replace('www.', '').replace('http://', '').replace('https://', '') }" target="_blank"><span data-bind="    text: Website"></span></a></p>
                              
                        </td>
                        <td data-bind="html: Description"></td>
                    </tr>
                </tbody>
            </table>

      
        
      
    <script src="../FrontEnd/Scripts/Employers.js"></script>
</asp:Content>
