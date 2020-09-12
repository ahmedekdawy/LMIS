<%@ Page Title="<%$ Resources:CommonControls, B002 %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Unions.aspx.cs" Inherits="LMIS.Portal.FrontEnd.Unions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../Scripts/Extensions/ko.trilingualtext.js" async="async"></script>
    <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../FrontEnd/Scripts/Unions.js"></script>
    <div class="tab-pane fade in active" id="tab6default">

        <div class="row form-divider col-sm-12">
            <div class="col-lg-4"><hr></div>
            <div class="col-lg-4 form-divider-title"><%=GetGlobalResourceObject("CommonControls", "B002")%></div>
            <div class="col-lg-4"><hr></div>
        </div>
        
        <div id="List" class="col-sm-12" >

       

            <table id="grdUnions" class="display" style="border-spacing: 0; width: 100%; display: none;">
                <thead>
                    <tr>
                        <th  class="col-sm-3"><%=GetGlobalResourceObject("CommonControls", "B002_Name")%></th>
                        <th class="col-sm-2"><%=GetGlobalResourceObject("CommonControls", "B002_Address")%></th>
                        <th class="col-sm-1"><%=GetGlobalResourceObject("CommonControls", "B002_Telephone")%></th>
                        <th class="col-sm-1"><%=GetGlobalResourceObject("CommonControls", "B002_Fax")%></th>
                        <th class="col-sm-3"><%=GetGlobalResourceObject("CommonControls", "B002_Email")%></th>
                        <th class="col-sm-1"><%=GetGlobalResourceObject("CommonControls", "B002_Website")%></th>
                        <th class="col-sm-1"><%=GetGlobalResourceObject("CommonControls", "B002_Profs")%></th>
                        <th class="col-sm-1">
                            <table>
                                <tr >
                                    <td colspan="2" style="text-align:center"><%=GetGlobalResourceObject("CommonControls", "B002_Comms")%></td>
                              
                                </tr>
                                <tr ">
                                  <td><%=GetGlobalResourceObject("CommonControls", "B002_Gov")%></td>
                            <td><%=GetGlobalResourceObject("CommonControls", "B002_Comm")%></td>
                                </tr>
                            </table>
                        </th>
                     
                    </tr>
                </thead>
           
                <tbody data-bind="foreach: UnionList" >
                    <tr>
                    <td style="vertical-align:text-top" data-bind="text: Name.toLocal"></td>
                    <td style="vertical-align:text-top" data-bind="text: Address.toLocal"></td>
                    <td style="vertical-align:text-top" data-bind="text: Telephone"></td>
                    <td style="vertical-align:text-top" data-bind="text: Fax"></td>
                    <td style="vertical-align:text-top" data-bind="text: Email"></td>
                    <td style="vertical-align:text-top" data-bind="text: Website"></td>
                    <td style="vertical-align:text-top" >
                              <table style="border-spacing: 0; width: 100%;">
               
                    <tbody data-bind="foreach: Professions, visible: Professions().length > 0">
                        <tr>
                            <td data-bind="text: lmis.globalString.toLocal($data, true)"></td>
                        
                        </tr>
                    
                  
                    </tbody>
                
                </table>
                          </td>
                        <td style="vertical-align:text-top" >
                              <table style="border-spacing: 0; width: 100%;">
                 
                    <tbody data-bind="foreach: Committees, visible: Committees().length > 0">
                        <tr>
                            <td data-bind="text: $data.Gov.desc"></td>
                            <td data-bind="text: lmis.globalString.toLocal($data.Name, true)"></td>
                          
                        </tr>
                    </tbody>
              
                </table>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>

    

    </div>

   

</asp:Content>