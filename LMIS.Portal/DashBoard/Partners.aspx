<%@ Page Title="<%$ Resources:CommonControls,F041_Title %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Partners.aspx.cs" Inherits="LMIS.Portal.DashBoard.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container white-bg">
          
                   <div class="col-lg-9 latest" data-bind="foreach: PartnersList">
                  <div class="item row">
                                <a class="col-md-4 col-sm-4 col-xs-12" data-bind="attr: { href: PartnerDetails }" target="_blank">
                                <img class="img-responsive project-image"  data-bind="attr: { src: OrganizationLogoPath }" alt="Orgnization Logo" />
                                </a>
                                <div class="desc col-md-8 col-sm-8 col-xs-12">
                                    <h3 class="title"><a data-bind="attr: { href: PartnerDetails }" target="_blank" ><span data-bind="    text:OrganizationName"></span> </a></h3>
                                    <p  data-bind="text: GeneralDescriptionCoreBusiness"></p>
                                    <p><i class="fa fa-globe"></i><a class="more-link"  data-bind="attr: { href: 'http://'+  OrganizationWebsite().replace('www.','').replace('http://','').replace('https://','')   }" target="_blank"><span data-bind="    text: OrganizationWebsite"></span></a></p>
                              
                                      <p><a class="more-link"  data-bind="attr: { href:  PartnerDetails }" target="_blank"><i class="fa fa-external-link"></i> <%=GetGlobalResourceObject("CommonControls", "X_FindOutMore")%> </a></p>
                                      </div><!--//desc-->                          
                            </div><!--//item-->
                            
                <!--/.services-->
            </div><!--/.row--> 

<div class="col-lg-3 ">
<div class="becomePartner">
    
</div>
<a href="../DashBoard/BecomePartner.aspx"class="partners-link" > <i class="fa fa-arrow-circle-o-right" ></i><%=GetGlobalResourceObject("CommonControls", "F042_Title")%> </a>
</div>
            <!--/.get-started-->
   
</div>
    <script src="../Scripts/Extensions/config.js"></script>
    <script src="../DashBoard/Scripts/PartnersList.js"></script>

</asp:Content>
