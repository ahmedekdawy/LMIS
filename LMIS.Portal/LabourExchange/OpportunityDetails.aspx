<%@ Page Language="C#" Title="<%$ Resources:CommonControls, F005_OppDetails %>" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="OpportunityDetails.aspx.cs" Inherits="LMIS.Portal.LabourExchange.OpportunityDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
 <script src="../Scripts/Extensions/lmis.js" async="async"></script>
    <script src="../LabourExchange/Scripts/OpportunityDetails.js" async="async"></script>

 <div class="col-md-9 blog" >
        <div class="blog-item" data-bind="foreach: VmList">
            <div class="row">
                <div class="col-xs-12 col-sm-3 text-center">
                    <div class="entry-meta">
                  <span id="publish_date" style="font-weight: bolder" data-bind="text: OpportunityDate"></span>
            
                     
                    </div>
                </div>
                <div class="col-xs-12 col-sm-9 blog-content">
                    <h2 data-bind="text: Title "></h2>
           
                   
            
                   <div data-bind="visible: FilePath">
                        <img alt="dd" class="img-responsive"  data-bind="attr: { src: FilePath } "/>
                      
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