<%@ Page Title="<%$ Resources:CommonControls,F041_Title %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="PartnerDetails.aspx.cs" Inherits="LMIS.Portal.DashBoard.PartnerDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container white-bg">
          

            <div class="col-lg-9" data-bind="foreach: PartnersList">
                <h2 class=" inner-title" data-bind="text: OrganizationName"> </h2>
               <div class="item row">
                   <div  class="col-md-4 col-sm-4 col-xs-12">
                                
                                <img alt="Logo"  class="img-responsive project-image" data-bind="attr: { src: OrganizationLogoPath }">
                              </div>
                                <div class="desc col-md-8 col-sm-8 col-xs-12">
                                    
                                    <p data-bind="    text: GeneralDescriptionCoreBusiness"></p>
                                    <p data-bind="    text: PossibleAreaOfCooperation"></p>
                                    <p ><i class="fa fa-phone-square" ></i> <span data-bind="    text: Telephone"></span>  </p>
                                    <p><i class="fa fa-location-arrow"></i> <span data-bind="text: Address"></span>  </p>
                                    <p><i class="fa fa-globe"><a data-bind="attr: { href: 'http://' + OrganizationWebsite().replace('www.', '').replace('http://', '').replace('https://', '') }" target="_blank"   class="more-link"></i> <span data-bind="    text: OrganizationWebsite"></span></a></p>
                                </div><!--//desc-->                          
                            </div><!--//item-->
                <!--/.services-->
            </div><!--/.row--> 

<div class="col-lg-3">
<p> Some text about how to become a partner and what is the advantages of it Some text about how to become a partner and what is the advantages of it </p>
<a class="partners-link" href="../BecomePartner"> <i class="fa fa-arrow-circle-o-right"></i>Become a Partner </a>
<a class="partners-link" href="../Partners"> <i class="fa fa-arrow-circle-o-right"></i>All Partners </a>
</div>
   
        </div>
    <script src="../Scripts/Extensions/config.js"></script>
    <script src="../DashBoard/Scripts/PartnersList.js"></script>

</asp:Content>
