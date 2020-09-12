<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="FaqDetail.aspx.cs" Inherits="LMIS.Portal.FrontEnd.FaqDetail" %>
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
      <script src="../FrontEnd/Scripts/Faq.js"></script>
     <div class="faq-single">
              
                    
                   
                    <div class="col-md-10 col-sm-10 wow fadeInDown" data-wow-duration="1000ms" data-wow-delay="600ms">
                        <div class="faq-wrap orange-faq" data-bind="foreach: FaqsList">
                               <div class="circle multi-line" data-bind="text: GroupName"></div>
                           
                            <div class="single-question-faq"> <h3 data-bind="text:Question  "></h3>
                            <p data-bind="text: Answer "></p>
                            </div>
                      
                    </div>
                    
                       </div>

                    
                </div><!--/.services-->
         

    <script src="../FrontEnd/Scripts/Faq.js"></script>
</asp:Content>
