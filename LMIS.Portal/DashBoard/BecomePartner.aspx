<%@ Page Title="<%$ Resources:CommonControls,F042_Title %>" Language="C#" MasterPageFile="~/MasterPages/FrontEnd.Master" AutoEventWireup="true" CodeBehind="BecomePartner.aspx.cs" Inherits="LMIS.Portal.DashBoard.BecomePartner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
     <!-- InstanceBeginEditable name="EditRegion1" -->
   <section id="internal-page-section">
  
     <div class="container white-bg">
          

            <div class="">
            <div class="center">  
               
                  <h2><%=GetGlobalResourceObject("CommonControls", "F042_Title")%></h2>
              
                <p class="lead"><asp:Literal ID="Literal1"  runat="server"></asp:Literal></p>
            </div> 
            <div class=" contact-wrap">
            
                <div class="status alert alert-success" style="display: none"></div>
                <form id="main-contact-form" class="contact-form" name="contact-form" method="post" action="">
                
                   
                     <div class="row form-divider "> 
                        <div class="col-lg-4" > <hr> </div>
                        <div class="col-lg-4 form-divider-title " ><%=GetGlobalResourceObject("CommonControls", "F042_ContactsDetails")%> </div>
                        <div class="col-lg-4" > <hr> </div>
                    </div> 
                    
                    <div class="col-sm-5 col-sm-offset-1">
                     
                       <div class="form-group">
                            <label><%=GetGlobalResourceObject("CommonControls", "F042_CEOFirstName")%><span class="">*</span></label>
                            <input type="text" name="name" maxlength="50" data-bind="text: CEOFirstName" class="form-control CEOFirstName validationElement" required>
                        </div>
                             <div class="form-group">
                            <label><%=GetGlobalResourceObject("CommonControls", "F042_CEOEmail")%><span class="">*</span></label>
                            <input type="email" class="form-control CEOEmail validationElement" maxlength="100"  data-bind="text: CEOEmail">
                        </div>                    
                    </div>
                    <div class="col-sm-5">
                     
                        <div class="form-group">
                            <label><%=GetGlobalResourceObject("CommonControls", "F042_CEOLastName")%> <span class="">*</span></label>
                            <input type="text" name="address" class="form-control CEOLastName validationElement" required maxlength="50" data-bind="text:CEOLastName">
                        </div>
                      <div class="form-group col-sm-5">
                          
                      </div>
                    </div>
                    
                    
                    
                     <div class="row form-divider col-lg-12"> 
                        <div class="col-lg-4" > <hr> </div>
                        <div class="col-lg-4 form-divider-title " ><%=GetGlobalResourceObject("CommonControls", "F042_OrganizationSkills")%>  </div>
                        <div class="col-lg-4" > <hr> </div>
                    </div> 
                    
                    <div class="col-sm-10 col-sm-offset-1">
                        <div class="form-group">
                            <label><%=GetGlobalResourceObject("CommonControls", "F042_GeneralDescriptionCoreBusiness")%> <span class="">*</span></label>
                           <textarea name="general-description" data-bind="text:GeneralDescriptionCoreBusiness" id="general-description" required class="form-control GeneralDescriptionCoreBusiness validationElement" rows="3" required maxlength="1000"></textarea>
                        </div>
                        <div class="form-group">
                            <label><%=GetGlobalResourceObject("CommonControls", "F042_PossibleAreaCooperation")%>  </label>
                           <textarea name="cooperation-area" data-bind="text:PossibleAreaOfCooperation" id="cooperation-area" required class="form-control PossibleAreaOfCooperation" rows="3" maxlength="100"></textarea>
                        </div>
                          <div class="form-group">
                            <label><%=GetGlobalResourceObject("CommonControls", "F042_YearFounded")%><span class="">*</span></label>
                            <input type="text" data-bind="text: YearFounded" class="form-control YearFounded validationElement" maxlength="4" >
                        </div>
                           <div class="form-group">
                           
                          <input id="btnSave" value="<%=GetGlobalResourceObject("CommonControls", "F042_SavePartner")%>" class="btn btn-default btn-primary" type="button" data-bind="click: Insert" />
                        </div>                    
                    </div>
                    
                    
                    
                </form> 
            </div><!--/.row-->
        </div><!--/.row--> 


            <!--/.get-started-->

            

            

        </div>
     <!--/.container-->
   </section>
   <!-- InstanceEndEditable -->
    <script src="../DashBoard/Scripts/PartnersList.js"></script>
</asp:Content>
