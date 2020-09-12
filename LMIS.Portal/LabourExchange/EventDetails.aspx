<%@ Page Title="<%$ Resources:CommonControls, F006_EventDetails %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="EventDetails.aspx.cs" Inherits="LMIS.Portal.LabourExchange.EventDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
 <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/EventDetails.js" async="async"></script>

    <div class="col-md-9 blog" >
        <div class="blog-item" data-bind="foreach: VmList">
            <div class="row">
                <div class="col-xs-12 col-sm-3 text-center">
                    <div class="entry-meta">
                  <span id="publish_date" style="font-weight: bolder" data-bind="text: EventDate"></span>
                       <span><i class="fa fa-phone"></i>
                            <span data-bind="text: ContactTelephone">   </span>
                        </span>
                        <span data-bind="visible: ContactWebsite"><i class="fa fa-globe"></i>
                            <a href="#" target="_blank" data-bind="attr: { href: ContactWebsite }"><%=GetGlobalResourceObject("CommonControls", "X_Website")%></a>
                        </span>
                    
                        <span><i class="fa fa-home"></i>
                            <span data-bind="text: ContactAddress">   </span>
                        </span>
                     
                    </div>
                </div>
                <div class="col-xs-12 col-sm-9 blog-content">
                    <h2 data-bind="text:Title "></h2>
                  <div data-bind="visible: Address">
                        <h3><%=GetGlobalResourceObject("CommonControls", "X_Address")%>: <span data-bind="text: Address"></span></h3>
                       
                    </div>
                   
                    <div data-bind="visible: TypeStr">
                        <h3><%=GetGlobalResourceObject("CommonControls", "F006_Type")%>: <span data-bind="text: TypeStr"></span></h3>  
                        
                     
                    </div>
                   <div data-bind="visible: FilePath">
                        <img alt="dd" class="img-responsive" data-bind="attr: { src: FilePath } "/>
                      
                    </div>
               
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-3">
        <div class="widget categories">
        </div>
    </div>


</asp:Content>