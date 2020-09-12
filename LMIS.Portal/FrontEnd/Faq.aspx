<%@ Page Title="<%$ Resources:CommonControls,F057 %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Faq.aspx.cs" Inherits="LMIS.Portal.FrontEnd.Faq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .circle{

    margin:10px;
    width:120px;
    height:70px;
    padding-left: 5px;
    padding-right: 5px;
    border-radius:90%;
    background-color:#3D6AA2;
    color:#fff;
    text-align:center;
  
    font-size:14px;
    overflow:hidden;
}

.circle.one-line{line-height:100px;}
.circle.multi-line{
    padding-top:30px;
    height:70px;
}
    </style>
   <div data-bind="foreach: FaqsList">
   
        <!-- ko if: $index() === 0  -->


      <div class="circle multi-line" data-bind="text: GroupName"></div>
               
        <!-- /ko -->
               <!-- ko if: ($index() !== 0 && $parent.FaqsList()[$index() - 1].GroupName !== GroupName) -->

         <div> <a href="faq-group.html" class="faq-all-questuions">All Questions</a></div>
    
       <div class="circle multi-line" data-bind="text: GroupName"></div>
               
        <!-- /ko -->
  
          <div class="questions"><a  data-bind="attr: { href: '../FaqDetail?id=' + FAQID }"><span data-bind="text: Question "></span></a></div>
          
    
</div>
      <a href="faq-group.html" class="faq-all-questuions">All Questions</a>
    
    <script src="../FrontEnd/Scripts/Faq.js"></script>
</asp:Content>
