<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="HelpfulLinksFront.aspx.cs" Inherits="LMIS.Portal.FrontEnd.HelpfulLinksFront" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 


    
    <ul data-bind="foreach: HelpfulLinkList">

       <!-- ko if: $index() === 0  -->
        <h1 style="color:steelblue"  data-bind="text: GroupName() +':'" class="label-blue" >:</h1>
        
        <!--/ko-->

         <!-- ko ifnot: ($index() === 0 ) -->
         <!-- ko if:  $parent.HelpfulLinkList()[$index()-1].GroupName() !== GroupName() -->
 <%--       <span data-bind="text: GroupName" ></span>--%>
        <br />
        <h1 style="color:steelblue" data-bind="text: $parent.HelpfulLinkList()[$index() ].GroupName() +':'" ></h1>
         <!--/ko-->
        <!--/ko-->
        <br />
      <a target="_blank"  data-bind="attr: { href: 'http://'+  HelpfulLinkURL().replace('www.','').replace('http://','').replace('https://','') }"><span data-bind="    text: HelpfulLinkName"></span></a>
  
</ul>
    <script src="../FrontEnd/Scripts/HelpfulLinksFront.js"></script>
  
</asp:Content>
