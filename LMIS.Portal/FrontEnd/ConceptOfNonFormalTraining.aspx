<%@ Page Title="<%$ Resources:CommonControls,F052 %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="ConceptOfNonFormalTraining.aspx.cs" Inherits="LMIS.Portal.FrontEnd.ConceptNonFormalTraining" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container white-bg">
          

            <div class="col-md-12 blog">
                    <div class="blog-item" data-bind="foreach: ConceptList">
                        <img class="img-responsive img-blog" alt="not found" data-bind="attr: { src: ImagePath }"  width="100%">
                            <div class="row">  
                        
                                <div class="col-xs-12 col-sm-10 blog-content"  >
                                    <h2 data-bind="text: ConceptTitle">  </h2>
                                   <div data-bind="html: ConceptDescription"></div>

                                </div>
                            </div>
                        </div>
                    </div>
        
        </div>
    <script src="../FrontEnd/Scripts/ConceptOfNonFormalTraining.js"></script>
</asp:Content>
