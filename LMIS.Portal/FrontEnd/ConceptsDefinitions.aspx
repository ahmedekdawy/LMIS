<%@ Page Title="<%$ Resources:CommonControls,F058 %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="ConceptsDefinitions.aspx.cs" Inherits="LMIS.Portal.FrontEnd.ConceptsDefinitions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container white-bg">
          
               <div class="faq" >
                    <div class="col-md-12 col-sm-12 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms" data-bind="foreach: ConceptList">
                        <div class="faq-wrap orange-faq">
                            
                            <h2 data-bind="text: ConceptDefTitle"></h2>
                            <div class="questions"> 
                              <p data-bind="text: ConceptDefDesc"> </p>
                              
                            
                            </div>
                            
                            
                        </div>
                    </div><!--/.col-md-6-->

        </div>

        
        </div>
    <script src="../FrontEnd/Scripts/ConceptsDefinitions.js"></script>
</asp:Content>
